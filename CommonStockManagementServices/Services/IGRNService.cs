using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Model;
using System.Linq.Expressions;

namespace CommonStockManagementServices.Services
{


    public class GRNService(AppDbContext context, AuditTrailService auditTrailService, IMemoryCache cache)
    {
        private readonly AppDbContext _context = context;
        private readonly AuditTrailService _auditTrailService = auditTrailService;
        private readonly IMemoryCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));

        #region Good Received Note 

        private void InvalidateCache(string methodName)
        {
            _cache.Remove(methodName); // Invalidate cache for the specified method
        }


        public async Task<PaginationViewGrnHead> GetAllPaginationGrnHead(int page, int pagecount, string searchTerm, string sort, string order, int SupplierId, int locationID)
        {
            var cacheKey = $"{nameof(PaginationViewGrnHead)}";
            // Check if the result is already in the cache

            IQueryable<VwListGRNHeads> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewGrnHead cachedResult))
            {

                query = cachedResult.IQueryData.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<VwListGRNHeads, object>> defaultSort1 = x => x.Id;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<VwListGRNHeads>.Search([.. query], searchTerm, c => c.InvoiceDate.ToString(), c => c.Description, c => c.Type, c => c.Id.ToString(),
                c => c.GRNType, c => c.Gross.ToString(), c => c.Total.ToString(), c => c.RefInv);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewGrnHead
                       {

                           Delete_By = a.Delete_By,
                           Delete_Date = a.Delete_Date,
                           Edit_By = a.Edit_By,
                           Edit_Date = a.Edit_Date,
                           Id = a.Id,
                           Created = a.Created,
                           Date = a.InvoiceDate.ToString("yyyy-MM-dd"),
                           Supplier = a.Supplier,
                           Discount = a.Discount,
                           Gross = a.Gross,
                           GRNType = a.GRNType,
                           Pono = a.Pono,
                           RefInv = a.RefInv,
                           Total = a.Total,
                           Type = a.Type,
                           IsDelete = a.IsDelete,
                           Description = a.Description,
                           LocationId = a.LocationId,
                           FKSupplier_ID = a.FKSupplier_ID,
                       }).Where(d => d.FKSupplier_ID.Equals(SupplierId) && d.LocationId.Equals(locationID))
                       .ToList();

                // Prepare the pagination response
                var TotalAmount = pagedData1.Sum(x => x.Gross);
                var paginationListData1 = new PaginationViewGrnHead
                {
                    Count = pagedData1.Count,
                    ViewGrnHeads = pagedData1.Skip((page - 1) * pagecount).Take(pagecount).ToList(),
                    TotalAmount = (decimal)TotalAmount,

                };

                return paginationListData1;
            }
            else
            {
                query = _context.VwListGRNHeads.AsQueryable();

            }

            // Apply sorting based on sort and order parameters

            Expression<Func<VwListGRNHeads, object>> defaultSort = x => x.Id;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => !d.IsDelete).ToListAsync();

            filteredData = QueryHelper.SearchUtility<VwListGRNHeads>.Search(filteredData, null, c => c.InvoiceDate.ToString(), c => c.Description, c => c.Type, c => c.Id.ToString(),
                c => c.GRNType, c => c.Gross.ToString(), c => c.Total.ToString(), c => c.RefInv);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewGrnHead
                {
                    Delete_By = a.Delete_By,
                    Delete_Date = a.Delete_Date,
                    Edit_By = a.Edit_By,
                    Edit_Date = a.Edit_Date,
                    Id = a.Id,
                    Created = a.Created,
                    Date = a.InvoiceDate.ToString("yyyy-MM-dd"),
                    Supplier = a.Supplier,
                    Discount = a.Discount,
                    Gross = a.Gross,
                    GRNType = a.GRNType,
                    Pono = a.Pono,
                    RefInv = a.RefInv,
                    Total = a.Total,
                    Type = a.Type,
                    IsDelete = a.IsDelete,
                    Description = a.Description,
                    LocationId = a.LocationId,
                    FKSupplier_ID = a.FKSupplier_ID,
                }).Where(d => d.FKSupplier_ID.Equals(SupplierId) && d.LocationId.Equals(locationID))
                .ToList();

            // Prepare the pagination response
            var TotalAmount1 = pagedData.Sum(x => x.Gross);
            var paginationListData = new PaginationViewGrnHead
            {
                Count = pagedData.Count,
                TotalAmount = (decimal)TotalAmount1,
                ViewGrnHeads = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList()
            };

            // Cache the result

            var CacheResult = new PaginationViewGrnHead
            {
                Count = filteredData.Count,
                IQueryData = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;

        }

        public async Task<Message<string>> RegisterGrnHeadAsync(InsertGrnHead model)
        {

            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var tblGRNHead = new TblGRNHead
                {
                    Type = model.Type,
                    Edit_By = model.Edit_By,
                    Edit_Date = model.Edit_Date,
                    Id = model.Id,
                    Date = CommonResources.LocalDatetime(),
                    RefInv = model.RefInv,
                    Created = model.Created,
                    Discount = model.Discount,
                    FKSupplier_ID = model.FKSupplier_ID,
                    GRNType = model.GRNType,
                    Gross = model.Gross,
                    Pono = model.Pono,
                    Description = model.Description,
                    Total = model.Total,
                    FKLocationId = model.LocationId
                };
                _context.TblGRNHeads.Add(tblGRNHead);
                await _context.SaveChangesAsync();


                var dataItem = _context.TblGRNBodyTemps.Where(i => i.UserID == model.Created && i.GRnNo.Equals(model.Id)).ToList();

                #region Accounting

                //TblSupplierPayment tblSupplierPayment = new()
                //{
                //    FKSupplierID = getsupplierID.ID,
                //    GRNNo = obj.Id,
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
                    TblGRNBody tblGRNBody = new()
                    {
                        ItemID = s.ItemID,
                        Code = s.Code,
                        Grnno = tblGRNHead.Id,
                        Cost = s.Cost,
                        Qty = s.Qty + s.FreeQty,
                        UnitCost = s.UnitCost,
                        ExpDate = s.ExpDate,
                        DisCount = s.Discount ?? 0,
                        FreeQty = s.FreeQty,
                        Price = s.Sellingprice,
                        FKLocationId = model.LocationId,
                        Qtypiece = s.Qtypiece,
                        UnitName = s.UnitName,
                        UnitSize = s.UnitSize,
                    };
                    _context.TblGRNBodies.Add(tblGRNBody);
                    _context.SaveChanges();

                    // Update Price on the Item 
                    var Price = _context.TblStock_Mains.SingleOrDefault(i => i.ID == s.ItemID);
                    Price.SellingPrice = s.Sellingprice;
                    Price.LastPurchasePrice = s.UnitCost;
                    _context.SaveChanges();

                    if ("Cash" == model.Type && model.GRNType == "Partial Good Received Note")
                    {

                        //var getlot = _context.TblStock_Pharma_Lots.Where(x => x.FKStock_ID == s.ItemID).OrderBy(x => x.Lot).LastOrDefault();
                        //if (getlot != null)
                        //{
                        //    TblStock_Pharma_Lot stock_Pharma_Lot = new()
                        //    {
                        //        Batch = s.Batch ?? getlot.Batch,
                        //        Cost = (decimal)s.Cost,
                        //        DisCount = (decimal?)s.Discount ?? 0,
                        //        Exp_date = s.ExpDate,
                        //        Supplier_ID = model.FKSupplier_ID,
                        //        FKStock_ID = s.ItemID,
                        //        Lot = (Convert.ToInt32(getlot.Lot + 1)),
                        //        Price = (decimal?)s.Sellingprice ?? getlot.Price,
                        //        QTY = (decimal)s.Qty + (decimal)s.FreeQty,
                        //    };
                        //    _context.TblStock_Pharma_Lots.Add(stock_Pharma_Lot);
                        //    _context.SaveChanges();
                        //}
                        //else
                        //{
                        //    TblStock_Pharma_Lot stock_Pharma_Lot = new()
                        //    {
                        //        Batch = s.Batch ?? string.Empty,
                        //        Cost = (decimal)s.Cost,
                        //        DisCount = (decimal?)s.Discount ?? 0,
                        //        Exp_date = s.ExpDate,
                        //        Supplier_ID = model.FKSupplier_ID,
                        //        FKStock_ID = s.ItemID,
                        //        Lot = (Convert.ToInt32(0 + 1)),
                        //        Price = (decimal?)s.Sellingprice ?? 0,
                        //        QTY = (decimal)s.Qty + (decimal)s.FreeQty,
                        //    };
                        //    _context.TblStock_Pharma_Lots.Add(stock_Pharma_Lot);
                        //    _context.SaveChanges();
                        //}
                    }


                    _context.TblGRNBodyTemps.Remove(s);
                    _context.SaveChanges();

                }

                // Log audit trail
                _auditTrailService.LogAudit("TblGRNHeads", "Insert", $"Insert {model.Id + ' '} ID.", model.Edit_By);

                transactionscope.Commit();
                InvalidateCache(nameof(PaginationViewGrnHead));
                InvalidateCache(nameof(PaginationViewGRNBody));
                InvalidateCache(nameof(PaginationViewStockMain.IQueryData1));
                InvalidateCache(nameof(PaginationViewStockMain.IQueryData));
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

        public TblGRNHead GetByGrnHeadId(int id)
        {
            return _context.TblGRNHeads.SingleOrDefault(d => d.Id.Equals(id));
        }

        public ViewGrnHead GetDetailsByGrnHeadIdForPrint(int id)
        {
            try
            {
                var dt = (from x in _context.VwListGRNHeads
                          join u in _context.Users on x.Created equals u.Id
                          where x.Id == id

                          select new ViewGrnHead()
                          {
                              Created = u.FirstName,
                              Date = x.InvoiceDate.ToString("yyyy-MM-dd"),
                              Supplier = x.Supplier,
                              Discount = x.Discount,
                              Gross = x.Gross,
                              LocationId = x.LocationId,
                              Id = x.Id,
                              GRNType = x.GRNType,
                              Pono = x.Pono,
                              RefInv = x.RefInv,
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

        public List<ViewTempGrnBody> GetByGRNBodiesId(int id)
        {
            var dt = (from a in _context.TblGRNBodies
                      join i in _context.TblStock_Mains on a.ItemID equals i.ID
                      where a.Grnno.Equals(id)
                      select new ViewTempGrnBody
                      {
                          Code = a.Code,
                          //Batch = a.Batch,
                          Cost = a.Cost,
                          Discount = a.DisCount,
                          FreeQty = a.FreeQty,
                          GRnNo = (int)a.Grnno,
                          ItemID = a.ItemID,
                          Qty = a.Qty,
                          Id = a.Id,
                          Item_name = i.ItemName,
                          Sellingprice = a.Price,
                          UnitCost = a.UnitCost,
                          Amount = a.UnitCost,
                          UnitName = a.UnitName,
                          Qtypiece = a.Qtypiece,
                          UnitSize = a.UnitSize,
                      }).ToList();

            return dt;
        }

        public async Task<Message<string>> UpdateGrnHeadAsync(UpdateGrnHead model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existdata = GetByGrnHeadId(model.Id);


                existdata.Type = model.Type;
                existdata.Edit_By = model.Edit_By;
                existdata.Edit_Date = model.Edit_Date;
                existdata.Id = model.Id;
                existdata.Date = CommonResources.LocalDatetime();
                existdata.RefInv = model.RefInv;
                existdata.Discount = model.Discount;
                existdata.FKSupplier_ID = model.FKSupplier_ID;
                existdata.GRNType = model.GRNType;
                existdata.Gross = model.Gross;
                existdata.Description = model.Description;
                existdata.Pono = model.Pono;
                existdata.Total = model.Total;
                existdata.FKLocationId = model.LocationId;
                await _context.SaveChangesAsync();


                var dataItem = _context.TblGRNBodyTemps.Where(i => i.UserID == model.Edit_By).ToList();

                // Delete All GRN Boady Values
                var GrnBody = _context.TblGRNBodies.Where(d => d.Grnno == model.Id).ToList();
                _context.TblGRNBodies.RemoveRange(GrnBody);

                _context.SaveChanges();
                #region Accounting

                //TblSupplierPayment tblSupplierPayment = new()
                //{
                //    FKSupplierID = getsupplierID.ID,
                //    GRNNo = obj.Id,
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
                { //&& d.ItemID == s.ItemID && d.Id == s.GRnBodyNo


                    TblGRNBody tblGRNBody = new()
                    {
                        ItemID = s.ItemID,
                        Code = s.Code,
                        Grnno = model.Id,
                        Cost = s.Cost,
                        Qty = s.Qty,
                        UnitCost = s.UnitCost,
                        ExpDate = s.ExpDate,
                        DisCount = s.Discount ?? 0,
                        FreeQty = s.FreeQty,
                        Price = s.Sellingprice,
                        FKLocationId = model.LocationId,
                        UnitSize = s.UnitSize,
                        UnitName = s.UnitName,
                        Qtypiece = s.Qtypiece,

                    };
                    _context.TblGRNBodies.Add(tblGRNBody);
                    _context.SaveChanges();



                    _context.TblGRNBodyTemps.Remove(s);
                    _context.SaveChanges();

                }

                // Log audit trail
                _auditTrailService.LogAudit("TblGRNHeads", "Update", $"Update {model.Id + ' '} ID.", model.Edit_By);

                transactionscope.Commit();
                InvalidateCache(nameof(PaginationViewGrnHead));
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

        public async Task<Message<string>> DeleteGrnHeadAsync(DeleteGrnHead model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existClient = GetByGrnHeadId(model.Id);

                if (existClient.IsDelete == false)
                {
                    existClient.IsDelete = true;
                    existClient.Delete_By = model.Delete_By;
                    existClient.Delete_Date = model.Delete_Date;

                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblGRNHeads", "Delete", $"Delete this {model.Id} name.", model.Edit_By);

                    await _context.SaveChangesAsync();

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewGrnHead));
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
        public async Task<ViewGrnHead> GetDetailsByGrnHeadId(int id, string Userid)
        {
            try
            {

                var allRows = _context.TblGRNBodyTemps.Where(d => d.UserID.Equals(Userid)).ToList();

                _context.TblGRNBodyTemps.RemoveRange(allRows);

                _context.SaveChanges();

                var getTempdata = (from a in _context.TblGRNBodies
                                   join s in _context.TblStock_Mains on a.ItemID equals s.ID
                                   where a.Grnno.Equals(id)
                                   select new ViewTempGrnBody()
                                   {
                                       Code = a.Code,
                                       Cost = a.Cost,
                                       Discount = a.DisCount,
                                       FreeQty = a.FreeQty,
                                       GRnNo = (int)a.Grnno,
                                       ItemID = a.ItemID,
                                       GRnBodyNo = a.Id,
                                       Qty = a.Qty,
                                       Id = a.Id,
                                       Item_name = s.ItemName,
                                       Sellingprice = a.Price,
                                       UnitCost = a.UnitCost,
                                       Amount = a.Cost - a.DisCount,
                                       UnitSize = a.UnitSize,
                                       Qtypiece = a.Qtypiece,
                                       UnitName = a.UnitName,
                                   }).ToList();


                foreach (var s in getTempdata)
                {
                    var ChecktheisthereExsitingCustomerName = GetByName(s.Item_name, Userid, id);

                    if (ChecktheisthereExsitingCustomerName == null)
                    {
                        var tblbodytemp = new TblGRNBodyTemp
                        {
                            Code = s.Code,
                            Cost = s.Cost,
                            Discount = s.Discount,
                            GRnBodyNo = s.Id,
                            ItemID = s.ItemID,
                            ItemName = s.Item_name,
                            Qty = s.Qty,
                            Amount = s.Amount,
                            Sellingprice = s.Sellingprice,
                            UnitCost = s.UnitCost,
                            UserID = Userid,
                            GRnNo = id,
                            UnitName = s.UnitName,
                            Qtypiece = s.Qtypiece,
                            UnitSize = s.UnitSize,

                        };
                        _context.TblGRNBodyTemps.Add(tblbodytemp);
                        _context.SaveChanges();
                        InvalidateCache(nameof(PaginationViewGRNBody));
                    }
                }
                var dt = await (from x in _context.TblGRNHeads
                                select new ViewGrnHead()
                                {
                                    Created = x.Created,
                                    Date = x.Date.ToString("yyyy-MM-dd"),
                                    Supplier = x.FKTblSupplier.Company,
                                    Discount = x.Discount,
                                    Gross = x.Gross,
                                    Id = x.Id,
                                    GRNType = x.GRNType,
                                    Pono = x.Pono,
                                    RefInv = x.RefInv,
                                    Total = x.Total,
                                    Description = x.Description,
                                    Type = x.Type,
                                    IsDelete = x.IsDelete,
                                }).SingleOrDefaultAsync(d => d.Id.Equals(id));
                return dt;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void ClearToBin(string Userid)
        {
            try
            {
                InvalidateCache(nameof(PaginationViewGRNBody));
                var allRows = _context.TblGRNBodyTemps.Where(d => d.UserID.Equals(Userid) && d.GRnNo > 0).ToList();

                _context.TblGRNBodyTemps.RemoveRange(allRows);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public void ClearToBinOnlyUserId(string Userid)
        {
            try
            {
                InvalidateCache(nameof(PaginationViewGRNBody));
                var allRows = _context.TblGRNBodyTemps.Where(d => d.UserID.Equals(Userid)).ToList();

                _context.TblGRNBodyTemps.RemoveRange(allRows);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }




        #endregion

        #region GRN Temp Data Load
        public async Task<PaginationViewGRNBody> GetAllGRnTempBodyData(int page, int pagecount, string searchTerm, string sort, string order, string userId)
        {

            var cacheKey = $"{nameof(PaginationViewGRNBody)}";
            // Check if the result is already in the cache

            IQueryable<ViewTempGrnBody> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewGRNBody cachedResult))
            {

                query = cachedResult.ViewTempGrnBodies.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<ViewTempGrnBody, object>> defaultSort1 = x => x.Id;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<ViewTempGrnBody>.Search([.. query], searchTerm, null);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewTempGrnBody
                       {
                           Code = a.Code,
                           Batch = a.Batch,
                           Cost = a.Cost,
                           Discount = a.Discount,
                           FreeQty = a.FreeQty,
                           GRnNo = a.GRnNo,
                           ItemID = a.ItemID,
                           GRnBodyNo = a.GRnBodyNo,
                           Qty = a.Qty,
                           Item_name = a.Item_name,
                           Id = a.Id,
                           Sellingprice = a.Sellingprice,
                           UnitCost = a.UnitCost,
                           UserID = a.UserID,
                           Amount = a.Amount,
                           Qtypiece = a.Qtypiece,
                           UnitSize = a.UnitSize,
                           UnitName = a.UnitName,
                       })
                       .ToList();

                // Prepare the pagination response
                var TotalAmount1 = filteredData1.Sum(x => x.Cost);
                var TotalDiscount1 = filteredData1.Sum(x => x.Discount);
                var TotalGross1 = filteredData1.Sum(x => x.Amount);

                var paginationListData1 = new PaginationViewGRNBody
                {
                    Count = filteredData1.Count,
                    ViewTempGrnBodies = pagedData1.Skip((page - 1) * pagecount).Take(pagecount).ToList(),
                    ToatalDiscount = TotalDiscount1,
                    ToatalAmount = TotalAmount1,
                    ToatalGross = TotalGross1,
                };

                return paginationListData1;
            }
            else
            {
                query = (from a in _context.TblGRNBodyTemps
                         join s in _context.TblStock_Mains on a.ItemID equals s.ID
                         select new ViewTempGrnBody()
                         {
                             Code = a.Code,
                             Batch = a.Batch,
                             Cost = a.Cost,
                             Discount = a.Discount,
                             FreeQty = a.FreeQty,
                             GRnNo = a.GRnNo,
                             ItemID = a.ItemID,
                             GRnBodyNo = a.GRnBodyNo,
                             Qty = a.Qty,
                             Item_name = s.ItemName,
                             Id = a.Id,
                             Sellingprice = a.Sellingprice,
                             UnitCost = a.UnitCost,
                             UserID = a.UserID,
                             Amount = a.Amount,
                             Qtypiece = a.Qtypiece,
                             UnitSize = a.UnitSize,
                             UnitName = a.UnitName,
                         }).AsQueryable();

            }

            // Apply sorting based on sort and order parameters

            Expression<Func<ViewTempGrnBody, object>> defaultSort = x => x.Id;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => d.UserID.Equals(userId)).ToListAsync();

            filteredData = QueryHelper.SearchUtility<ViewTempGrnBody>.Search(filteredData, null, null);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewTempGrnBody
                {

                    Code = a.Code,
                    Batch = a.Batch,
                    Cost = a.Cost,
                    Discount = a.Discount,
                    FreeQty = a.FreeQty,
                    GRnNo = a.GRnNo,
                    ItemID = a.ItemID,
                    GRnBodyNo = a.GRnBodyNo,
                    Qty = a.Qty,
                    Item_name = a.Item_name,
                    Id = a.Id,
                    Sellingprice = a.Sellingprice,
                    UnitCost = a.UnitCost,
                    UserID = a.UserID,
                    Amount = a.Amount,
                    Qtypiece = a.Qtypiece,
                    UnitSize = a.UnitSize,
                    UnitName = a.UnitName,
                })
                .ToList();

            // Prepare the pagination response
            var TotalAmount = filteredData.Sum(x => x.Cost);
            var TotalDiscount = filteredData.Sum(x => x.Discount);
            var TotalGross = filteredData.Sum(x => x.Amount);

            var paginationListData = new PaginationViewGRNBody
            {
                Count = filteredData.Count,
                ViewTempGrnBodies = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList(),
                ToatalDiscount = TotalDiscount,
                ToatalAmount = TotalAmount,
                ToatalGross = TotalGross,
            };


            // Cache the result
            var Cacheresult = new PaginationViewGRNBody
            {
                Count = filteredData.Count,
                ViewTempGrnBodies = pagedData,

            };

            _cache.Set(cacheKey, Cacheresult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;
        }

        public async Task<Message<string>> RegisterTempBodyAsync(InsertTempGrnBody model)
        {

            try
            {
                var ChecktheisthereExsitingCustomerName = GetByName(model.Item_name, model.UserID, model.GRnNo);

                if (ChecktheisthereExsitingCustomerName == null)
                {
                    // DateTime dateTime = DateTime.ParseExact(model.ExpDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    var transactionscope = _context.Database.BeginTransaction();
                    //if (DateTime.TryParse(model.ExpDate, out DateTime expDate))
                    //{
                    //    DateTime date = expDate;
                    //}
                    var temp = new TblGRNBodyTemp
                    {
                        FreeQty = model.FreeQty,
                        Batch = model.Batch,
                        Code = model.Code,
                        Discount = model.Discount,
                        Cost = model.Cost,
                        ItemID = model.ItemID,
                        GRnNo = model.GRnNo,
                        Qty = model.Qty,
                        Sellingprice = model.Sellingprice,
                        UnitCost = model.UnitCost,
                        UserID = model.UserID,
                        Amount = model.Amount,
                        ItemName = model.Item_name,
                        Qtypiece = model.Qtypiece,
                        UnitSize = model.UnitSize,
                        UnitName = model.UnitName
                    };

                    _context.TblGRNBodyTemps.Add(temp);
                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblGRNBodyTemps", "Insert", $"Insert {model.Code + ' '} Code.", model.UserID);

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewGRNBody));

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

        public TblGRNBodyTemp GetTempBodyById(int id)
        {
            return _context.TblGRNBodyTemps.SingleOrDefault(d => d.Id.Equals(id));
        }

        public TblGRNBodyTemp GetByName(string Name, string userid, int GRNNo)
        {
            return _context.TblGRNBodyTemps.SingleOrDefault(d => d.ItemName.Equals(Name) && d.UserID.Equals(userid) && d.GRnNo.Equals(GRNNo));
        }

        public async Task<Message<string>> UpdateTempBodyAsync(UpdateTempGrnBody model)
        {
            try
            {

                var transactionscope = _context.Database.BeginTransaction();

                var exist = GetTempBodyById(model.Id);

                exist.FreeQty = model.FreeQty;
                exist.Batch = model.Batch;
                exist.Code = model.Code;
                exist.Discount = model.Discount;
                exist.Cost = model.Cost;
                exist.ItemID = model.ItemID;
                exist.GRnNo = model.GRnNo;
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
                _auditTrailService.LogAudit("TblGRNBodyTemps", "Update", $"Update {model.Code + ' '} Code.", model.UserID);

                transactionscope.Commit();
                InvalidateCache(nameof(PaginationViewGRNBody));
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

        public async Task<Message<string>> DeleteTempBodyAsync(DeleteTempGrnBody model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existClient = GetTempBodyById(model.Id);

                _context.Remove(existClient);
                await _context.SaveChangesAsync();

                // Log audit trail
                _auditTrailService.LogAudit("TblGRNBodyTemps", "Delete", $"Delete this {model.Code + ' ' + model.Id} name.", model.UserID);

                await _context.SaveChangesAsync();

                transactionscope.Commit();

                InvalidateCache(nameof(PaginationViewGRNBody));
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
        public async Task<ViewTempGrnBody> GetDetailsTempBodyById(int id)
        {
            return await (from a in _context.TblGRNBodyTemps
                          join s in _context.TblStock_Mains on a.ItemID equals s.ID
                          select new ViewTempGrnBody()
                          {
                              Code = a.Code,
                              Batch = a.Batch,
                              Cost = a.Cost,
                              Discount = a.Discount,
                              FreeQty = a.FreeQty,
                              GRnNo = a.GRnNo,
                              ItemID = a.ItemID,
                              GRnBodyNo = a.GRnBodyNo,
                              Qty = a.Qty,
                              Id = a.Id,
                              Item_name = s.ItemName,
                              Sellingprice = a.Sellingprice,
                              UnitCost = a.UnitCost,
                              Amount = a.Amount,
                              UserID = a.UserID,
                              Qtypiece = a.Qtypiece,
                              UnitSize = a.UnitSize,
                              UnitName = a.UnitName,
                          }).SingleOrDefaultAsync(d => d.Id.Equals(id));

        }


        #endregion



    }
}
