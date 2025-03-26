using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Model;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CommonStockManagementServices.Services
{


    public class POSReturnService(AppDbContext context, AuditTrailService auditTrailService, IMemoryCache cache)
    {
        private readonly AppDbContext _context = context;
        private readonly AuditTrailService _auditTrailService = auditTrailService;
        private readonly IMemoryCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));

        #region Header Details
        public async Task<PaginationViewPOSReturnHead> GetAllPaginationPOSReturnHead(int page, int pagecount, string searchTerm, string sort, string order, int CustomerId, int locationId)
        {

            var cacheKey = $"{nameof(PaginationViewPOSReturnHead)}";
            // Check if the result is already in the cache

            IQueryable<VwListPOSReturnHeads> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewPOSReturnHead cachedResult))
            {

                query = cachedResult.IQueryData.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<VwListPOSReturnHeads, object>> defaultSort1 = x => x.Id;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<VwListPOSReturnHeads>.Search([.. query], searchTerm, c => c.InvoiceDate.ToString(), c => c.Description, c => c.Type, c => c.Id.ToString(),
                 c => c.Gross.ToString(), c => c.Total.ToString(), c => c.RefInv);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewPOSReturnHead
                       {

                           Delete_By = a.Delete_By,
                           Delete_Date = a.Delete_Date,
                           Edit_By = a.Edit_By,
                           Edit_Date = a.Edit_Date,
                           Id = a.Id,
                           Created = a.Created,
                           Date = a.InvoiceDate.ToString("yyyy-MM-dd"),
                           Customer = a.Customer,
                           Discount = a.Discount,
                           Gross = a.Gross,
                           RefInv = a.RefInv,
                           Total = a.Total,
                           Type = a.Type,
                           IsDelete = a.IsDelete,
                           Description = a.Description,
                           LocationId = a.LocationId,
                           FKClientId = a.FKClientId
                       }).Where(d => d.FKClientId.Equals(CustomerId) && d.LocationId.Equals(locationId))
                       .ToList();

                // Prepare the pagination response
                var TotalAmount = pagedData1.Sum(x => x.Gross);
                var paginationListData1 = new PaginationViewPOSReturnHead
                {
                    Count = pagedData1.Count,
                    ViewPOSReturnHeads = pagedData1.Skip((page - 1) * pagecount).Take(pagecount).ToList(),
                    ToatalAmount = (decimal)TotalAmount,

                };

                return paginationListData1;
            }
            else
            {
                query = _context.VwListPOSReturnHeads.AsQueryable();
            }

            // Apply sorting based on sort and order parameters

            Expression<Func<VwListPOSReturnHeads, object>> defaultSort = x => x.Id;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => !d.IsDelete).ToListAsync();

            filteredData = QueryHelper.SearchUtility<VwListPOSReturnHeads>.Search(filteredData, null, c => c.InvoiceDate.ToString(), c => c.Description, c => c.Type, c => c.Id.ToString(),
                 c => c.Gross.ToString(), c => c.Total.ToString(), c => c.RefInv);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewPOSReturnHead
                {
                    Delete_By = a.Delete_By,
                    Delete_Date = a.Delete_Date,
                    Edit_By = a.Edit_By,
                    Edit_Date = a.Edit_Date,
                    Id = a.Id,
                    Created = a.Created,
                    Date = a.InvoiceDate.ToString("yyyy-MM-dd"),
                    Customer = a.Customer,
                    Discount = a.Discount,
                    Gross = a.Gross,
                    FKClientId = a.FKClientId,
                    LocationId = a.LocationId,
                    RefInv = a.RefInv,
                    Total = a.Total,
                    Type = a.Type,
                    IsDelete = a.IsDelete,
                    Description = a.Description,
                }).Where(d => d.FKClientId.Equals(CustomerId) && d.LocationId.Equals(locationId))
                .ToList();

            // Prepare the pagination response
            var TotalAmount1 = pagedData.Sum(x => x.Gross);
            var paginationListData = new PaginationViewPOSReturnHead
            {
                Count = pagedData.Count,
                ToatalAmount = (decimal)TotalAmount1,
                ViewPOSReturnHeads = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList()
            };

            // Cache the result

            var CacheResult = new PaginationViewPOSReturnHead
            {
                Count = filteredData.Count,
                IQueryData = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;
        }

        public async Task<Message<string>> RegisterPOSReturnHeadAsync(InsertPOSReturnHead model)
        {

            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var tblPOSHead = new TblPOSReturnHead
                {
                    Type = model.Type,
                    Edit_By = model.Edit_By,
                    Edit_Date = model.Edit_Date,
                    Date = CommonResources.LocalDatetime(),
                    RefInv = model.RefInv,
                    Created = model.Created,
                    Discount = model.Discount,
                    FKClientId = model.FKClientId,
                    Gross = model.Gross,
                    Description = model.Description,
                    Total = model.Total,
                    POSInvoiceNO = model.RefInv,
                    FKLocationId = model.LocationId
                };
                _context.TblPOSReturnHeads.Add(tblPOSHead);
                await _context.SaveChangesAsync();


                var dataItem = _context.TblPOSReturnBodyTemps.Where(i => i.UserID == model.Created && i.RefInv.Equals(model.RefInv)).ToList();

                #region Accounting

                //TblSupplierPayment tblSupplierPayment = new()
                //{
                //    FKSupplierID = getsupplierID.ID,
                //    POSNo = obj.Grid,
                //    Balance = 0,
                //    Date = obj.Date,
                //    Pay = 0,
                //    Ref_invoive = obj.RefInv,
                //    Total = (double)obj.Total,
                //};
                //con.TblSupplierPayments.Add(tblSupplierPayment);
                //con.SaveChanges();

                ////GL Transaction One Suupler Credit Ledger
                //TblGL tblGL = new()
                //{
                //    Date = obj.Date,
                //    Acc = obj.Supplier + "  ledger",
                //    AccCode = getsupplierID.LedgerCode,
                //    Description = "Good Received Note Invoice :" + obj.RefInv + " | Acc:",
                //    Reference = obj.RefInv,
                //    ReferenceType = "Good Received Note Invoice",
                //    Cr = obj.Total - obj.Discount,
                //    Dr = 0.00,


                //};
                //con.TblGLs.Add(tblGL);
                //con.SaveChanges();

                //if (obj.Discount > 0)
                //{
                //    var GetDiscountLedgerCode = con.TblAutomationSystemLedgerCode.FirstOrDefault(x => x.StockTable.Equals("Received Discount"));

                //    TblGL tblGLdis = new()
                //    {
                //        Date = obj.Date,
                //        Acc = GetDiscountLedgerCode.Acc + "  ledger",
                //        AccCode = GetDiscountLedgerCode.Code,
                //        Description = "Good Received Note Invoice Discount :" + obj.RefInv + " | Acc:",
                //        Reference = obj.RefInv,
                //        ReferenceType = "Good Received Note Invoice Discount",
                //        Cr = obj.Discount,
                //        Dr = 0.00,
                //    };
                //    con.TblGLs.Add(tblGLdis);
                //}




                //GL Transaction One Stock Debit Ledger

                #endregion

                foreach (var s in dataItem)
                {
                    TblPOSReturnBody tblPOSReturnBody = new()
                    {
                        ItemID = s.ItemID,
                        Code = s.Code,
                        POSReturnNO = tblPOSHead.Id,
                        POSInvoiceNO = tblPOSHead.RefInv,
                        Cost = s.Cost,
                        Qty = s.Qty + s.FreeQty,
                        UnitCost = s.UnitCost,
                        ExpDate = s.ExpDate,
                        DisCount = s.Discount ?? 0,
                        FreeQty = s.FreeQty,
                        Price = s.Sellingprice,
                        POSBodyKeyNo = s.POSBodyKeyNo,
                        FKLocationId = model.LocationId,
                        Qtypiece = s.Qtypiece,
                        UnitSize = s.UnitSize,
                        UnitName = s.UnitName,

                    };
                    _context.TblPOSReturnBodies.Add(tblPOSReturnBody);

                    _context.TblPOSReturnBodyTemps.Remove(s);
                    _context.SaveChanges();

                }

                // Log audit trail
                _auditTrailService.LogAudit("TblPOSReturnHeads", "Insert", $"Insert {model.Id + ' '} ID.", model.Edit_By);

                transactionscope.Commit();
                InvalidateCache(nameof(PaginationViewPOSReturnHead));
                InvalidateCache(nameof(PaginationViewPOSInvoiceItemData));

                return new Message<string>()
                {
                    Code = Convert.ToString(tblPOSHead.Id),
                    Text = $"Customer Return Invoice has been registered",
                };


            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }

        public async Task<Message<string>> CheckInvoiceHaveaPOS(InsertPOSReturnHead model)
        {

            try
            {
                string input = model.RefInv;
                string result = input.Replace("INV000", "");

                var values = await _context.TblPOSHeads.SingleOrDefaultAsync(d => d.IsDelete.Equals(false) && d.Id.Equals(Convert.ToInt32(result)) && d.FKClientId.Equals(model.FKClientId));

                if (values != null)
                {

                    var allRows = _context.TblPOSReturnBodyTemps.Where(d => d.UserID.Equals(model.Edit_By) && d.RefInv.Equals(Convert.ToInt32(result))).ToList();

                    _context.TblPOSReturnBodyTemps.RemoveRange(allRows);
                    _context.SaveChanges();

                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This Customer has the invoice",
                    };
                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This Customer does not have the invoice",
                    };
                }

            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }


        public TblPOSReturnHead GetByPOSReturnHeadId(int id)
        {
            return _context.TblPOSReturnHeads.SingleOrDefault(d => d.Id.Equals(id));
        }

        public ViewPOSReturnHead GetDetailsByPRNHeadIdForPrint(int id)
        {
            try
            {
                var dt = (from x in _context.VwListPOSReturnHeads
                          join u in _context.Users on x.Created equals u.Id
                          where x.Id == id

                          select new ViewPOSReturnHead()
                          {
                              Created = u.FirstName,
                              Date = x.InvoiceDate.ToString("yyyy-MM-dd"),
                              Customer = x.Customer,
                              Discount = x.Discount,
                              Gross = x.Gross,
                              Id = x.Id,
                              RefInv = x.RefInv,
                              Total = x.Total,
                              Type = x.Type,
                              LocationId = x.LocationId,
                              IsDelete = x.IsDelete,
                          }).SingleOrDefault();
                return dt;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<ViewTempPOSReturnBody> GetByPOSReturnBodiesId(int id)
        {
            var dt = (from a in _context.TblPOSReturnBodies
                      join i in _context.TblStock_Mains on a.ItemID equals i.ID
                      where a.POSReturnNO.Equals(id)
                      select new ViewTempPOSReturnBody
                      {
                          Code = a.Code,
                          Cost = a.Cost,
                          Discount = a.DisCount,
                          FreeQty = a.FreeQty,
                          POSBodyNo = (int)a.POSReturnNO,
                          ItemID = a.ItemID,
                          Qty = a.Qty,
                          Id = a.Id,
                          ItemName = i.ItemName,
                          Sellingprice = a.Price,
                          UnitCost = a.UnitCost,
                          Amount = a.UnitCost,
                          UnitName = a.UnitName,
                          UnitSize = a.UnitSize,
                          Qtypiece = a.Qtypiece,

                      }).ToList();

            return dt;
        }


        public async Task<Message<string>> DeletePOSReturnHeadAsync(DeletePOSReturnHead model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existClient = GetByPOSReturnHeadId(model.Id);

                if (existClient.IsDelete == false)
                {
                    existClient.IsDelete = true;
                    existClient.Delete_By = model.Delete_By;
                    existClient.Delete_Date = model.Delete_Date;

                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblPOSReturnHeads", "Delete", $"Delete this {model.Id} name.", model.Edit_By);

                    await _context.SaveChangesAsync();

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewPOSReturnHead));
                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"Customer Return Invoice has been already deleted",
                    };
                }

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Customer Return Invoice have been deleted successfully"
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Delete error {ex.Message}" };
            }
        }


        #endregion

        #region POSReturn Temp Data Load
        public async Task<PaginationViewPOSReturnBody> GetAllPOSReturnTempBodyData(int page, int pagecount, string searchTerm, string sort, string order, string userid)
        {

            var query = (from a in _context.TblPOSReturnBodyTemps
                         join s in _context.TblStock_Mains on a.ItemID equals s.ID
                         //where (string.IsNullOrEmpty(searchTerm) || a.Code.Contains(searchTerm))
                         select new ViewTempPOSReturnBody()
                         {
                             Code = a.Code,
                             Cost = a.Cost,
                             Discount = a.Discount,
                             FreeQty = a.FreeQty,
                             ItemID = a.ItemID,
                             Qty = a.Qty,
                             ItemName = s.ItemName,
                             Id = a.Id,
                             Sellingprice = a.Sellingprice,
                             UnitCost = a.UnitCost,
                             UserID = a.UserID,
                             Amount = a.Amount,
                             RefInv = a.RefInv,
                             UnitName = a.UnitName,
                             UnitSize = a.UnitSize,
                             Qtypiece = a.Qtypiece,
                         }).AsQueryable();

            // Apply sorting based on sort and order parameters
            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
            {
                // Determine the property to sort by dynamically
                var propertyInfo = typeof(ViewTempPOSReturnBody).GetProperty(sort, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    // Ensure that propertyInfo.GetValue(a, null) is not null
                    // You can use Expression to dynamically generate the lambda expression
                    var parameter = Expression.Parameter(typeof(ViewTempPOSReturnBody), "a");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda<Func<ViewTempPOSReturnBody, object>>(Expression.Convert(property, typeof(object)), parameter);

                    // Apply sorting
                    query = order.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
                        ? query.OrderBy(lambda)
                        : query.OrderByDescending(lambda);
                }
            }
            else
            {
                query = query.OrderByDescending(d => d.Id);
            }


            // Filter out deleted records and fetch the data
            var filteredData = await query.Where(d => d.UserID.Equals(userid)).ToListAsync();



            // Apply searchTerm filtering client-side
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower(); // Convert searchTerm to lowercase for case-insensitive search

                filteredData = filteredData
                    .Where(a =>
                    a.Code.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            }
            // Project the filtered data to VWListClient
            var pagedData = filteredData
                .Select(a => new ViewTempPOSReturnBody
                {
                    Code = a.Code,
                    Cost = a.Cost,
                    Discount = a.Discount,
                    FreeQty = a.FreeQty,
                    ItemID = a.ItemID,
                    Qty = a.Qty,
                    ItemName = a.ItemName,
                    Id = a.Id,
                    Sellingprice = a.Sellingprice,
                    UnitCost = a.UnitCost,
                    UserID = a.UserID,
                    Amount = a.Amount,
                    RefInv = a.RefInv,
                    UnitName = a.UnitName,
                    UnitSize = a.UnitSize,
                    Qtypiece = a.Qtypiece,
                })
                .ToList();

            // Prepare the pagination response


            var TotalAmount = filteredData.Sum(x => x.Cost);
            var TotalDiscount = filteredData.Sum(x => x.Discount);
            var TotalGross = filteredData.Sum(x => x.Amount);
            var paginationListDatas = new PaginationViewPOSReturnBody
            {
                Count = filteredData.Count,
                ViewTempPOSReturnBodies = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList(),
                ToatalAmount = (decimal)TotalAmount,
                ToatalDiscount = (decimal)TotalDiscount,
                ToatalGross = (decimal)TotalGross,
            };

            // Cache the result with a sliding expiration of 5 minutes (adjust as needed)
            // _cache.Set(cacheKey, dt, TimeSpan.FromMinutes(1000));

            return paginationListDatas;
        }


        public async Task<PaginationViewPOSInvoiceItemData> GetAllPOSInvoiceItems1(int page, int pagecount, string searchTerm, string sort, string order, string invoiceNo = null, int locationId = 0)
        {
            var cacheKey = $"{nameof(PaginationViewPOSInvoiceItemData)}";
            // Check if the result is already in the cache

            IQueryable<ViewAllPOsInvoiceItem> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewPOSInvoiceItemData cachedResult))
            {

                query = cachedResult.ViewAllPOsInvoiceItems.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<ViewAllPOsInvoiceItem, object>> defaultSort1 = x => x.LocationId;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<ViewAllPOsInvoiceItem>.Search([.. query], searchTerm, c => c.Code.ToString(), c => c.ItemName);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewAllPOsInvoiceItem
                       {

                           Price = a.Price,
                           DisCount = a.DisCount,
                           BodyId = a.BodyId,
                           Code = a.Code,
                           Cost = a.Cost,
                           FreeQty = a.FreeQty,
                           INVNo = a.INVNo,
                           ItemID = a.ItemID,
                           Qty = a.Qty,
                           UnitCost = a.UnitCost,
                           UserName = a.UserName,
                           ItemName = a.ItemName,
                           ReturnQTY = a.ReturnQTY,
                           TotalQty = a.TotalQty,
                           UnitName = a.UnitName,
                           UnitSize = a.UnitSize,
                           Qtypiece = a.Qty / Convert.ToDecimal(a.UnitSize)
                       }).Where(d => d.INVNo.Equals(invoiceNo))
                       .ToList();

                // Prepare the pagination response
                var paginationListData1 = new PaginationViewPOSInvoiceItemData
                {
                    Count = pagedData1.Count,
                    ViewAllPOsInvoiceItems = pagedData1.Skip((page - 1) * pagecount).Take(pagecount).ToList(),
                };

                return paginationListData1;
            }
            else
            {
                query = (from a in _context.ViewAllPOsInvoiceItems
                         select new ViewAllPOsInvoiceItem()
                         {
                             Price = a.Price,
                             DisCount = a.DisCount,
                             BodyId = a.BodyId,
                             Code = a.Code,
                             Cost = a.Cost,
                             FreeQty = a.FreeQty,
                             INVNo = a.INVNo,
                             ItemID = a.ItemID,
                             Qty = a.Qty,
                             UnitCost = a.UnitCost,
                             UserName = a.UserName,
                             ItemName = a.ItemName,
                             ReturnQTY = a.ReturnQTY,
                             TotalQty = a.TotalQty,
                             UnitName = a.UnitName,
                             UnitSize = a.UnitSize,
                             Qtypiece = a.Qty / Convert.ToDecimal(a.UnitSize),
                             LocationId = a.LocationId
                         }).AsNoTracking().AsQueryable();
            }

            // Apply sorting based on sort and order parameters

            Expression<Func<ViewAllPOsInvoiceItem, object>> defaultSort = x => x.LocationId;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.ToListAsync();

            filteredData = QueryHelper.SearchUtility<ViewAllPOsInvoiceItem>.Search(filteredData, null, null);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewAllPOsInvoiceItem
                {
                    Price = a.Price,
                    DisCount = a.DisCount,
                    BodyId = a.BodyId,
                    Code = a.Code,
                    Cost = a.Cost,
                    FreeQty = a.FreeQty,
                    INVNo = a.INVNo,
                    ItemID = a.ItemID,
                    Qty = a.Qty,
                    UnitCost = a.UnitCost,
                    UserName = a.UserName,
                    ItemName = a.ItemName,
                    ReturnQTY = a.ReturnQTY,
                    TotalQty = a.TotalQty,
                    UnitName = a.UnitName,
                    UnitSize = a.UnitSize,
                    LocationId = a.LocationId,
                    Qtypiece = a.Qty / Convert.ToDecimal(a.UnitSize)
                }).Where(d => d.INVNo.Equals(invoiceNo) && d.LocationId.Equals(locationId))
                .ToList();

            // Prepare the pagination response
            var paginationListData = new PaginationViewPOSInvoiceItemData
            {
                Count = pagedData.Count,
                ViewAllPOsInvoiceItems = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList()
            };

            // Cache the result

            var CacheResult = new PaginationViewPOSInvoiceItemData
            {
                Count = filteredData.Count,
                ViewAllPOsInvoiceItems = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;

        }

        public async Task<PaginationViewPOSInvoiceItemData> GetAllPOSInvoiceItems(
          int page, int pagecount, string searchTerm, string sort, string order, string invoiceNo = null, int locationId = 0)
        {

            var cacheKey = $"{nameof(PaginationViewPOSInvoiceItemData)}";
            // Check if the result is already in the cache

            IQueryable<ViewAllPOsInvoiceItem> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewPOSInvoiceItemData cachedResult))
            {
                return GetPaginatedResult(cachedResult.ViewAllPOsInvoiceItems.AsQueryable(), page, pagecount, searchTerm, sort, order, invoiceNo, locationId);
            }
            else
            {
                query = _context.ViewAllPOsInvoiceItems.AsQueryable();



                var CacheResult = new PaginationViewPOSInvoiceItemData
                {
                    Count = query.ToList().Count,
                    ViewAllPOsInvoiceItems = [.. query]
                };
                _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed
                return GetPaginatedResult(query, page, pagecount, searchTerm, sort, order, invoiceNo, locationId);
            }
        }

        private static PaginationViewPOSInvoiceItemData GetPaginatedResult(
            IQueryable<ViewAllPOsInvoiceItem> query, int page, int pagecount, string searchTerm, string sort, string order, string invoiceNo = null, int locationId = 0)
        {
            Expression<Func<ViewAllPOsInvoiceItem, object>> defaultSort = x => x.ItemID;
            query = QueryHelper.ApplySort(query, sort, order, defaultSort);


            var filteredData = query
                .Select(a => new ViewAllPOsInvoiceItem
                {
                    Price = a.Price,
                    DisCount = a.DisCount,
                    BodyId = a.BodyId,
                    Code = a.Code,
                    Cost = a.Cost,
                    FreeQty = a.FreeQty,
                    INVNo = a.INVNo,
                    ItemID = a.ItemID,
                    Qty = a.Qty,
                    UnitCost = a.UnitCost,
                    UserName = a.UserName,
                    ItemName = a.ItemName,
                    ReturnQTY = a.ReturnQTY,
                    TotalQty = a.TotalQty,
                    UnitName = a.UnitName,
                    UnitSize = a.UnitSize,
                    LocationId = a.LocationId,
                    Qtypiece = a.Qty / Convert.ToDecimal(a.UnitSize)
                }).Where(d => d.INVNo.Equals(invoiceNo) && d.LocationId.Equals(locationId))
                .ToList();

            filteredData = QueryHelper.SearchUtility<ViewAllPOsInvoiceItem>.Search([.. filteredData], searchTerm, null).ToList();



            List<ViewAllPOsInvoiceItem> pagedData;
            if (page > 0 && pagecount > 0)
            {
                pagedData = filteredData
                    .Skip((page - 1) * pagecount)
                    .Take(pagecount)
                    .ToList();
            }
            else
            {
                pagedData = filteredData;
            }


            return new PaginationViewPOSInvoiceItemData
            {
                Count = query.Count(),
                ViewAllPOsInvoiceItems = pagedData
            };
        }




        public TblPOSReturnBodyTemp GetByName(string Name, string userid)
        {
            return _context.TblPOSReturnBodyTemps.SingleOrDefault(d => d.ItemName.Equals(Name) && d.UserID.Equals(userid) /*&& d.POSNo.Equals(POSNo)*/);
        }
        public async Task<Message<string>> RegisterTempBodyAsync(InsertTempPOSReturnBody model)
        {

            try
            {
                var ChecktheisthereExsitingCustomerName = GetByName(model.ItemName, model.UserID);

                if (ChecktheisthereExsitingCustomerName == null)
                {
                    // DateTime dateTime = DateTime.ParseExact(model.ExpDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    var transactionscope = _context.Database.BeginTransaction();
                    //if (DateTime.TryParse(model.ExpDate, out DateTime expDate))
                    //{
                    //    DateTime date = expDate;
                    //}
                    var temp = new TblPOSReturnBodyTemp
                    {
                        FreeQty = model.FreeQty ?? 0,
                        Code = model.Code,
                        Discount = model.Discount,
                        Cost = model.Cost,
                        ItemID = model.ItemID,
                        Qty = model.Qty,
                        Sellingprice = model.Sellingprice,
                        UnitCost = model.UnitCost,
                        UserID = model.UserID,
                        Amount = model.Amount,
                        ItemName = model.ItemName,
                        RefInv = model.RefInv,
                        POSBodyKeyNo = model.POSBodyNo,
                        UnitSize = model.UnitSize,
                        Qtypiece = model.Qtypiece,
                        UnitName = model.UnitName,

                    };

                    _context.TblPOSReturnBodyTemps.Add(temp);
                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblPOSReturnBodyTemps", "Insert", $"Insert {model.Code + ' '} Code.", model.UserID);

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewPOSReturnBody));

                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This Item has been Add to Cart",
                    };
                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This Item has been already Add to Cart"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }


        public TblPOSReturnBodyTemp GetTempBodyById(int id)
        {
            return _context.TblPOSReturnBodyTemps.SingleOrDefault(d => d.Id.Equals(id));
        }


        public async Task<Message<string>> UpdateTempBodyAsync(UpdateTempPOSReturnBody model)
        {
            try
            {

                var transactionscope = _context.Database.BeginTransaction();

                var exist = GetTempBodyById(model.Id);

                exist.FreeQty = model.FreeQty;
                exist.Code = model.Code;
                exist.Discount = model.Discount;
                exist.Cost = model.Cost;
                exist.ItemID = model.ItemID;
                exist.Qty = model.Qty;
                exist.Sellingprice = model.Sellingprice;
                exist.UnitCost = model.UnitCost;
                exist.UserID = model.UserID;
                exist.Amount = model.Amount;
                exist.UnitName = model.UnitName;
                exist.UnitSize = model.UnitSize;
                exist.Qtypiece = model.Qtypiece;


                await _context.SaveChangesAsync();


                // Log audit trail
                _auditTrailService.LogAudit("TblPOSReturnBodyTemps", "Update", $"Update {model.Code + ' '} Code.", model.UserID);

                transactionscope.Commit();
                InvalidateCache(nameof(PaginationViewPOSReturnBody));

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Item has been updated successfully",
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Update error {ex.Message}" };
            }


        }

        public async Task<Message<string>> DeleteTempBodyAsync(DeleteTempPOSReturnBody model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existClient = GetTempBodyById(model.Id);

                _context.Remove(existClient);
                await _context.SaveChangesAsync();

                // Log audit trail
                _auditTrailService.LogAudit("TblPOSReturnBodyTemps", "Delete", $"Delete this {model.Code + ' ' + model.Id} name.", model.UserID);

                await _context.SaveChangesAsync();

                transactionscope.Commit();

                InvalidateCache(nameof(PaginationViewPOSReturnBody));

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Item have been deleted successfully"
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Delete error {ex.Message}" };
            }
        }
        public async Task<ViewTempPOSReturnBody> GetDetailsTempBodyById(int id)
        {
            return await (from a in _context.TblPOSReturnBodyTemps
                          join s in _context.TblStock_Mains on a.ItemID equals s.ID
                          select new ViewTempPOSReturnBody()
                          {
                              Code = a.Code,
                              Cost = a.Cost,
                              Discount = a.Discount,
                              FreeQty = a.FreeQty,
                              ItemID = a.ItemID,
                              Qty = a.Qty,
                              Id = a.Id,
                              ItemName = s.ItemName,
                              Sellingprice = a.Sellingprice,
                              UnitCost = a.UnitCost,
                              Amount = a.Amount,
                              UserID = a.UserID,
                              UnitSize = a.UnitSize,
                              RefInv = a.RefInv,
                              Qtypiece = a.Qtypiece,
                              UnitName = a.UnitName,

                          }).SingleOrDefaultAsync(d => d.Id.Equals(id));

        }



        #endregion


        private void InvalidateCache(string methodName)
        {
            _cache.Remove(methodName); // Invalidate cache for the specified method
        }




    }
}
