using Microsoft.AspNetCore.Mvc;
using CommonStockManagementDatabase.Model;
using CommonStockManagementServices.Services;

namespace CommonStockManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GRNController(GRNService gRNService) : ControllerBase
    {
        private const string API_ROUTE_NAME = "/api/GRN";
        private readonly GRNService _GRNService = gRNService;

        #region GRN Head Details

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAllGRNHead")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllGRNHead([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null, int SupplierId = 0, int locationId = 0)
        {
            try
            {
                var Data = await _GRNService.GetAllPaginationGrnHead(page, items_per_page, search, sort, order, SupplierId, locationId);

                var paginationHelper = new PaginationHelper<ViewGrnHead>(items_per_page, Data.Count, Data.TotalAmount);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewGrnHead>>
                {
                    Data = Data.ViewGrnHeads,
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
        [Route(API_ROUTE_NAME + "/GetGRNHeadDetails/{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetGRNHeadDetails(int id)
        {
            try
            {
                return Ok(new DataResponse<ViewGrnHead> { Data = await _GRNService.GetDetailsByGrnHeadId(id, User.Identities.First().Claims.Single(s => s.Type == "uid").Value) });
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/InsertGrnHead")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> InsertGRNHead([FromBody] InsertGrnHead model)
        {
            try
            {
                return Ok(await _GRNService.RegisterGrnHeadAsync(new InsertGrnHead()
                {
                    Created = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Date = model.Date,
                    Description = model.Description,
                    Discount = model.Discount,
                    FKSupplier_ID = model.FKSupplier_ID,
                    GRNType = model.GRNType,
                    Gross = model.Gross,
                    Pono = model.Pono,
                    RefInv = model.RefInv,
                    Total = model.Total,
                    Type = model.Type,
                    Edit_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Edit_Date = CommonResources.LocalDatetime(),
                    LocationId = model.LocationId,
                }));
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPut]
        [Route(API_ROUTE_NAME + "/UpdateGrnHead")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateGRNHead([FromBody] UpdateGrnHead model)
        {
            try
            {
                model.Edit_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;
                model.Edit_Date = CommonResources.LocalDatetime();
                return Ok(await _GRNService.UpdateGrnHeadAsync(model));

            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpDelete]
        [Route(API_ROUTE_NAME + "/DeleteGrnHead/{id}")]
        public async Task<IActionResult> DeleteGRNHead(int id)
        {
            try
            {
                var model = new DeleteGrnHead()
                {
                    Id = id,
                    Delete_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Delete_Date = CommonResources.LocalDatetime()
                };

                return Ok(await _GRNService.DeleteGrnHeadAsync(model));

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


        #region GRN Temp Body Data 

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAllGRNBody")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllGRNBody([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null, string type = null)
        {
            try
            {
                var userId = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;
                if (type == "New")
                {
                    _GRNService.ClearToBin(userId);
                }
                var Data = await _GRNService.GetAllGRnTempBodyData(page, items_per_page, search, sort, order, userId);

                var paginationHelper = new PaginationHelper<ViewTempGrnBody>(items_per_page, Data.Count, Data.ToatalAmount, Data.ToatalDiscount, Data.ToatalGross);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewTempGrnBody>>
                {
                    Data = Data.ViewTempGrnBodies,
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
        [Route(API_ROUTE_NAME + "/GetGRNBodyDetails/{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetGRNBodyDetails(int id)
        {
            try
            {
                return Ok(new DataResponse<ViewTempGrnBody> { Data = await _GRNService.GetDetailsTempBodyById(id) });
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpDelete]
        [Route(API_ROUTE_NAME + "/ClearToBinOnlyUserId/{userId}")]
        public IActionResult ClearToBinOnlyUserId(string userId)
        {
            try
            {
                _GRNService.ClearToBinOnlyUserId(userId);
                return Ok(new Message<string>() { Text = "Items cleared successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "E", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/InsertGRNBody")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> InsertGRNBody([FromBody] InsertTempGrnBody model)
        {
            try
            {
                return Ok(await _GRNService.RegisterTempBodyAsync(new InsertTempGrnBody()
                {
                    Batch = model.Batch,
                    Code = model.Code,
                    Cost = model.Cost,
                    Discount = model.Discount,
                    FreeQty = model.FreeQty,
                    ItemID = model.ItemID,
                    Qty = model.Qty,
                    Sellingprice = model.Sellingprice,
                    UnitCost = model.UnitCost,
                    Item_name = model.Item_name,
                    Amount = model.Amount,
                    GRnNo = model.GRnNo,
                    UnitName = model.UnitName,
                    UnitSize = model.UnitSize,
                    Qtypiece = model.Qtypiece,
                    UserID = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                }));
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPut]
        [Route(API_ROUTE_NAME + "/UpdateGRNBody")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateGRNBody([FromBody] UpdateTempGrnBody model)
        {
            try
            {
                model.UserID = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;
                model.Batch = model.Batch;
                model.Code = model.Code;
                model.Cost = model.Cost;
                model.Discount = model.Discount;
                model.FreeQty = model.FreeQty;
                model.ItemID = model.ItemID;
                model.Qty = model.Qty;
                model.Sellingprice = model.Sellingprice;
                model.UnitCost = model.UnitCost;
                model.GRnBodyNo = model.GRnBodyNo;
                model.GRnNo = model.GRnNo;
                model.Id = model.Id;
                return Ok(await _GRNService.UpdateTempBodyAsync(model));

            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpDelete]
        [Route(API_ROUTE_NAME + "/DeleteGRNBody/{id}")]
        public async Task<IActionResult> DeleteGRNBody(int id)
        {
            try
            {
                var model = new DeleteTempGrnBody()
                {
                    Id = id,
                    UserID = User.Identities.First().Claims.Single(s => s.Type == "uid").Value
                };

                return Ok(await _GRNService.DeleteTempBodyAsync(model));

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
