using Microsoft.AspNetCore.Mvc;
using CommonStockManagementDatabase.Model;
using CommonStockManagementServices.Services;
using Microsoft.AspNetCore.Authorization;

namespace CommonStockManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockMainController(StockMainService stockMainService) : ControllerBase
    {
        private const string API_ROUTE_NAME = "/api/stockMain";
        private readonly StockMainService _stockMainService = stockMainService;

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAll")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null)
        {
            try
            {
                var Data = await _stockMainService.GetAllPagination(page, items_per_page, search, sort, order);

                var paginationHelper = new PaginationHelper<ViewStockMainModel>(items_per_page, Data.Count);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewStockMainModel>>
                {
                    Data = Data.ViewStockMainModels,
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
        [Route(API_ROUTE_NAME + "/GetAllForPriceChanged")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllForPriceChanged([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null, int Categoryid = 0, int brandId = 0, int typeId = 0)
        {
            try
            {
                var Data = await _stockMainService.GetAllPaginationGroupByCategory_Brand_Type(page, items_per_page, search, sort, order, Categoryid, brandId, typeId);

                var paginationHelper = new PaginationHelper<ViewStockMainModel>(items_per_page, Data.Count);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewStockMainModel>>
                {
                    Data = Data.ViewStockMainModels,
                    Payload = payload
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }


        // For the list, View the Quantity balance for each item  
        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAllWithOutPagination")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllWithOutPagination([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null, int locationId = 0)
        {
            try
            {
                var Data = await _stockMainService.GetAllPaginationWithOutPagination(page, items_per_page, search, sort, order, locationId);

                var paginationHelper = new PaginationHelper<ViewStockMainModel>(items_per_page, Data.Count);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewStockMainModel>>
                {
                    Data = Data.ViewStockMainModels,
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
        [Route(API_ROUTE_NAME + "/GetAllWithOutPaginationForSupplierReturn")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllWithOutPaginationForSupplierReturn([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null, string invoiceNo = null, int LocationId = 0)
        {
            try
            {
                var Data = await _stockMainService.GetAllPaginationWithOutPaginationForSupplierReturn(page, items_per_page, search, sort, order, invoiceNo, LocationId);

                var paginationHelper = new PaginationHelper<ViewStockMainModel>(items_per_page, Data.Count);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewStockMainModel>>
                {
                    Data = Data.ViewStockMainModels,
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
        [Route(API_ROUTE_NAME + "/GetAllItemPriceBackupHistory")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllItemPriceBackupHistory([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null)
        {
            try
            {
                var Data = await _stockMainService.GetAllPriceBackUphistoryList(page, items_per_page, search, sort, order);

                var paginationHelper = new PaginationHelper<VwAllPriceBackupHistory>(items_per_page, Data.Count);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<VwAllPriceBackupHistory>>
                {
                    Data = Data.VwAllPriceBackupHistories,
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
        [Route(API_ROUTE_NAME + "/GetDetails/{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetDetails(int id)
        {
            try
            {
                return Ok(new DataResponse<ViewStockMainModel> { Data = await _stockMainService.GetDetailsById(id) });
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/Insert")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Insert([FromForm] InsertStockMainModel model)
        {
            try
            {

                return Ok(await _stockMainService.RegisterAsync(new InsertStockMainModel()
                {
                    ItemName = model.ItemName,
                    ItemCode = model.ItemCode,
                    ImageUrl = model.ImageUrl,
                    ItemDescription = model.ItemDescription,
                    FkUnitId = model.FkUnitId,
                    UnitSize = model.UnitSize,
                    FkCategoryId = model.FkCategoryId,
                    FkBrandId = model.FkBrandId,
                    LastPurchasePrice = model.LastPurchasePrice,
                    SellingPrice = model.SellingPrice,
                    MaxLevel = model.MaxLevel,
                    MinLevel = model.MinLevel,
                    ReorderLevel = model.ReorderLevel,
                    IsItemCode = model.IsItemCode,
                    Edit_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Edit_Date = CommonResources.LocalDatetime(),
                }));
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/PriceChanged")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PriceChanged([FromForm] ChangeItemPriceByCategoryWiseViewModel model)
        {
            try
            {
                return Ok(await _stockMainService.PercentagePriceChangeItemWise(new ChangeItemPriceByCategoryWiseViewModel()
                {
                    FkBrandId = model.FkBrandId,
                    FkCategoryId = model.FkCategoryId,
                    PercentageLastPurchasePrice = model.PercentageLastPurchasePrice,
                    PercentageSellingPrice = model.PercentageSellingPrice,
                    PriceChangeBackupDate = CommonResources.LocalDatetime(),
                    Edit_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                }));
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/RecoveryPriceChanged")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> RecoveryPriceChanged([FromBody] ChangeItemPriceByCategoryWiseViewModel model)
        {
            try
            {
                return Ok(await _stockMainService.RecoveryPriceChangeItemWise(new ChangeItemPriceByCategoryWiseViewModel()
                {
                    FkBrandId = model.FkBrandId,
                    FkCategoryId = model.FkCategoryId,
                    PercentageLastPurchasePrice = model.PercentageLastPurchasePrice,
                    PercentageSellingPrice = model.PercentageSellingPrice,
                    PriceChangeBackupDate = model.PriceChangeBackupDatestring,
                    Edit_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
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
        public async Task<IActionResult> Update([FromForm] UpdateStockMainModel model)
        {
            try
            {
                model.Edit_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;
                model.Edit_Date = CommonResources.LocalDatetime();
                return Ok(await _stockMainService.UpdateAsync(model));

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
                var model = new DeleteStockMainModel()
                {
                    ID = id,
                    Delete_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Delete_Date = CommonResources.LocalDatetime()
                };

                return Ok(await _stockMainService.DeleteAsync(model));

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
