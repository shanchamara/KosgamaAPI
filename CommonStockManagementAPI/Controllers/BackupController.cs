using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using CommonStockManagementDatabase.Model;
using CommonStockManagementServices.Services;

namespace CommonStockManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BackupController(DatabaseBackupServices ClientService, IConfiguration configuration) : Controller
    {
        private const string API_ROUTE_NAME = "/api/backup";
        private readonly DatabaseBackupServices _ClientService = ClientService;
        private readonly IConfiguration _configuration = configuration;

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAll")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null)
        {
            try
            {
                var Data = await _ClientService.GetAllPagination(page, items_per_page, search, sort, order);

                var paginationHelper = new PaginationHelper<ViewDatabaseBackup>(items_per_page, Data.Count);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);

                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewDatabaseBackup>>
                {
                    Data = Data.ViewDatabaseBackup,
                    Payload = payload
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/BackupDatabase")]
        public async Task<IActionResult> BackupDatabaseAsync([FromBody] InsertDatabaseBackup model)
        {
            // Generate a unique backup file name with timestamp
            string backupFileName = $"backup_{DateTime.Now:yyyyMMddHHmmss}.sql";

            // Path to the directory where you want to store backups

            string conn = _configuration.GetConnectionString("DefaultConnection");
            var type = backupFileName;
            var ImageURl = string.Format("{0}.{1}", Guid.NewGuid().ToString(), type);
            var fileParth = string.Format("{0}/{1}", CommonResources.System_File_Path + "/Database/", ImageURl);
            using (MySqlConnection connection = new(conn))
            {
                using (MySqlCommand cmd = new())
                {
                    using (MySqlBackup backup = new(cmd))
                    {
                        try
                        {
                            connection.Open();
                            cmd.Connection = connection;
                            backup.ExportToFile(fileParth);

                            return Ok(await _ClientService.RegisterAsync(new InsertDatabaseBackup()
                            {
                                DatabaseName = ImageURl,
                                Date = Convert.ToString(CommonResources.LocalDatetime()),
                                Reason = model.Reason,
                                TagDiscription = model.TagDiscription,
                                Edit_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                                Edit_Date = CommonResources.LocalDatetime(),
                                UserName = model.UserName,
                            }));
                            //return Ok("Backup successful.");
                        }
                        catch (Exception ex)
                        {
                            return StatusCode(500, "Error: " + ex.Message);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                    ;
                }
                ;
            }
            ;
        }

        [HttpGet]
        [Route(API_ROUTE_NAME + "/Download/{filename}")]
        public async Task<IActionResult> Download(string filename)
        {
            var filePath = Path.Combine(string.Format("{0}/{1}", CommonResources.System_File_Path + "/Database/", filename));

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "application/octet-stream", filename);
        }
    }
}
