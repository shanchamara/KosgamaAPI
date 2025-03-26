using Microsoft.AspNetCore.Mvc;
using CommonStockManagementDatabase.Model;
using CommonStockManagementServices.Services;

namespace CommonStockManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockCategoryController(StockMainCategoryService stockMainItemCategory) : ControllerBase
    {
        private const string API_ROUTE_NAME = "/api/stockCategory";
        private readonly StockMainCategoryService _stockMainItemCategory = stockMainItemCategory;

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAll")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null)
        {
            try
            {
                var Data = await _stockMainItemCategory.GetAllPagination(page, items_per_page, search, sort, order);

                var paginationHelper = new PaginationHelper<ViewStockMainCategory>(items_per_page, Data.Count);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewStockMainCategory>>
                {
                    Data = Data.ViewStockMainCategories,
                    Payload = payload
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAllWithOutPagination")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllWithOutPagination()
        {
            try
            {
                var Data = await _stockMainItemCategory.GetAllPaginationWithoutPagination();


                var response = new DataResponse<List<ViewStockMainCategory>>
                {
                    Data = Data.ViewStockMainCategories,
                    // Payload = payload
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetDetails/{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetDetails(int id)
        {
            try
            {
                return Ok(new DataResponse<ViewStockMainCategory> { Data = await _stockMainItemCategory.GetDetailsById(id) });
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/Insert")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Insert([FromBody] InsertStockMainCategory model)
        {
            try
            {

                return Ok(await _stockMainItemCategory.RegisterAsync(new InsertStockMainCategory()
                {
                    Description = model.Description,
                    Name = model.Name,
                    Edit_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Edit_Date = CommonResources.LocalDatetime(),
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
        public async Task<IActionResult> Update([FromBody] UpdateStockMainCategory model)
        {
            try
            {
                model.Edit_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;
                model.Edit_Date = CommonResources.LocalDatetime();
                return Ok(await _stockMainItemCategory.UpdateAsync(model));

            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpDelete]
        [Route(API_ROUTE_NAME + "/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = new DeleteStockMainCategory()
                {
                    Id = id,
                    Delete_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Delete_Date = CommonResources.LocalDatetime()
                };

                return Ok(await _stockMainItemCategory.DeleteAsync(model));

            }

            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>()
                {
                    Text = ex.Message
                });
            }
        }


    }
}
