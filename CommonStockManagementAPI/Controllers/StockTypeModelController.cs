using Microsoft.AspNetCore.Mvc;
using CommonStockManagementDatabase.Model;
using CommonStockManagementServices.Services;

namespace CommonStockManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTypeModelController : ControllerBase
    {
        private const string API_ROUTE_NAME = "/api/stockTypeModel";
        private readonly StockMainTypeModelService _stockMainItemTypeModel;
        public StockTypeModelController(StockMainTypeModelService stockMainItemCategory)
        {
            _stockMainItemTypeModel = stockMainItemCategory;
        }

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAll")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null)
        {
            try
            {
                var Data = await _stockMainItemTypeModel.GetAllPagination(page, items_per_page, search, sort, order);

                var paginationHelper = new PaginationHelper<ViewStockMainItemTypeModel>(items_per_page, Data.Count);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewStockMainItemTypeModel>>
                {
                    Data = Data.ViewStockMainItemTypeModels,
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
        [Route(API_ROUTE_NAME + "/GetAllWithoutPagination")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllWithoutPagination()
        {
            try
            {
                var Data = await _stockMainItemTypeModel.GetAllPaginationwithoutPagination();


                var response = new DataResponse<List<ViewStockMainItemTypeModel>>
                {
                    Data = Data.ViewStockMainItemTypeModels,
                    //Payload = payload
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
                return Ok(new DataResponse<ViewStockMainItemTypeModel> { Data = await _stockMainItemTypeModel.GetDetailsById(id) });
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/Insert")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Insert([FromBody] InsertStockMainItemTypeModel model)
        {
            try
            {

                return Ok(await _stockMainItemTypeModel.RegisterAsync(new InsertStockMainItemTypeModel()
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
        public async Task<IActionResult> Update([FromBody] UpdateStockMainItemTypeModel model)
        {
            try
            {
                model.Edit_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;
                model.Edit_Date = CommonResources.LocalDatetime();
                return Ok(await _stockMainItemTypeModel.UpdateAsync(model));

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
                var model = new DeleteStockMainItemTypeModel()
                {
                    Id = id,
                    Delete_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Delete_Date = CommonResources.LocalDatetime()
                };

                return Ok(await _stockMainItemTypeModel.DeleteAsync(model));

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
