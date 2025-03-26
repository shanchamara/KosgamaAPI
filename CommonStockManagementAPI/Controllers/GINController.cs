using Microsoft.AspNetCore.Mvc;
using CommonStockManagementDatabase.Model;
using CommonStockManagementServices.Services;

namespace CommonStockManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GINController : ControllerBase
    {
        private const string API_ROUTE_NAME = "/api/GIN";
        private readonly GINService _GINService;
        public GINController(GINService GINService)
        {
            _GINService = GINService;
        }

        #region GIN Head Details

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAllGINHead")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllGINHead([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null, string type = null)
        {
            try
            {
                var Data = await _GINService.GetAllPaginationGIHead(page, items_per_page, search, sort, order, type);

                var paginationHelper = new PaginationHelper<ViewGINModel>(items_per_page, Data.Count, Data.TotalAmount);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewGINModel>>
                {
                    Data = Data.ViewGINModels,
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
        [Route(API_ROUTE_NAME + "/GetGINHeadDetails/{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetGINHeadDetails(int id)
        {
            try
            {
                return Ok(new DataResponse<ViewGINModel> { Data = await _GINService.GetDetailsByGINHeadId(id, User.Identities.First().Claims.Single(s => s.Type == "uid").Value) });
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/InsertGINHead")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> InsertGINHead([FromBody] InsertGINModel model)
        {
            try
            {
                return Ok(await _GINService.RegisterGINHeadAsync(new InsertGINModel()
                {
                    Created = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Date = model.Date,
                    Description = model.Description,
                    Discount = model.Discount,
                    Gross = model.Gross,
                    Total = model.Total,
                    Type = model.Type,
                    FKLocationId = model.FKLocationId,
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
        [Route(API_ROUTE_NAME + "/UpdateGINHead")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateGINHead([FromBody] UpdateGINModel model)
        {
            try
            {
                model.Edit_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;
                model.Edit_Date = CommonResources.LocalDatetime();

                return Ok(await _GINService.UpdateGINHeadAsync(model));

            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpDelete]
        [Route(API_ROUTE_NAME + "/DeleteGINHead/{id}")]
        public async Task<IActionResult> DeleteGINHead(int id)
        {
            try
            {
                var model = new DeleteGINModel()
                {
                    GINId = id,
                    Delete_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Delete_Date = CommonResources.LocalDatetime()
                };

                return Ok(await _GINService.DeleteGINHeadAsync(model));

            }

            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>()
                {
                    Text = ex.Message
                });
            }
        }


        #endregion


        #region GIN Temp Body Data 

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAllGINBody")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllGINBody([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null, string type = null)
        {
            try
            {
                if (type == "New")
                {
                    _GINService.ClearToBin(User.Identities.First().Claims.Single(s => s.Type == "uid").Value);
                }
                var Data = await _GINService.GetAllGINTempBodyData(page, items_per_page, search, sort, order);

                var paginationHelper = new PaginationHelper<ViewGINBodyModel>(items_per_page, Data.Count, Data.ToatalAmount, Data.ToatalDiscount, Data.ToatalGross);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewGINBodyModel>>
                {
                    Data = Data.ViewGINBodyModels,
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
        [Route(API_ROUTE_NAME + "/GetGINBodyDetails/{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetGINBodyDetails(int id)
        {
            try
            {
                return Ok(new DataResponse<ViewGINBodyModel> { Data = await _GINService.GetDetailsTempBodyById(id) });
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/InsertGINBody")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> InsertGINBody([FromBody] InsertGINbodyModel model)
        {
            try
            {
                return Ok(await _GINService.RegisterTempBodyAsync(new InsertGINbodyModel()
                {
                    Code = model.Code,
                    Cost = model.Cost,
                    DisCount = model.DisCount,
                    ItemID = model.ItemID,
                    Qty = model.Qty,
                    Price = model.Price,
                    UnitCost = model.UnitCost,
                    ItemName = model.ItemName,
                    Amount = model.Amount,
                    UserID = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                }));
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPut]
        [Route(API_ROUTE_NAME + "/UpdateGINBody")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateGINBody([FromBody] UpdateGINBodyModel model)
        {
            try
            {
                model.UserID = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;
                model.Code = model.Code;
                model.Cost = model.Cost;
                model.DisCount = model.DisCount;
                model.ItemID = model.ItemID;
                model.Qty = model.Qty;
                model.Price = model.Price;
                model.UnitCost = model.UnitCost;
                model.GINBodyNo = model.GINBodyNo;
                model.GINNo = model.GINNo;
                model.Id = model.Id;
                return Ok(await _GINService.UpdateTempBodyAsync(model));

            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpDelete]
        [Route(API_ROUTE_NAME + "/DeleteGINBody/{id}")]
        public async Task<IActionResult> DeleteGINBody(int id)
        {
            try
            {
                var model = new DeleteGINBodyModel()
                {
                    Id = id,
                    UserID = User.Identities.First().Claims.Single(s => s.Type == "uid").Value
                };

                return Ok(await _GINService.DeleteTempBodyAsync(model));

            }

            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>()
                {
                    Text = ex.Message
                });
            }
        }


        #endregion



    }
}
