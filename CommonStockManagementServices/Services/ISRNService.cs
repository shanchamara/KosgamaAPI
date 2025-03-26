using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Model;
using System.Linq.Expressions;

namespace CommonStockManagementServices.Services
{


    public class SRNService(AppDbContext context, AuditTrailService auditTrailService, IMemoryCache cache)
    {
        private readonly AppDbContext _context = context;
        private readonly AuditTrailService _auditTrailService = auditTrailService;
        private readonly IMemoryCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));


        private void InvalidateCache(string methodName)
        {
            _cache.Remove(methodName); // Invalidate cache for the specified method
        }

        #region Super Return Note 
        public async Task<PaginationViewSRNHead> GetAllPaginationSRNHead(int page, int pagecount, string searchTerm, string sort, string order, int SupplierId, int locationId)
        {
            var cacheKey = $"{nameof(PaginationViewSRNHead)}";
            // Check if the result is already in the cache

            IQueryable<VwListSRNHeads> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewSRNHead cachedResult))
            {

                query = cachedResult.IQueryData.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<VwListSRNHeads, object>> defaultSort1 = x => x.ID;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<VwListSRNHeads>.Search([.. query], searchTerm, c => c.InvoiceDate.ToString(), c => c.Description, c => c.Type, c => c.ID.ToString(),
                c => c.SRNType, c => c.Gross.ToString(), c => c.Total.ToString(), c => c.RefInv);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(x => new ViewSRNHead
                       {

                           Created = x.Created,
                           Date = x.InvoiceDate.ToString("yyyy-MM-dd"),
                           Supplier = x.Supplier,
                           Discount = x.Discount,
                           Gross = x.Gross,
                           ID = x.ID,
                           SRNType = x.SRNType,
                           RefInv = x.RefInv,
                           Total = x.Total,
                           Type = x.Type,
                           LocationId = x.LocationId,
                           FKSupplier_ID = x.FKSupplier_ID,
                           IsDelete = x.IsDelete,
                           Description = x.Description,
                       }).Where(d => d.FKSupplier_ID.Equals(SupplierId) && d.LocationId.Equals(locationId))
                       .ToList();

                // Prepare the pagination response
                var TotalAmount = pagedData1.Sum(x => x.Gross);
                var paginationListData1 = new PaginationViewSRNHead
                {
                    Count = pagedData1.Count,
                    ViewSRNHeads = pagedData1.Skip((page - 1) * pagecount).Take(pagecount).ToList(),
                    ToatalAmount = (decimal)TotalAmount,

                };

                return paginationListData1;
            }
            else
            {
                query = _context.VwListSRNHeads.AsQueryable();

            }

            // Apply sorting based on sort and order parameters

            Expression<Func<VwListSRNHeads, object>> defaultSort = x => x.ID;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => !d.IsDelete).ToListAsync();

            filteredData = QueryHelper.SearchUtility<VwListSRNHeads>.Search(filteredData, null, c => c.InvoiceDate.ToString(), c => c.Description, c => c.Type, c => c.ID.ToString(),
                c => c.SRNType, c => c.Gross.ToString(), c => c.Total.ToString(), c => c.RefInv);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(x => new ViewSRNHead
                {
                    Created = x.Created,
                    Date = x.InvoiceDate.ToString("yyyy-MM-dd"),
                    Supplier = x.Supplier,
                    Discount = x.Discount,
                    Gross = x.Gross,
                    ID = x.ID,
                    SRNType = x.SRNType,
                    RefInv = x.RefInv,
                    Total = x.Total,
                    Type = x.Type,
                    IsDelete = x.IsDelete,
                    LocationId = x.LocationId,
                    FKSupplier_ID = x.FKSupplier_ID,
                    Description = x.Description,
                }).Where(d => d.FKSupplier_ID.Equals(SupplierId) && d.LocationId.Equals(locationId))
                .ToList();

            // Prepare the pagination response
            var TotalAmount1 = pagedData.Sum(x => x.Gross);
            var paginationListData = new PaginationViewSRNHead
            {
                Count = pagedData.Count,
                ToatalAmount = (decimal)TotalAmount1,
                ViewSRNHeads = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList(),
            };

            // Cache the result

            var CacheResult = new PaginationViewSRNHead
            {
                Count = filteredData.Count,
                IQueryData = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;

        }

        public async Task<Message<string>> RegisterSRNHeadAsync(InsertSRNHead model)
        {

            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var tblSRNHead = new TblStockReturnNoteHead
                {
                    Type = model.Type,
                    Edit_By = model.Edit_By,
                    Edit_Date = model.Edit_Date,
                    Date = CommonResources.LocalDatetime(),
                    RefInv = model.RefInv,
                    Created = model.Created,
                    Discount = model.Discount,
                    FKSupplier_ID = model.FKSupplier_ID,
                    SRNType = model.SRNType,
                    Gross = model.Gross,
                    Description = model.Description,
                    Total = model.Total,
                    FKLocationId = model.LocationId
                };
                _context.TblStockReturnNoteHeads.Add(tblSRNHead);
                await _context.SaveChangesAsync();


                var dataItem = _context.TblStockReturnNoteBodyTemps.Where(i => i.UserID == model.Created && i.SRnNo.Equals(model.ID)).ToList();

                #region Accounting

                //TblSupplierPayment tblSupplierPayment = new()
                //{
                //    FKSupplierID = getsupplierID.ID,
                //    SRNNo = obj.Grid,
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
                    TblStockReturnNoteBody tblSRNBody = new()
                    {
                        ItemID = s.ItemID,
                        Code = s.Code,
                        SRNno = tblSRNHead.ID,
                        Cost = s.Cost,
                        Qty = s.Qty,
                        UnitCost = s.UnitCost,
                        ExpDate = s.ExpDate,
                        DisCount = s.Discount ?? 0,
                        Price = s.Sellingprice,
                        FKLocationId = model.LocationId

                    };
                    _context.TblStockReturnNoteBodies.Add(tblSRNBody);
                    _context.SaveChanges();


                    _context.TblStockReturnNoteBodyTemps.Remove(s);
                    _context.SaveChanges();

                }

                // Log audit trail
                _auditTrailService.LogAudit("TblSRNHeads", "Insert", $"Insert {model.ID + ' '} ID.", model.Edit_By);

                transactionscope.Commit();
                InvalidateCache(nameof(PaginationViewSRNHead));
                InvalidateCache(nameof(PaginationViewSRNBody));
                InvalidateCache(nameof(PaginationViewStockMain.IQueryData1));
                InvalidateCache(nameof(PaginationViewStockMain.IQueryData));
                return new Message<string>()
                {
                    Status = "S",
                    Text = $"This Supplier Return Note has been registered",
                };


            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }

        public async Task<Message<string>> CheckInvoiceHaveaGRN(InsertSRNHead model)
        {

            try
            {
                var values = await _context.TblGRNHeads.SingleOrDefaultAsync(d => d.IsDelete.Equals(false) && d.RefInv.Trim().Equals(model.RefInv.Trim()) && d.FKSupplier_ID.Equals(model.FKSupplier_ID));

                if (values != null)
                {

                    var allRows = _context.TblStockReturnNoteBodyTemps.Where(d => d.UserID.Equals(model.Edit_By) && d.RefInv.Equals(model.RefInv)).ToList();

                    foreach (var row in allRows)
                    {
                        _context.TblStockReturnNoteBodyTemps.Remove(row);
                    }
                    _context.SaveChanges();

                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This supplier has the invoice",
                    };
                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This supplier does not have the invoice",
                    };
                }

            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }


        public TblStockReturnNoteHead GetBySRNHeadId(int id)
        {
            return _context.TblStockReturnNoteHeads.SingleOrDefault(d => d.ID.Equals(id));
        }

        public ViewSRNHead GetDetailsBySRNHeadIdForPrint(int id)
        {
            try
            {
                var dt = (from x in _context.VwListSRNHeads
                          join u in _context.Users on x.Created equals u.Id
                          where x.ID == id

                          select new ViewSRNHead()
                          {
                              Created = u.FirstName,
                              Date = x.InvoiceDate.ToString("yyyy-MM-dd"),
                              Supplier = x.Supplier,
                              Discount = x.Discount,
                              Gross = x.Gross,
                              ID = x.ID,
                              SRNType = x.SRNType,
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

        public List<ViewTempSRNBody> GetBySRNBodiesId(int id)
        {
            var dt = (from a in _context.TblStockReturnNoteBodies
                      join i in _context.TblStock_Mains on a.ItemID equals i.ID
                      where a.SRNno.Equals(id)
                      select new ViewTempSRNBody
                      {
                          Code = a.Code,
                          //Batch = a.Batch,
                          Cost = a.Cost,
                          Discount = a.DisCount,
                          SRNNo = (int)a.SRNno,
                          ItemID = a.ItemID,
                          Qty = a.Qty,
                          Id = a.Id,
                          ItemName = i.ItemName,
                          Sellingprice = a.Price,
                          UnitCost = a.UnitCost,
                          Amount = a.UnitCost,

                      }).ToList();

            return dt;
        }

        public async Task<Message<string>> UpdateSRNHeadAsync(UpdateSRNHead model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existdata = GetBySRNHeadId(model.ID);


                existdata.Type = model.Type;
                existdata.Edit_By = model.Edit_By;
                existdata.Edit_Date = model.Edit_Date;
                existdata.ID = model.ID;
                existdata.Date = CommonResources.LocalDatetime();
                existdata.RefInv = model.RefInv;
                existdata.Discount = model.Discount;
                existdata.FKSupplier_ID = model.FKSupplier_ID;
                existdata.SRNType = model.SRNType;
                existdata.Gross = model.Gross;
                existdata.Description = model.Description;
                existdata.Total = model.Total;
                existdata.FKLocationId = model.LocationId;

                await _context.SaveChangesAsync();


                var dataItem = _context.TblStockReturnNoteBodyTemps.Where(i => i.UserID == model.Edit_By).ToList();

                #region Accounting

                //TblSupplierPayment tblSupplierPayment = new()
                //{
                //    FKSupplierID = getsupplierID.ID,
                //    SRNNo = obj.Grid,
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
                    var SRNBody = _context.TblStockReturnNoteBodies.SingleOrDefault(d => d.SRNno == model.ID && d.ItemID == s.ItemID && d.Id == s.SRnBodyNo);
                    if (SRNBody != null)
                    {
                        SRNBody.Qty = s.Qty;
                        SRNBody.Code = s.Code;
                        SRNBody.Cost = s.Cost;
                        SRNBody.UnitCost = s.UnitCost;
                        SRNBody.ExpDate = s.ExpDate;
                        SRNBody.Price = s.Sellingprice;
                        SRNBody.DisCount = s.Discount ?? 0;
                        SRNBody.FKLocationId = model.LocationId;
                        _context.SaveChanges();
                    }
                    else
                    {
                        TblStockReturnNoteBody tblSRNBody = new()
                        {
                            ItemID = s.ItemID,
                            Code = s.Code,
                            SRNno = model.ID,
                            Cost = s.Cost,
                            Qty = s.Qty,
                            UnitCost = s.UnitCost,
                            ExpDate = s.ExpDate,
                            DisCount = s.Discount ?? 0,
                            Price = s.Sellingprice,
                            FKLocationId = model.LocationId,
                        };
                        _context.TblStockReturnNoteBodies.Add(tblSRNBody);
                        _context.SaveChanges();

                    }

                    _context.TblStockReturnNoteBodyTemps.Remove(s);
                    _context.SaveChanges();

                }

                // Log audit trail
                _auditTrailService.LogAudit("TblSRNHeads", "Update", $"Update {model.ID + ' '} ID.", model.Edit_By);

                transactionscope.Commit();
                InvalidateCache(nameof(PaginationViewSRNHead));
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

        public async Task<Message<string>> DeleteSRNHeadAsync(DeleteSRNHead model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existClient = GetBySRNHeadId(model.ID);

                if (existClient.IsDelete == false)
                {
                    existClient.IsDelete = true;
                    existClient.Delete_By = model.Delete_By;
                    existClient.Delete_Date = model.Delete_Date;

                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblSRNHeads", "Delete", $"Delete this {model.ID} name.", model.Edit_By);

                    await _context.SaveChangesAsync();

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewSRNHead));
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
        public async Task<ViewSRNHead> GetDetailsBySRNHeadId(int id, string Userid)
        {
            try
            {

                var allRows = _context.TblStockReturnNoteBodyTemps.Where(d => d.UserID.Equals(Userid)).ToList();


                _context.TblStockReturnNoteBodyTemps.RemoveRange(allRows);

                _context.SaveChanges();

                var getTempdata = (from a in _context.TblStockReturnNoteBodies
                                   join s in _context.TblStock_Mains on a.ItemID equals s.ID
                                   where a.SRNno.Equals(id)
                                   select new ViewTempSRNBody()
                                   {
                                       Code = a.Code,
                                       Cost = a.Cost,
                                       Discount = a.DisCount,
                                       SRNNo = (int)a.SRNno,
                                       ItemID = a.ItemID,
                                       SRNBodyNo = a.Id,
                                       Qty = a.Qty,
                                       Id = a.Id,
                                       ItemName = s.ItemName,
                                       Sellingprice = a.Price,
                                       UnitCost = a.UnitCost,
                                       Amount = a.Cost - a.DisCount,
                                   }).ToList();


                foreach (var s in getTempdata)
                {
                    var ChecktheisthereExsitingCustomerName = GetByName(s.ItemName, Userid);

                    if (ChecktheisthereExsitingCustomerName == null)
                    {
                        var tblbodytemp = new TblStockReturnNoteBodyTemp
                        {
                            Code = s.Code,
                            Cost = s.Cost,
                            Discount = s.Discount,
                            SRnBodyNo = s.Id,
                            ItemID = s.ItemID,
                            ItemName = s.ItemName,
                            Qty = s.Qty,
                            Amount = s.Amount,
                            Sellingprice = s.Sellingprice,
                            UnitCost = s.UnitCost,
                            UserID = Userid,
                            SRnNo = id,

                        };
                        _context.TblStockReturnNoteBodyTemps.Add(tblbodytemp);
                        _context.SaveChanges();
                    }
                }
                var dt = await (from x in _context.TblStockReturnNoteHeads
                                select new ViewSRNHead()
                                {
                                    Created = x.Created,
                                    Date = x.Date.ToString("yyyy-MM-dd"),
                                    Supplier = x.FKTblSupplier.Company,
                                    Discount = x.Discount,
                                    Gross = x.Gross,
                                    ID = x.ID,
                                    SRNType = x.SRNType,
                                    RefInv = x.RefInv,
                                    Total = x.Total,
                                    Description = x.Description,
                                    Type = x.Type,
                                    IsDelete = x.IsDelete,
                                }).SingleOrDefaultAsync(d => d.ID.Equals(id));
                return dt;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void ClearToBin(string Userid, string InvoiceNO)
        {
            try
            {
                var allRows = _context.TblStockReturnNoteBodyTemps.Where(d => d.UserID.Equals(Userid) && d.RefInv.Equals(InvoiceNO)).ToList();


                _context.TblStockReturnNoteBodyTemps.RemoveRange(allRows);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }




        #endregion

        #region SRN Temp Data Load
        public async Task<PaginationViewSRNBody> GetAllSRNTempBodyData(int page, int pagecount, string searchTerm, string sort, string order, string userid)
        {

            var cacheKey = $"{nameof(PaginationViewSRNBody)}";
            // Check if the result is already in the cache

            IQueryable<ViewTempSRNBody> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewSRNBody cachedResult))
            {

                query = cachedResult.ViewTempSRNBodies.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<ViewTempSRNBody, object>> defaultSort1 = x => x.Id;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<ViewTempSRNBody>.Search([.. query], searchTerm, null);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewTempSRNBody
                       {
                           Code = a.Code,
                           Batch = a.Batch,
                           Cost = a.Cost,
                           Discount = a.Discount,
                           SRNNo = a.SRNNo,
                           ItemID = a.ItemID,
                           SRNBodyNo = a.SRNBodyNo,
                           Qty = a.Qty,
                           ItemName = a.ItemName,
                           Id = a.Id,
                           Sellingprice = a.Sellingprice,
                           UnitCost = a.UnitCost,
                           UserID = a.UserID,
                           Amount = a.Amount,
                           RefInv = a.RefInv,
                           UnitName = a.UnitName,
                           Qtypiece = a.Qtypiece,
                           UnitSize = a.UnitSize,
                       })
                       .ToList();

                // Prepare the pagination response
                var TotalAmount1 = filteredData1.Sum(x => x.Cost);
                var TotalDiscount1 = filteredData1.Sum(x => x.Discount);
                var TotalGross1 = filteredData1.Sum(x => x.Amount);

                var paginationListData1 = new PaginationViewSRNBody
                {
                    Count = filteredData1.Count,
                    ViewTempSRNBodies = pagedData1.Skip((page - 1) * pagecount).Take(pagecount).ToList(),
                    ToatalDiscount = TotalDiscount1,
                    ToatalAmount = TotalAmount1,
                    ToatalGross = TotalGross1,
                };

                return paginationListData1;
            }
            else
            {
                query = (from a in _context.TblStockReturnNoteBodyTemps
                         join s in _context.TblStock_Mains on a.ItemID equals s.ID
                         //where (string.IsNullOrEmpty(searchTerm) || a.Code.Contains(searchTerm))
                         select new ViewTempSRNBody()
                         {
                             Code = a.Code,
                             Batch = a.Batch,
                             Cost = a.Cost,
                             Discount = a.Discount,
                             SRNNo = a.SRnNo,
                             ItemID = a.ItemID,
                             SRNBodyNo = a.SRnBodyNo,
                             Qty = a.Qty,
                             ItemName = s.ItemName,
                             Id = a.Id,
                             Sellingprice = a.Sellingprice,
                             UnitCost = a.UnitCost,
                             UserID = a.UserID,
                             Amount = a.Amount,
                             RefInv = a.RefInv,
                             UnitName = a.UnitName,
                             Qtypiece = a.Qtypiece,
                             UnitSize = a.UnitSize,

                         }).AsQueryable();

            }

            // Apply sorting based on sort and order parameters

            Expression<Func<ViewTempSRNBody, object>> defaultSort = x => x.Id;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => d.UserID.Equals(userid)).ToListAsync();

            filteredData = QueryHelper.SearchUtility<ViewTempSRNBody>.Search(filteredData, null, null);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewTempSRNBody
                {
                    Code = a.Code,
                    Batch = a.Batch,
                    Cost = a.Cost,
                    Discount = a.Discount,
                    SRNNo = a.SRNNo,
                    ItemID = a.ItemID,
                    SRNBodyNo = a.SRNBodyNo,
                    Qty = a.Qty,
                    ItemName = a.ItemName,
                    Id = a.Id,
                    Sellingprice = a.Sellingprice,
                    UnitCost = a.UnitCost,
                    UserID = a.UserID,
                    Amount = a.Amount,
                    RefInv = a.RefInv,
                    UnitName = a.UnitName,
                    Qtypiece = a.Qtypiece,
                    UnitSize = a.UnitSize,
                })
                .ToList();

            // Prepare the pagination response
            var TotalAmount = filteredData.Sum(x => x.Cost);
            var TotalDiscount = filteredData.Sum(x => x.Discount);
            var TotalGross = filteredData.Sum(x => x.Amount);

            var paginationListData = new PaginationViewSRNBody
            {
                Count = filteredData.Count,
                ViewTempSRNBodies = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList(),
                ToatalDiscount = TotalDiscount,
                ToatalAmount = TotalAmount,
                ToatalGross = TotalGross,
            };

            // Cache the result

            var CacheResult = new PaginationViewSRNBody
            {
                Count = filteredData.Count,
                ViewTempSRNBodies = pagedData,
            };

            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;
        }

        public async Task<Message<string>> RegisterTempBodyAsync(InsertTempSRNBody model)
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
                    var temp = new TblStockReturnNoteBodyTemp
                    {
                        Batch = model.Batch,
                        Code = model.Code,
                        Discount = model.Discount,
                        Cost = model.Cost,
                        ItemID = model.ItemID,
                        SRnNo = model.SRNNo,
                        Qty = model.Qty,
                        Sellingprice = model.Sellingprice,
                        UnitCost = model.UnitCost,
                        UserID = model.UserID,
                        Amount = model.Amount,
                        ItemName = model.ItemName,
                        RefInv = model.RefInv,
                        UnitSize = model.UnitSize,
                        Qtypiece = model.Qtypiece,
                        UnitName = model.UnitName,
                        //REfInvoiceNo = model.RefInv,
                    };

                    _context.TblStockReturnNoteBodyTemps.Add(temp);
                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblSRNBodyTemps", "Insert", $"Insert {model.Code + ' '} Code.", model.UserID);

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewSRNBody));

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

        public TblStockReturnNoteBodyTemp GetTempBodyById(int id)
        {
            return _context.TblStockReturnNoteBodyTemps.SingleOrDefault(d => d.Id.Equals(id));
        }

        public TblStockReturnNoteBodyTemp GetByName(string Name, string userid)
        {
            return _context.TblStockReturnNoteBodyTemps.SingleOrDefault(d => d.ItemName.Equals(Name) && d.UserID.Equals(userid));
        }

        public async Task<Message<string>> UpdateTempBodyAsync(UpdateTempSRNBody model)
        {
            try
            {

                var transactionscope = _context.Database.BeginTransaction();

                var exist = GetTempBodyById(model.Id);

                exist.Batch = model.Batch;
                exist.Code = model.Code;
                exist.Discount = model.Discount;
                exist.Cost = model.Cost;
                exist.ItemID = model.ItemID;
                exist.SRnNo = model.SRNNo;
                exist.Qty = model.Qty;
                exist.Sellingprice = model.Sellingprice;
                exist.UnitCost = model.UnitCost;
                exist.UserID = model.UserID;
                exist.Amount = model.Amount;
                exist.RefInv = model.RefInv;
                exist.UnitName = model.UnitName;
                exist.UnitSize = model.UnitSize;
                exist.Qtypiece = model.Qtypiece;

                await _context.SaveChangesAsync();


                // Log audit trail
                _auditTrailService.LogAudit("TblSRNBodyTemps", "Update", $"Update {model.Code + ' '} Code.", model.UserID);

                transactionscope.Commit();
                InvalidateCache(nameof(PaginationViewSRNBody));

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

        public async Task<Message<string>> DeleteTempBodyAsync(DeleteTempSRNBody model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existClient = GetTempBodyById(model.Id);

                _context.Remove(existClient);
                await _context.SaveChangesAsync();

                // Log audit trail
                _auditTrailService.LogAudit("TblSRNBodyTemps", "Delete", $"Delete this {model.Code + ' ' + model.Id} name.", model.UserID);

                await _context.SaveChangesAsync();

                transactionscope.Commit();

                InvalidateCache(nameof(PaginationViewSRNBody));

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
        public async Task<ViewTempSRNBody> GetDetailsTempBodyById(int id)
        {
            return await (from a in _context.TblStockReturnNoteBodyTemps
                          join s in _context.TblStock_Mains on a.ItemID equals s.ID
                          select new ViewTempSRNBody()
                          {
                              Code = a.Code,
                              Batch = a.Batch,
                              Cost = a.Cost,
                              Discount = a.Discount,
                              SRNNo = a.SRnNo,
                              ItemID = a.ItemID,
                              SRNBodyNo = a.SRnBodyNo,
                              Qty = a.Qty,
                              Id = a.Id,
                              ItemName = s.ItemName,
                              Sellingprice = a.Sellingprice,
                              UnitCost = a.UnitCost,
                              Amount = a.Amount,
                              RefInv = a.RefInv,
                              UserID = a.UserID,
                          }).SingleOrDefaultAsync(d => d.Id.Equals(id));

        }




        #endregion


    }
}
