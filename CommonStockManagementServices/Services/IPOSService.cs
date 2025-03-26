using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Model;
using System.Linq.Expressions;
using CommonStockManagementDatabase.Context;

namespace CommonStockManagementServices.Services
{


    public class IPOSService(AppDbContext context, AuditTrailService auditTrailService, IMemoryCache cache)
    {
        private readonly AppDbContext _context = context;
        private readonly AuditTrailService _auditTrailService = auditTrailService;
        private readonly IMemoryCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));


        private void InvalidateCache(string methodName)
        {
            _cache.Remove(methodName); // Invalidate cache for the specified method
        }


        #region Point Of Sales Note 
        public async Task<PaginationViewPOSHead> GetAllPaginationPOSHead(int page, int pagecount, string searchTerm, string sort, string order, int CustomerId, int locationId)
        {

            var cacheKey = $"{nameof(PaginationViewPOSHead)}";
            // Check if the result is already in the cache

            IQueryable<VwListPOSHeads> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewPOSHead cachedResult))
            {

                query = cachedResult.IQueryData.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<VwListPOSHeads, object>> defaultSort1 = x => x.Id;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<VwListPOSHeads>.Search([.. query], searchTerm, c => c.InvoiceDate.ToString(), c => c.Description, c => c.Type, c => c.Id.ToString(),
                c => c.Gross.ToString(), c => c.Total.ToString(), c => c.RefInv);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(x => new ViewPOSHead
                       {
                           Created = x.Created,
                           Date = x.InvoiceDate.ToString("yyyy-MM-dd"),
                           Customer = x.Customer,
                           Discount = x.Discount,
                           Gross = x.Gross,
                           Id = x.Id,
                           RefInv = x.RefInv,
                           Total = x.Total,
                           Type = x.Type,
                           IsDelete = x.IsDelete,
                           FKClientId = x.FKClientId,
                           Description = x.Description,
                           LocationId = x.LocationId
                       }).Where(d => d.FKClientId.Equals(CustomerId) && d.LocationId.Equals(locationId))
                       .ToList();

                // Prepare the pagination response
                var TotalAmount = pagedData1.Sum(x => x.Total);
                var TotalDiscount = pagedData1.Sum(x => x.Discount);
                var TotalGross = pagedData1.Sum(x => x.Gross);

                var paginationListData = new PaginationViewPOSHead
                {
                    Count = pagedData1.Count,
                    ViewPOSHeads = pagedData1.Skip((page - 1) * pagecount).Take(pagecount).ToList(),
                    ToatalAmount = (decimal)TotalAmount,
                    ToatalGross = (decimal)TotalGross,
                    ToatalDiscount = (decimal)TotalDiscount,
                };

                return paginationListData;
            }
            else
            {
                query = _context.VwListPOSHeads.AsQueryable();

            }

            // Apply sorting based on sort and order parameters

            Expression<Func<VwListPOSHeads, object>> defaultSort = x => x.Id;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => !d.IsDelete).ToListAsync();

            filteredData = QueryHelper.SearchUtility<VwListPOSHeads>.Search(filteredData, null, c => c.InvoiceDate.ToString(), c => c.Description, c => c.Type, c => c.Id.ToString(),
                c => c.Gross.ToString(), c => c.Total.ToString(), c => c.RefInv);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(x => new ViewPOSHead
                {
                    LocationId = x.LocationId,
                    Created = x.Created,
                    Date = x.InvoiceDate.ToString("yyyy-MM-dd"),
                    Customer = x.Customer,
                    Discount = x.Discount,
                    Gross = x.Gross,
                    Id = x.Id,
                    RefInv = x.RefInv,
                    Total = x.Total,
                    Type = x.Type,
                    IsDelete = x.IsDelete,
                    FKClientId = x.FKClientId,
                    Description = x.Description,
                }).Where(d => d.FKClientId.Equals(CustomerId) && d.LocationId.Equals(locationId))
                .ToList();

            // Prepare the pagination response

            var TotalAmount1 = pagedData.Sum(x => x.Total);
            var TotalDiscount1 = pagedData.Sum(x => x.Discount);
            var TotalGross1 = pagedData.Sum(x => x.Gross);

            var paginationListData1 = new PaginationViewPOSHead
            {
                Count = pagedData.Count,
                ViewPOSHeads = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList(),
                ToatalAmount = (decimal)TotalAmount1,
                ToatalGross = (decimal)TotalGross1,
                ToatalDiscount = (decimal)TotalDiscount1,
            };

            // Cache the result

            var CacheResult = new PaginationViewPOSHead
            {
                Count = filteredData.Count,
                IQueryData = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData1;

        }

        public async Task<Message<string>> RegisterPOSHeadAsync(InsertPOSHead model)
        {

            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var tblPOSHead = new TblPOSHead
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
                    FKLocationId = model.LocationId
                };
                _context.TblPOSHeads.Add(tblPOSHead);
                await _context.SaveChangesAsync();


                var dataItem = _context.TblPOSBodyTemps.Where(i => i.UserID == model.Created).ToList();

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
                    TblPOSBody tblPOSBody = new()
                    {
                        ItemID = s.ItemID,
                        Code = s.Code,
                        POSNO = tblPOSHead.Id,
                        Cost = s.Cost,
                        UnitCost = s.UnitCost,
                        ExpDate = s.ExpDate,
                        DisCount = s.Discount ?? 0,
                        FreeQty = s.FreeQty,
                        Price = s.Sellingprice,
                        FKLocationId = model.LocationId,
                        UnitSize = s.UnitSize,
                        Qtypiece = s.Qtypiece,
                        UnitName = s.UnitName,
                        Qty = s.Qty
                    };
                    _context.TblPOSBodies.Add(tblPOSBody);
                    _context.SaveChanges();





                    _context.TblPOSBodyTemps.Remove(s);
                    _context.SaveChanges();

                }

                // Log audit trail
                _auditTrailService.LogAudit("TblPOSHeads", "Insert", $"Insert {model.Id + ' '} ID.", model.Edit_By);

                transactionscope.Commit();
                InvalidateCache(nameof(PaginationViewPOSHead));
                InvalidateCache(nameof(PaginationViewPOSBody));
                InvalidateCache(nameof(PaginationViewStockMain.IQueryData1));
                InvalidateCache(nameof(PaginationViewStockMain.IQueryData));
                return new Message<string>()
                {
                    Code = Convert.ToString(tblPOSHead.Id),
                    Text = $"This Invoice has been registered",
                };


            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }

        public TblPOSHead GetByPOSHeadId(int id)
        {
            return _context.TblPOSHeads.SingleOrDefault(d => d.Id.Equals(id));
        }

        public ViewPOSHead GetDetailsByPOSHeadIdForPrint(int id)
        {
            try
            {
                var dt = (from x in _context.VwListPOSHeads
                          join u in _context.Users on x.Created equals u.Id
                          where x.Id == id

                          select new ViewPOSHead()
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
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<ViewTempPOSBody> GetByPOSBodiesId(int id)
        {
            var dt = (from a in _context.TblPOSBodies
                      join i in _context.TblStock_Mains on a.ItemID equals i.ID
                      where a.POSNO.Equals(id)
                      select new ViewTempPOSBody
                      {
                          Code = a.Code,
                          Cost = a.Cost,
                          Discount = a.DisCount,
                          FreeQty = a.FreeQty,
                          POSBodyNo = (int)a.POSNO,
                          ItemID = a.ItemID,
                          Qty = a.Qty,
                          Id = a.Id,
                          ItemName = i.ItemName,
                          Sellingprice = a.Price,
                          UnitCost = a.Price,
                          Amount = a.UnitCost,
                          UnitName = a.UnitName,
                          Qtypiece = a.Qtypiece,
                          UnitSize = a.UnitSize,

                      }).ToList();

            return dt;
        }


        public async Task<Message<string>> DeletePOSHeadAsync(DeletePOSHead model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existClient = GetByPOSHeadId(model.Id);

                if (existClient.IsDelete == false)
                {
                    existClient.IsDelete = true;
                    existClient.Delete_By = model.Delete_By;
                    existClient.Delete_Date = model.Delete_Date;

                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblPOSHeads", "Delete", $"Delete this {model.Id} name.", model.Edit_By);

                    await _context.SaveChangesAsync();

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewPOSHead));
                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"Good Received Note has been already deleted",
                    };
                }

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Good Received Note details have been deleted successfully"
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Delete error {ex.Message}" };
            }
        }

        public void ClearToBin(string Userid)
        {
            try
            {
                InvalidateCache(nameof(PaginationViewPOSBody));
                var allRows = _context.TblPOSBodyTemps.Where(d => d.UserID.Equals(Userid)).ToList();

                foreach (var row in allRows)
                {
                    _context.TblPOSBodyTemps.Remove(row);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }




        #endregion

        #region POS Temp Data Load
        public async Task<PaginationViewPOSBody> GetAllPOSTempBodyData(int page, int pagecount, string searchTerm, string sort, string order, string userid)
        {

            var cacheKey = $"{nameof(PaginationViewPOSBody)}";
            // Check if the result is already in the cache

            IQueryable<ViewTempPOSBody> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewPOSBody cachedResult))
            {

                query = cachedResult.ViewTempPOSBodies.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<ViewTempPOSBody, object>> defaultSort1 = x => x.Id;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<ViewTempPOSBody>.Search([.. query], searchTerm, null);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewTempPOSBody
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
                           UnitSize = a.UnitSize,
                           Qtypiece = a.Qtypiece,
                           UnitName = a.UnitName,
                       })
                       .ToList();

                // Prepare the pagination response
                var TotalAmount1 = filteredData1.Sum(x => x.Cost);
                var TotalDiscount1 = filteredData1.Sum(x => x.Discount);
                var TotalGross1 = filteredData1.Sum(x => x.Amount);

                var paginationListData1 = new PaginationViewPOSBody
                {
                    Count = filteredData1.Count,
                    ViewTempPOSBodies = pagedData1.Skip((page - 1) * pagecount).Take(pagecount).ToList(),
                    ToatalDiscount = TotalDiscount1,
                    ToatalAmount = TotalAmount1,
                    ToatalGross = TotalGross1,
                };

                return paginationListData1;
            }
            else
            {
                query = (from a in _context.TblPOSBodyTemps
                         join s in _context.TblStock_Mains on a.ItemID equals s.ID
                         //where (string.IsNullOrEmpty(searchTerm) || a.Code.Contains(searchTerm))
                         select new ViewTempPOSBody()
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
                             UnitSize = a.UnitSize,
                             Qtypiece = a.Qtypiece,
                             UnitName = a.UnitName,
                         }).AsQueryable();

            }

            // Apply sorting based on sort and order parameters

            Expression<Func<ViewTempPOSBody, object>> defaultSort = x => x.Id;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => d.UserID.Equals(userid)).ToListAsync();

            filteredData = QueryHelper.SearchUtility<ViewTempPOSBody>.Search(filteredData, null, null);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewTempPOSBody
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
                    UnitSize = a.UnitSize,
                    Qtypiece = a.Qtypiece,
                    UnitName = a.UnitName,
                })
                .ToList();

            // Prepare the pagination response
            var TotalAmount = filteredData.Sum(x => x.Cost);
            var TotalDiscount = filteredData.Sum(x => x.Discount);
            var TotalGross = filteredData.Sum(x => x.Amount);

            var paginationListData = new PaginationViewPOSBody
            {
                Count = filteredData.Count,
                ViewTempPOSBodies = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList(),
                ToatalDiscount = TotalDiscount,
                ToatalAmount = TotalAmount,
                ToatalGross = TotalGross,
            };

            // Cache the result
            var CacheResult = new PaginationViewPOSBody
            {
                Count = filteredData.Count,
                ViewTempPOSBodies = pagedData

            };

            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;

        }

        public async Task<Message<string>> RegisterTempBodyAsync(InsertTempPOSBody model)
        {

            try
            {
                var ChecktheisthereExsitingCustomerName = GetByName(model.ItemName, model.UserID, 0);

                if (ChecktheisthereExsitingCustomerName == null)
                {
                    // DateTime dateTime = DateTime.ParseExact(model.ExpDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    var transactionscope = _context.Database.BeginTransaction();
                    //if (DateTime.TryParse(model.ExpDate, out DateTime expDate))
                    //{
                    //    DateTime date = expDate;
                    //}
                    var temp = new TblPOSBodyTemp
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
                        Qtypiece = model.Qtypiece,
                        UnitName = model.UnitName,
                        UnitSize = model.UnitSize,
                    };

                    _context.TblPOSBodyTemps.Add(temp);
                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblPOSBodyTemps", "Insert", $"Insert {model.Code + ' '} Code.", model.UserID);

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewPOSBody));

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

        public TblPOSBodyTemp GetTempBodyById(int id)
        {
            return _context.TblPOSBodyTemps.SingleOrDefault(d => d.Id.Equals(id));
        }

        public TblPOSBodyTemp GetByName(string Name, string userid, int POSNo)
        {
            return _context.TblPOSBodyTemps.SingleOrDefault(d => d.ItemName.Equals(Name) && d.UserID.Equals(userid) /*&& d.POSNo.Equals(POSNo)*/);
        }

        public async Task<Message<string>> UpdateTempBodyAsync(UpdateTempPOSBody model)
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
                _auditTrailService.LogAudit("TblPOSBodyTemps", "Update", $"Update {model.Code + ' '} Code.", model.UserID);

                transactionscope.Commit();
                InvalidateCache(nameof(PaginationViewPOSBody));

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

        public async Task<Message<string>> DeleteTempBodyAsync(DeleteTempPOSBody model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existClient = GetTempBodyById(model.Id);

                _context.Remove(existClient);
                await _context.SaveChangesAsync();

                // Log audit trail
                _auditTrailService.LogAudit("TblPOSBodyTemps", "Delete", $"Delete this {model.Code + ' ' + model.Id} name.", model.UserID);

                await _context.SaveChangesAsync();

                transactionscope.Commit();

                InvalidateCache(nameof(PaginationViewPOSBody));
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
        public async Task<ViewTempPOSBody> GetDetailsTempBodyById(int id)
        {
            return await (from a in _context.TblPOSBodyTemps
                          join s in _context.TblStock_Mains on a.ItemID equals s.ID
                          select new ViewTempPOSBody()
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
                              Qtypiece = a.Qtypiece,
                              UnitName = a.UnitName,
                          }).SingleOrDefaultAsync(d => d.Id.Equals(id));

        }




        #endregion



    }
}
