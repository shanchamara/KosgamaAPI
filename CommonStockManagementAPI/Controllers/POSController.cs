using Microsoft.AspNetCore.Mvc;
using CommonStockManagementDatabase.Model;
using CommonStockManagementServices.Services;

namespace CommonStockManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class POSController : ControllerBase
    {
        private const string API_ROUTE_NAME = "/api/POS";
        private readonly IPOSService _POSService;
        public POSController(IPOSService POSService)
        {
            _POSService = POSService;
        }

        #region POS Head Details

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAllPOSHead")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllPOSHead([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null, int CustomerId = 0, int locationId = 0)
        {
            try
            {
                var Data = await _POSService.GetAllPaginationPOSHead(page, items_per_page, search, sort, order, CustomerId, locationId);

                var paginationHelper = new PaginationHelper<ViewPOSHead>(items_per_page, Data.Count, Data.ToatalGross);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewPOSHead>>
                {
                    Data = Data.ViewPOSHeads,
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
        [Route(API_ROUTE_NAME + "/InsertPOSHead")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> InsertPOSHead([FromBody] InsertPOSHead model)
        {
            try
            {
                return Ok(await _POSService.RegisterPOSHeadAsync(new InsertPOSHead()
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
        [Route(API_ROUTE_NAME + "/DeletePOSHead/{id}")]
        public async Task<IActionResult> DeletePOSHead(int id)
        {
            try
            {
                var model = new DeletePOSHead()
                {
                    Id = id,
                    Delete_By = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Delete_Date = CommonResources.LocalDatetime()
                };

                return Ok(await _POSService.DeletePOSHeadAsync(model));

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


        #region POS Temp Body Data 

        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAllPOSBody")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllPOSBody([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null, string type = null)
        {
            try
            {
                var userId = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;
                if (type == "New")
                {
                    _POSService.ClearToBin(userId);
                }
                var Data = await _POSService.GetAllPOSTempBodyData(page, items_per_page, search, sort, order, userId);

                var paginationHelper = new PaginationHelper<ViewTempPOSBody>(items_per_page, Data.Count, Data.ToatalAmount, Data.ToatalDiscount, Data.ToatalGross);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<ViewTempPOSBody>>
                {
                    Data = Data.ViewTempPOSBodies,
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
        [Route(API_ROUTE_NAME + "/GetPOSBodyDetails/{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetPOSBodyDetails(int id)
        {
            try
            {
                return Ok(new DataResponse<ViewTempPOSBody> { Data = await _POSService.GetDetailsTempBodyById(id) });
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/InsertPOSBody")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> InsertPOSBody([FromBody] InsertTempPOSBody model)
        {
            try
            {
                return Ok(await _POSService.RegisterTempBodyAsync(new InsertTempPOSBody()
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
                    Amount = model.Amount,
                    UserID = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    Qtypiece = model.Qtypiece,
                    UnitName = model.UnitName,
                    UnitSize = model.UnitSize,
                }));
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPut]
        [Route(API_ROUTE_NAME + "/UpdatePOSBody")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdatePOSBody([FromBody] UpdateTempPOSBody model)
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
        [Route(API_ROUTE_NAME + "/DeletePOSBody/{id}")]
        public async Task<IActionResult> DeletePOSBody(int id)
        {
            try
            {
                var model = new DeleteTempPOSBody()
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
