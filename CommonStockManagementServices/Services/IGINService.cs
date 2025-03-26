using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Model;
using System.Linq.Expressions;

namespace CommonStockManagementServices.Services
{


    public class GINService(AppDbContext context, AuditTrailService auditTrailService, CacheService cache)
    {
        private readonly AppDbContext _context = context;
        private readonly AuditTrailService _auditTrailService = auditTrailService;
        private readonly CacheService _cache = cache ?? throw new ArgumentNullException(nameof(cache));

        #region Good Issue Note 

        private static PaginationViewGINModel GetPaginatedResult(IQueryable<ViewGINModel> query, int page, int pagecount, string searchTerm, string sort, string order, string active)
        {
            Expression<Func<ViewGINModel, object>> defaultSort = x => x.GINId;
            query = QueryHelper.ApplySort(query, sort, order, defaultSort);

            //bool searchable = active == "active" ? false : true;

            bool? isActive = active switch
            {
                "active" => true,
                "inactive" => false,
                _ => null
            };

            var filteredData = query.Where(d => isActive == null || d.IsDelete == !isActive).ToList();

            filteredData = QueryHelper.SearchUtility<ViewGINModel>.Search([.. filteredData], searchTerm, c => c.Type, c => c.Total.ToString()).ToList();

            List<ViewGINModel> pagedData;
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

            var TotalAmount = pagedData.Sum(x => x.Gross);
            return new PaginationViewGINModel
            {
                Count = query.Count(),
                ViewGINModels = pagedData,
                TotalAmount = (decimal)TotalAmount,
            };
        }


        public async Task<PaginationViewGINModel> GetAllPaginationGIHead(
         int page, int pagecount, string searchTerm, string sort = null, string order = null, string active = null)
        {
            var cacheKey = $"{nameof(PaginationViewGINModel)}";
            if (_cache.Get(cacheKey, out PaginationViewGINModel cachedResult))
            {
                return GetPaginatedResult(cachedResult.ViewGINModels.AsQueryable(), page, pagecount, searchTerm, sort, order, active);
            }

            // Project the filtered data to 
            var pagedData1 = await _context.VwListGINHead
                   .Select(x => new ViewGINModel
                   {

                       Description = x.Description,
                       Date = x.Date.ToString("yyyy-MM-dd"),
                       Discount = x.Discount,
                       Gross = x.Gross,
                       GINId = x.GINId,
                       Total = x.Total,
                       Type = x.Type,
                       IsDelete = x.IsDelete,
                       FKLocationId = x.FKLocationId

                   })
                   .ToListAsync();

            // Prepare the pagination response

            var query = pagedData1.AsQueryable();

            var cacheResult = new PaginationViewGINModel
            {
                Count = query.ToList().Count,
                ViewGINModels = [.. query]
            };
            _cache.Set(cacheKey, cacheResult, TimeSpan.FromMinutes(30));

            return GetPaginatedResult(query, page, pagecount, searchTerm, sort, order, active);
        }

       
        public async Task<Message<string>> RegisterGINHeadAsync(InsertGINModel model)
        {

            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var tblGRNHead = new TblGINHead
                {
                    Type = model.Type,
                    Edit_By = model.Edit_By,
                    Edit_Date = model.Edit_Date,
                    GINId = model.GINId,
                    Date = CommonResources.LocalDatetime(),
                    Discount = model.Discount,
                    Gross = model.Gross,
                    Total = model.Total,
                    Description = model.Description,
                    Created = model.Created,
                    FKLocationId = model.FKLocationId,


                };
                _context.TblGINHead.Add(tblGRNHead);
                await _context.SaveChangesAsync();


                var dataItem = _context.TblGINBodyTemps.Where(i => i.UserID == model.Created && i.GINNo.Equals(model.GINId)).ToList();

                #region Accounting

                //TblSupplierPayment tblSupplierPayment = new()
                //{
                //    FKSupplierID = getsupplierID.ID,
                //    GRNNo = obj.Grid,
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
                    TblGINBody tblGRNBody = new()
                    {
                        ItemID = s.ItemID,
                        Code = s.Code,
                        GINId = tblGRNHead.GINId,
                        Cost = s.Cost,
                        Qty = s.Qty,
                        UnitCost = s.UnitCost,
                        ExpDate = s.ExpDate,
                        DisCount = s.Discount ?? 0,
                        Price = s.Sellingprice,
                        FKLocationId = model.FKLocationId
                    };
                    _context.TblGINBodies.Add(tblGRNBody);
                    _context.SaveChanges();




                    _context.TblGINBodyTemps.Remove(s);
                    _context.SaveChanges();

                }

                // Log audit trail
                _auditTrailService.LogAudit("TblGINHeads", "Insert", $"Insert {model.GINId + ' '} ID.", model.Edit_By);

                transactionscope.Commit();
                ClearPaginationCacheGoodReceviedNote();

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"This Good Received Note has been registered",
                };


            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }

        public TblGINHead GetByGrnHeadId(int id)
        {
            return _context.TblGINHead.SingleOrDefault(d => d.GINId.Equals(id));
        }

        public async Task<Message<string>> UpdateGINHeadAsync(UpdateGINModel model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existdata = GetByGrnHeadId(model.GINId);


                existdata.Type = model.Type;
                existdata.Edit_By = model.Edit_By;
                existdata.Edit_Date = model.Edit_Date;
                existdata.Date = CommonResources.LocalDatetime();
                existdata.Discount = model.Discount;
                existdata.Gross = model.Gross;
                existdata.Total = model.Total;
                existdata.FKLocationId = model.FKLocationId;
                existdata.Description = model.Description;

                await _context.SaveChangesAsync();


                var dataItem = _context.TblGINBodyTemps.Where(i => i.UserID == model.Edit_By).ToList();

                #region Accounting

                //TblSupplierPayment tblSupplierPayment = new()
                //{
                //    FKSupplierID = getsupplierID.ID,
                //    GRNNo = obj.Grid,
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
                    var GrnBody = _context.TblGINBodies.SingleOrDefault(d => d.GINId == model.GINId && d.ItemID == s.ItemID && d.Id == s.GINBodyNo);
                    if (GrnBody != null)
                    {
                        GrnBody.Qty = s.Qty;
                        GrnBody.Code = s.Code;
                        GrnBody.Cost = s.Cost;
                        GrnBody.UnitCost = s.UnitCost;
                        GrnBody.ExpDate = s.ExpDate;
                        GrnBody.Price = s.Sellingprice;
                        GrnBody.DisCount = s.Discount ?? 0;
                    }
                    else
                    {
                        TblGINBody tblGRNBody = new()
                        {
                            ItemID = s.ItemID,
                            Code = s.Code,
                            GINId = model.GINId,
                            Cost = s.Cost,
                            Qty = s.Qty,
                            UnitCost = s.UnitCost,
                            ExpDate = s.ExpDate,
                            DisCount = s.Discount ?? 0,
                            Price = s.Sellingprice,
                            FKLocationId = model.FKLocationId
                        };
                        _context.TblGINBodies.Add(tblGRNBody);
                    }
                    _context.SaveChanges();

                    _context.TblGINBodyTemps.Remove(s);
                    _context.SaveChanges();

                }

                // Log audit trail
                _auditTrailService.LogAudit("TblGINBodyHeads", "Update", $"Update {model.GINId + ' '} ID.", model.Edit_By);

                transactionscope.Commit();
                ClearPaginationCacheGoodReceviedNote();
                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Good Received Note has been updated successfully",
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Update error {ex.Message}" };
            }


        }

        public async Task<Message<string>> DeleteGINHeadAsync(DeleteGINModel model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existClient = GetByGrnHeadId(model.GINId);

                if (existClient.IsDelete == false)
                {
                    existClient.IsDelete = true;
                    existClient.Delete_By = model.Delete_By;
                    existClient.Delete_Date = model.Delete_Date;

                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblGINBodyHeads", "Delete", $"Delete this {model.GINId} name.", model.Edit_By);

                    await _context.SaveChangesAsync();

                    transactionscope.Commit();
                    ClearPaginationCacheGoodReceviedNote();
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
        public async Task<ViewGINModel> GetDetailsByGINHeadId(int id, string Userid)
        {
            try
            {
                var allRows = _context.TblGINBodyTemps.Where(d => d.UserID.Equals(Userid)).ToList();

                foreach (var row in allRows)
                {
                    _context.TblGINBodyTemps.Remove(row);
                }


                _context.SaveChanges();
                var getTempdata = (from a in _context.TblGINBodies
                                   join s in _context.TblStock_Mains on a.ItemID equals s.ID
                                   where a.GINId.Equals(id)
                                   select new ViewGINBodyModel()
                                   {
                                       Code = a.Code,
                                       Cost = a.Cost,
                                       GINId = (int)a.GINId,
                                       ItemID = a.ItemID,
                                       GINBodyNo = a.Id,
                                       Qty = a.Qty,
                                       Id = a.Id,
                                       ItemName = s.ItemName,
                                       Price = a.Price,
                                       UnitCost = a.UnitCost,
                                       Amount = a.Cost - a.DisCount,

                                   }).ToList();


                foreach (var s in getTempdata)
                {
                    var ChecktheisthereExsitingCustomerName = GetByName(s.ItemName, Userid, id);

                    if (ChecktheisthereExsitingCustomerName == null)
                    {
                        var tblbodytemp = new TblGINBodyTemp
                        {
                            Code = s.Code,
                            Cost = s.Cost,
                            Discount = s.DisCount,
                            GINBodyNo = s.Id,
                            ItemID = s.ItemID,
                            ItemName = s.ItemName,
                            Qty = s.Qty,
                            Amount = s.Amount,
                            Sellingprice = s.Price,
                            UnitCost = s.UnitCost,
                            UserID = Userid,
                            GINNo = id,

                        };
                        _context.TblGINBodyTemps.Add(tblbodytemp);
                        _context.SaveChanges();
                    }
                }
                var dt = await (from x in _context.TblGINHead
                                select new ViewGINModel()
                                {
                                    Date = x.Date.ToString("yyyy-MM-dd"),
                                    Discount = x.Discount,
                                    Description = x.Description,
                                    Gross = x.Gross,
                                    GINId = x.GINId,
                                    Total = x.Total,
                                    Type = x.Type,
                                    IsDelete = x.IsDelete,
                                }).SingleOrDefaultAsync(d => d.GINId.Equals(id));
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void ClearPaginationCacheGoodReceviedNote()
        {
            string cacheKey = $"{nameof(ViewGINModel)}";

            if (_cache.Get(cacheKey, out ViewGINModel cachedResult))
            {
                _cache.Remove(cacheKey);
            }
            _cache.Remove(nameof(PaginationViewGrnHead));
            _cache.Remove(nameof(PaginationViewGRNBody));
            _cache.Remove(nameof(PaginationViewStockMain.IQueryData1));
            _cache.Remove(nameof(PaginationViewStockMain.IQueryData));
        }

        public void ClearToBin(string Userid)
        {
            try
            {
                var allRows = _context.TblGINBodyTemps.Where(d => d.UserID.Equals(Userid) && d.GINNo > 0).ToList();

                foreach (var row in allRows)
                {
                    _context.TblGINBodyTemps.Remove(row);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        #endregion

        #region GIN Temp Data Load
        public async Task<PaginationViewGINBodyModel> GetAllGINTempBodyData(int page, int pagecount, string searchTerm, string sort, string order)
        {


            var query = (from a in _context.TblGINBodyTemps
                         join s in _context.TblStock_Mains on a.ItemID equals s.ID
                         where (string.IsNullOrEmpty(searchTerm) || a.Code.Contains(searchTerm))
                         select new ViewGINBodyModel()
                         {
                             Code = a.Code,
                             Cost = a.Cost,
                             DisCount = a.Discount,
                             GINId = a.GINNo,
                             ItemID = a.ItemID,
                             GINBodyNo = a.GINBodyNo,
                             Qty = a.Qty,
                             ItemName = s.ItemName,
                             Id = a.Id,
                             Price = a.Sellingprice,
                             UnitCost = a.UnitCost,
                             UserID = a.UserID,
                             Amount = a.Amount,
                         }).AsNoTracking();

            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
            {
                switch (sort.ToLower())
                {
                    // Existing cases...
                    case "code":
                        query = (order.ToLower() == "asc") ? query.OrderBy(a => a.Code) : query.OrderByDescending(a => a.Code);
                        break;

                    case "cost":
                        query = (order.ToLower() == "asc") ? query.OrderBy(a => a.Cost) : query.OrderByDescending(a => a.Cost);
                        break;

                    case "ginid":
                        query = (order.ToLower() == "asc") ? query.OrderBy(a => a.GINId) : query.OrderByDescending(a => a.GINId);
                        break;
                    case "itemid":
                        query = (order.ToLower() == "asc") ? query.OrderBy(a => a.ItemID) : query.OrderByDescending(a => a.ItemID);
                        break;
                    case "ginbodyno":
                        query = (order.ToLower() == "asc") ? query.OrderBy(a => a.GINBodyNo) : query.OrderByDescending(a => a.GINBodyNo);
                        break;
                    case "qty":
                        query = (order.ToLower() == "asc") ? query.OrderBy(a => a.Qty) : query.OrderByDescending(a => a.Qty);
                        break;
                    case "id":
                        query = (order.ToLower() == "asc") ? query.OrderBy(a => a.Id) : query.OrderByDescending(a => a.Id);
                        break;
                    case "price":
                        query = (order.ToLower() == "asc") ? query.OrderBy(a => a.Price) : query.OrderByDescending(a => a.Price);
                        break;
                    case "unitcost":
                        query = (order.ToLower() == "asc") ? query.OrderBy(a => a.UnitCost) : query.OrderByDescending(a => a.UnitCost);
                        break;
                    case "userid":
                        query = (order.ToLower() == "asc") ? query.OrderBy(a => a.UserID) : query.OrderByDescending(a => a.UserID);
                        break;
                    default:
                        // Handle default case if needed
                        break;
                }
            }


            var dt = await query.ToListAsync();
            var TotalAmount = dt.Sum(x => x.Cost);
            var TotalDiscount = dt.Sum(x => x.DisCount);
            var TotalGross = dt.Sum(x => x.Amount);
            var paginationListDatas = new PaginationViewGINBodyModel
            {
                Count = dt.Count,
                ViewGINBodyModels = dt.Skip((page - 1) * pagecount).Take(pagecount).ToList(),
                ToatalGross = TotalGross,
                ToatalDiscount = TotalDiscount,
                ToatalAmount = TotalAmount,
            };

            // Cache the result with a sliding expiration of 5 minutes (adjust as needed)
            //_cache.Set(cacheKey, dt, TimeSpan.FromMinutes(1000));

            return paginationListDatas;
        }

        public async Task<Message<string>> RegisterTempBodyAsync(InsertGINbodyModel model)
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
                    var temp = new TblGINBodyTemp
                    {

                        Code = model.Code,
                        Discount = model.DisCount,
                        Cost = model.Cost,
                        ItemID = model.ItemID,
                        GINNo = model.GINNo,
                        Qty = model.Qty,
                        Sellingprice = model.Price,
                        UnitCost = model.UnitCost,
                        UserID = model.UserID,
                        Amount = model.Amount,
                        ItemName = model.ItemName,
                        GINBodyNo = model.GINNo,
                    };

                    _context.TblGINBodyTemps.Add(temp);
                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblGRNBodyTemps", "Insert", $"Insert {model.Code + ' '} Code.", model.UserID);

                    transactionscope.Commit();
                    ClearPaginationCacheGoodReceivedTemp();

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

        public TblGINBodyTemp GetTempBodyById(int id)
        {
            return _context.TblGINBodyTemps.SingleOrDefault(d => d.Id.Equals(id));
        }

        public TblGINBodyTemp GetByName(string Name, string userid, int GINNo)
        {
            return _context.TblGINBodyTemps.SingleOrDefault(d => d.ItemName.Equals(Name) && d.UserID.Equals(userid) && d.GINNo.Equals(GINNo));
        }

        public async Task<Message<string>> UpdateTempBodyAsync(UpdateGINBodyModel model)
        {
            try
            {

                var transactionscope = _context.Database.BeginTransaction();

                var exist = GetTempBodyById(model.Id);

                exist.Code = model.Code;
                exist.Discount = model.DisCount;
                exist.Cost = model.Cost;
                exist.ItemID = model.ItemID;
                exist.GINNo = model.GINNo;
                exist.Qty = model.Qty;
                exist.Sellingprice = model.Price;
                exist.UnitCost = model.UnitCost;
                exist.UserID = model.UserID;
                exist.Amount = model.Amount;


                await _context.SaveChangesAsync();


                // Log audit trail
                _auditTrailService.LogAudit("TblGRNBodyTemps", "Update", $"Update {model.Code + ' '} Code.", model.UserID);

                transactionscope.Commit();
                ClearPaginationCacheGoodReceivedTemp();

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

        public async Task<Message<string>> DeleteTempBodyAsync(DeleteGINBodyModel model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existClient = GetTempBodyById(model.Id);

                _context.Remove(existClient);
                await _context.SaveChangesAsync();

                // Log audit trail
                _auditTrailService.LogAudit("TblGINBodyTemps", "Delete", $"Delete this {model.Code + ' ' + model.Id} name.", model.UserID);

                await _context.SaveChangesAsync();

                transactionscope.Commit();

                ClearPaginationCacheGoodReceivedTemp();

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
        public async Task<ViewGINBodyModel> GetDetailsTempBodyById(int id)
        {
            return await (from a in _context.TblGINBodyTemps
                          join s in _context.TblStock_Mains on a.ItemID equals s.ID
                          select new ViewGINBodyModel()
                          {
                              Code = a.Code,
                              Cost = a.Cost,
                              DisCount = a.Discount,
                              GINNo = a.GINNo,
                              ItemID = a.ItemID,
                              GINBodyNo = a.GINBodyNo,
                              Qty = a.Qty,
                              Id = a.Id,
                              ItemName = s.ItemName,
                              Price = a.Sellingprice,
                              UnitCost = a.UnitCost,
                              Amount = a.Amount,
                              UserID = a.UserID,
                          }).SingleOrDefaultAsync(d => d.Id.Equals(id));

        }


        private void ClearPaginationCacheGoodReceivedTemp()
        {
            string cacheKey = $"{nameof(ViewGINBodyModel)}";

            if (_cache.Get(cacheKey, out ViewGINBodyModel cachedResult))
            {
                _cache.Remove(cacheKey);
            }
        }

        #endregion


        // For Print 
        public ViewGINModel GetDetailsByGINHeadIdForPrint(int id)
        {
            try
            {
                var dt = (from x in _context.VwListGINHead
                          join u in _context.Users on x.Created equals u.Id
                          where x.GINId == id

                          select new ViewGINModel()
                          {
                              Created = u.FirstName,
                              Date = x.Date.ToString("yyyy-MM-dd"),
                              Discount = x.Discount,
                              Gross = x.Gross,
                              Total = x.Total,
                              Type = x.Type,
                              IsDelete = x.IsDelete,

                          }).SingleOrDefault();
                return dt;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<ViewGINBodyModel> GetByGINBodiesId(int id)
        {
            var dt = (from a in _context.TblGINBodies
                      join i in _context.TblStock_Mains on a.ItemID equals i.ID
                      where a.GINId.Equals(id)
                      select new ViewGINBodyModel
                      {
                          Code = a.Code,
                          Cost = a.Cost,
                          GINId = (int)a.GINId,
                          ItemID = a.ItemID,
                          Qty = a.Qty,
                          Id = a.Id,
                          ItemName = i.ItemName,
                          Price = a.Price,
                          UnitCost = a.UnitCost,
                          Amount = a.UnitCost,

                      }).ToList();

            return dt;
        }



    }
}
