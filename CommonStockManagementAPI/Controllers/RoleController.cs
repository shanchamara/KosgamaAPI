using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CommonStockManagementDatabase.Model;
using CommonStockManagementDatabase.Model;
using CommonStockManagementServices.Services;

namespace CommonStockManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private const string API_ROUTE_NAME = "/api/roles";

        private readonly UserRole _staffcategoryService;
        public RoleController(UserRole staffcategoryService)
        {
            _staffcategoryService = staffcategoryService;
        }


        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null)
        {
            try
            {
                var Data = await _staffcategoryService.GetAll(page, items_per_page, search);

                double totalPages = (double)Data.Count / items_per_page;
                int roundedUpPages = (int)Math.Ceiling(totalPages);

                List<Arrylsit> paginationButtons = new();

                // Add Previous button if not on the first page
                if (page > 1)
                {
                    paginationButtons.Add(new Arrylsit { Active = false, Label = "&laquo; Previous", Url = $"/?page={page - 1}", Page = page - 1 });
                }

                // Calculate the range of pages to display, limiting to 4 pages
                int startPage = Math.Max(1, page - 2);
                int endPage = Math.Min(startPage + 3, roundedUpPages);

                // Add page buttons
                for (int i = startPage; i <= endPage; i++)
                {
                    paginationButtons.Add(new Arrylsit
                    {
                        Page = i,
                        Active = i == page,
                        Label = i.ToString(),
                        Url = $"/?page={i}"
                    });
                }

                // Add Next button if not on the last page
                if (page < roundedUpPages)
                {
                    paginationButtons.Add(new Arrylsit { Active = false, Label = "Next &raquo;", Url = $"/?page={page + 1}", Page = page + 1 });
                }


                var paginationinforo = new PaginationInfo // a custom class for holding pagination information
                {
                    First_page_url = $"/?page={1}",
                    Page = page,
                    From = (page * items_per_page) - items_per_page + 1,
                    To = (page * items_per_page),
                    Last_page = roundedUpPages,
                    Items_per_page = items_per_page,
                    Prev_page_url = page == 1 ? null : $"/?page={page - 1}",
                    Next_page_url = page + 1 > roundedUpPages == true ? null : $"/?page={page + 1}",
                    Total = Data.Count,
                    Links = paginationButtons

                };

                var payload = new Payload
                {
                    Pagination = paginationinforo,
                };

                var response = new DataResponse<List<UserRoleModel>>
                {
                    Data = Data.Account,
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
        [Route(API_ROUTE_NAME + "/Insert")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Insert([FromBody] IdentityRole model)
        {
            try
            {

                return Ok(await _staffcategoryService.RegisterAsync(new IdentityRole()
                {
                    Name = model.Name,

                }));
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPut]
        [Route(API_ROUTE_NAME + "/Update")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update([FromBody] IdentityRole model)
        {
            try
            {
                return Ok(await _staffcategoryService.UpdateAsync(model));
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }


        [HttpDelete]
        [Route(API_ROUTE_NAME + "/Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                return Ok(await _staffcategoryService.DeleteAsync(id));

            }

            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }


        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetDetails/{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetDetails(string id)
        {
            try
            {
                if (id != "null")
                {
                    return Ok(new DataResponse<IdentityRole> { Data = await _staffcategoryService.GetDetailsById(id) });
                }
                return Ok(new DataResponse<EditAccountDetails> { Data = null });
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

    }
}
