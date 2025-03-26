using Microsoft.AspNetCore.Mvc;
using CommonStockManagementDatabase.Model;
using CommonStockManagementServices.Services;

namespace CommonStockManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class POSReturnController(POSReturnService POSService) : ControllerBase
    {
        private const string API_ROUTE_NAME = "/api/POSReturn";
        private readonly POSReturnService _POSService = POSService;

        #region POSReturn Head Details

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAllPOSReturnHead")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllPOSReturnHead([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null, int customerId = 0, int locationId = 0)
        {
            try
            {
                var Data = await _POSService.GetAllPaginationPOSReturnHead(page, items_per_page, search, sort, order, customerId, locationId);

                var paginationHelper = new PaginationHelper<ViewPOSReturnHead>(items_per_page, Data.Count, Data.ToatalAmount);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewPOSReturnHead>>
                {
                    Data = Data.ViewPOSReturnHeads,
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
        [Route(API_ROUTE_NAME + "/CheckPOSReturnInvoiceNo")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CheckGRNInvoiceNo([FromBody] InsertPOSReturnHead model)
        {
            try
            {
                return Ok(await _POSService.CheckInvoiceHaveaPOS(new InsertPOSReturnHead()
                {
                    RefInv = model.RefInv,
                    FKClientId = model.FKClientId,
                    Edit_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value
                }));
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/InsertPOSReturnHead")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> InsertPOSReturnHead([FromBody] InsertPOSReturnHead model)
        {
            try
            {
                return Ok(await _POSService.RegisterPOSReturnHeadAsync(new InsertPOSReturnHead()
                {
                    Created = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Date = model.Date,
                    Description = model.Description,
                    Discount = model.Discount,
                    FKClientId = model.FKClientId,
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



        [HttpDelete]
        [Route(API_ROUTE_NAME + "/DeletePOSReturnHead/{id}")]
        public async Task<IActionResult> DeletePOSReturnHead(int id)
        {
            try
            {
                var model = new DeletePOSReturnHead()
                {
                    Id = id,
                    Delete_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Delete_Date = CommonResources.LocalDatetime()
                };

                return Ok(await _POSService.DeletePOSReturnHeadAsync(model));

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


        #region POSReturn Temp Body Data 

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAllPOSReturnBody")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllPOSReturnBody([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null)
        {
            try
            {
                var userId = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;
                var Data = await _POSService.GetAllPOSReturnTempBodyData(page, items_per_page, search, sort, order, userId);

                var paginationHelper = new PaginationHelper<ViewTempPOSReturnBody>(items_per_page, Data.Count, Data.ToatalAmount, Data.ToatalDiscount, Data.ToatalGross);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewTempPOSReturnBody>>
                {
                    Data = Data.ViewTempPOSReturnBodies,
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
        [Route(API_ROUTE_NAME + "/GetAllPOSInvoiceItems")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllPOSInvoiceItems([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null, string Invoiceno = null, int locationId = 0)
        {
            try
            {

                var Data = await _POSService.GetAllPOSInvoiceItems(page, items_per_page, search, sort, order, Invoiceno, locationId);

                var paginationHelper = new PaginationHelper<ViewAllPOsInvoiceItem>(items_per_page, Data.Count);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewAllPOsInvoiceItem>>
                {
                    Data = Data.ViewAllPOsInvoiceItems,
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
        [Route(API_ROUTE_NAME + "/InsertPOSReturnBody")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> InsertPOSReturnBody([FromBody] InsertTempPOSReturnBody model)
        {
            try
            {
                return Ok(await _POSService.RegisterTempBodyAsync(new InsertTempPOSReturnBody()
                {
                    Code = model.Code,
                    Cost = model.Cost,
                    Discount = model.Discount,
                    FreeQty = model.FreeQty,
                    ItemID = model.ItemID,
                    Qty = model.Qty,
                    Sellingprice = model.Sellingprice,
                    UnitCost = model.UnitCost,
                    ItemName = model.ItemName,
                    RefInv = model.RefInv,
                    POSBodyNo = model.POSBodyNo,
                    Amount = model.Amount,
                    UserID = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    UnitSize = model.UnitSize,
                    UnitName = model.UnitName,
                    Qtypiece = model.Qtypiece,
                }));
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPut]
        [Route(API_ROUTE_NAME + "/UpdatePOSReturnBody")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdatePOSReturnBody([FromBody] UpdateTempPOSReturnBody model)
        {
            try
            {
                model.UserID = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;

                return Ok(await _POSService.UpdateTempBodyAsync(model));

            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpDelete]
        [Route(API_ROUTE_NAME + "/DeletePOSReturnBody/{id}")]
        public async Task<IActionResult> DeletePOSReturnBody(int id)
        {
            try
            {
                var model = new DeleteTempPOSReturnBody()
                {
                    Id = id,
                    UserID = User.Identities.First().Claims.Single(s => s.Type == "uid").Value
                };

                return Ok(await _POSService.DeleteTempBodyAsync(model));

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
