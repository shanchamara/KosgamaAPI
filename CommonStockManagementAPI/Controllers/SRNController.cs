using Microsoft.AspNetCore.Mvc;
using CommonStockManagementDatabase.Model;
using CommonStockManagementServices.Services;

namespace CommonStockManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SRNController(SRNService SRNService) : ControllerBase
    {
        private const string API_ROUTE_NAME = "/api/SRN";
        private readonly SRNService _SRNService = SRNService;

        #region SRN Head Details

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAllSRNHead")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllSRNHead([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null, int SupplierId = 0, int LocationId = 0)
        {
            try
            {
                var Data = await _SRNService.GetAllPaginationSRNHead(page, items_per_page, search, sort, order, SupplierId, LocationId);

                var paginationHelper = new PaginationHelper<ViewSRNHead>(items_per_page, Data.Count, Data.ToatalAmount);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewSRNHead>>
                {
                    Data = Data.ViewSRNHeads,
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
        [Route(API_ROUTE_NAME + "/GetSRNHeadDetails/{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetSRNHeadDetails(int id)
        {
            try
            {
                return Ok(new DataResponse<ViewSRNHead> { Data = await _SRNService.GetDetailsBySRNHeadId(id, User.Identities.First().Claims.Single(s => s.Type == "uid").Value) });
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/InsertSRNHead")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> InsertSRNHead([FromBody] InsertSRNHead model)
        {
            try
            {
                return Ok(await _SRNService.RegisterSRNHeadAsync(new InsertSRNHead()
                {
                    Created = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Date = model.Date,
                    Description = model.Description,
                    Discount = model.Discount,
                    FKSupplier_ID = model.FKSupplier_ID,
                    SRNType = model.SRNType,
                    Gross = model.Gross,
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

        [HttpPost]
        [Route(API_ROUTE_NAME + "/CheckGRNInvoiceNo")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CheckGRNInvoiceNo([FromBody] InsertSRNHead model)
        {
            try
            {
                return Ok(await _SRNService.CheckInvoiceHaveaGRN(new InsertSRNHead()
                {
                    RefInv = model.RefInv,
                    FKSupplier_ID = model.FKSupplier_ID,
                    Edit_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value
                }));
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPut]
        [Route(API_ROUTE_NAME + "/UpdateSRNHead")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateSRNHead([FromBody] UpdateSRNHead model)
        {
            try
            {
                model.Edit_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;
                model.Edit_Date = CommonResources.LocalDatetime();
                return Ok(await _SRNService.UpdateSRNHeadAsync(model));

            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpDelete]
        [Route(API_ROUTE_NAME + "/DeleteSRNHead/{id}")]
        public async Task<IActionResult> DeleteSRNHead(int id)
        {
            try
            {
                var model = new DeleteSRNHead()
                {
                    ID = id,
                    Delete_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Delete_Date = CommonResources.LocalDatetime()
                };

                return Ok(await _SRNService.DeleteSRNHeadAsync(model));

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


        #region SRN Temp Body Data 

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAllSRNBody")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllSRNBody([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null)
        {
            try
            {
                var userId = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;

                var Data = await _SRNService.GetAllSRNTempBodyData(page, items_per_page, search, sort, order, userId);

                var paginationHelper = new PaginationHelper<ViewTempSRNBody>(items_per_page, Data.Count, Data.ToatalAmount, Data.ToatalDiscount, Data.ToatalGross);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewTempSRNBody>>
                {
                    Data = Data.ViewTempSRNBodies,
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
        [Route(API_ROUTE_NAME + "/GetSRNBodyDetails/{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetSRNBodyDetails(int id)
        {
            try
            {
                return Ok(new DataResponse<ViewTempSRNBody> { Data = await _SRNService.GetDetailsTempBodyById(id) });
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/InsertSRNBody")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> InsertSRNBody([FromBody] InsertTempSRNBody model)
        {
            try
            {
                return Ok(await _SRNService.RegisterTempBodyAsync(new InsertTempSRNBody()
                {
                    Batch = model.Batch,
                    Code = model.Code,
                    Cost = model.Cost,
                    Discount = model.Discount,
                    ItemID = model.ItemID,
                    Qty = model.Qty,
                    Sellingprice = model.Sellingprice,
                    UnitCost = model.UnitCost,
                    ItemName = model.ItemName,
                    Amount = model.Amount,
                    RefInv = model.RefInv,
                    UnitSize = model.UnitSize,
                    UnitName = model.UnitName,
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
        [Route(API_ROUTE_NAME + "/UpdateSRNBody")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateSRNBody([FromBody] UpdateTempSRNBody model)
        {
            try
            {
                model.UserID = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;
                model.Batch = model.Batch;
                model.Code = model.Code;
                model.Cost = model.Cost;
                model.Discount = model.Discount;
                model.ItemID = model.ItemID;
                model.Qty = model.Qty;
                model.Sellingprice = model.Sellingprice;
                model.UnitCost = model.UnitCost;
                model.SRNBodyNo = model.SRNBodyNo;
                model.SRNNo = model.SRNNo;
                model.Id = model.Id;
                return Ok(await _SRNService.UpdateTempBodyAsync(model));

            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpDelete]
        [Route(API_ROUTE_NAME + "/DeleteSRNBody/{id}")]
        public async Task<IActionResult> DeleteSRNBody(int id)
        {
            try
            {
                var model = new DeleteTempSRNBody()
                {
                    Id = id,
                    UserID = User.Identities.First().Claims.Single(s => s.Type == "uid").Value
                };

                return Ok(await _SRNService.DeleteTempBodyAsync(model));

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
