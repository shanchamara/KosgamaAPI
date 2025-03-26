using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Model;
using System.Linq.Expressions;

namespace CommonStockManagementServices.Services
{
    public class SupplierService(AppDbContext context, AuditTrailService auditTrailService, IMemoryCache cache)
    {
        private readonly AppDbContext _context = context;
        private readonly AuditTrailService _auditTrailService = auditTrailService;
        private readonly IMemoryCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));


        private void InvalidateCache(string methodName)
        {
            _cache.Remove(methodName); // Invalidate cache for the specified method
        }

        public async Task<PaginationViewSupplier> GetAllPagination(int page, int pagecount, string searchTerm, string sort = null, string order = null)
        {
            var cacheKey = $"{nameof(PaginationViewSupplier)}";
            // Check if the result is already in the cache

            IQueryable<VwListSupplier> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewSupplier cachedResult))
            {

                query = cachedResult.IQueryData.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<VwListSupplier, object>> defaultSort1 = x => x.ID;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<VwListSupplier>.Search([.. query], searchTerm, c => c.Company, c => c.Email, c => c.Tel, c => c.Mobile);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewSupplier
                       {

                           Delete_By = a.Delete_By,
                           Delete_Date = a.Delete_Date,
                           Edit_By = a.Edit_By,
                           Edit_Date = a.Edit_Date,
                           ID = a.ID,
                           Company = a.Company,
                           Address = a.Address,
                           AdvanceCreditorLedger = a.AdvanceCreditorLedger,
                           Contact = a.Contact,
                           CreditorLedger = a.CreditorLedger,
                           Email = a.Email,
                           Fax = a.Fax,
                           IsDelete = a.IsDelete,
                           LedgerCode = a.LedgerCode,
                           Mobile = a.Mobile,
                           Tel = a.Tel,
                           Type = a.Type,
                           ImageURl = a.ImageURl,
                       })
                       .ToList();

                // Prepare the pagination response

                var paginationListData1 = new PaginationViewSupplier
                {
                    Count = filteredData1.Count,
                    ViewSuppliers = pagedData1.Skip((page - 1) * pagecount).Take(pagecount).ToList()
                };

                return paginationListData1;
            }
            else
            {
                query = _context.VwListSuppliers.AsQueryable();

            }

            // Apply sorting based on sort and order parameters

            Expression<Func<VwListSupplier, object>> defaultSort = x => x.ID;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => !d.IsDelete).ToListAsync();

            filteredData = QueryHelper.SearchUtility<VwListSupplier>.Search([.. query], searchTerm, c => c.Company, c => c.Email, c => c.Tel, c => c.Mobile);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewSupplier
                {

                    Delete_By = a.Delete_By,
                    Delete_Date = a.Delete_Date,
                    Edit_By = a.Edit_By,
                    Edit_Date = a.Edit_Date,
                    ID = a.ID,
                    Company = a.Company,
                    Address = a.Address,
                    AdvanceCreditorLedger = a.AdvanceCreditorLedger,
                    Contact = a.Contact,
                    CreditorLedger = a.CreditorLedger,
                    Email = a.Email,
                    Fax = a.Fax,
                    IsDelete = a.IsDelete,
                    LedgerCode = a.LedgerCode,
                    Mobile = a.Mobile,
                    Tel = a.Tel,
                    Type = a.Type,
                    ImageURl = a.ImageURl,
                })
                .ToList();

            // Prepare the pagination response

            var paginationListData = new PaginationViewSupplier
            {
                Count = filteredData.Count,
                ViewSuppliers = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList()
            };

            // Cache the result

            var CacheResult = new PaginationViewSupplier
            {
                Count = filteredData.Count,
                IQueryData = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;

        }

        public async Task<Message<string>> RegisterAsync(InsertSupplier model)
        {

            try
            {
                var transactionscope = _context.Database.BeginTransaction();


                var ChecktheisthereExsitingCustomerName = GetByName(model.Company);

                if (model.Image != null && model.Image.Length > 0)
                {
                    if (model.ImageURl != "Select.png" && model.ImageURl != null)
                    {
                        File.Delete(CommonResources.System_File_Path + "/Supplier/" + model.ImageURl);
                    }

                    var type = model.Image.FileName.Split(".").Last();
                    model.ImageURl = string.Format("{0}.{1}", Guid.NewGuid().ToString(), type);
                    using (var fileStream = new FileStream(string.Format("{0}/{1}", CommonResources.System_File_Path + "/Supplier/", model.ImageURl), FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    };
                }
                else model.ImageURl ??= "Select.png";
                if (ChecktheisthereExsitingCustomerName == null)
                {
                    var tblSupplier = new TblSupplier
                    {
                        Company = model.Company,
                        LedgerCode = model.LedgerCode,
                        Mobile = model.Mobile,
                        Tel = model.Tel,
                        Address = model.Address,
                        AdvanceCreditorLedger = model.AdvanceCreditorLedger,
                        Contact = model.Contact,
                        CreditorLedger = model.CreditorLedger,
                        Email = model.Email,
                        Fax = model.Fax,
                        Type = model.Type,
                        Edit_By = model.Edit_By,
                        Edit_Date = model.Edit_Date,
                        ImageURl = model.ImageURl,
                    };
                    _context.TblSuppliers.Add(tblSupplier);
                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblSuppliers", "Insert", $"Insert {model.Company + ' '} Name.", model.Edit_By);

                    transactionscope.Commit();

                    InvalidateCache(nameof(PaginationViewSupplier));
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This Supplier has been registered",
                    };
                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This Supplier Name has been already registered"
                    };
                }

            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }

        public TblSupplier GetById(int id)
        {
            return _context.TblSuppliers.SingleOrDefault(d => d.ID.Equals(id));
        }

        public TblSupplier GetByName(string Name)
        {
            return _context.TblSuppliers.SingleOrDefault(d => d.Company.Equals(Name));
        }

        public async Task<Message<string>> UpdateAsync(UpdateSupplier model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                if (model.Image != null && model.Image.Length > 0)
                {
                    if (model.ImageURl != "Select.png" && model.ImageURl != null)
                    {
                        File.Delete(CommonResources.System_File_Path + "/Supplier/" + model.ImageURl);
                    }

                    var type = model.Image.FileName.Split(".").Last();
                    model.ImageURl = string.Format("{0}.{1}", Guid.NewGuid().ToString(), type);
                    using (var fileStream = new FileStream(string.Format("{0}/{1}", CommonResources.System_File_Path + "/Supplier/", model.ImageURl), FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    };
                }
                else
                {
                    if (model.ImageURl != null)
                    {
                        model.ImageURl = model.ImageURl;
                    }
                    else model.ImageURl ??= "Select.png";

                }


                var exist = GetById(model.ID);
                exist.Address = model.Address;
                exist.CreditorLedger = model.CreditorLedger;
                exist.AdvanceCreditorLedger = model.AdvanceCreditorLedger;
                exist.LedgerCode = model.LedgerCode;
                exist.Company = model.Company;
                exist.Contact = model.Contact;
                exist.Edit_By = model.Edit_By;
                exist.Edit_Date = model.Edit_Date;
                exist.Tel = model.Tel;
                exist.Type = model.Type;
                exist.Email = model.Email;
                exist.Mobile = model.Mobile;
                exist.ImageURl = model.ImageURl;

                await _context.SaveChangesAsync();


                // Log audit trail
                _auditTrailService.LogAudit("TblSuppliers", "Update", $"Update {model.Company + ' '} name.", model.Edit_By);

                transactionscope.Commit();
                InvalidateCache(nameof(PaginationViewSupplier));
                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Supplier has been updated successfully",
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Update error {ex.Message}" };
            }


        }

        public async Task<Message<string>> DeleteAsync(DeleteSupplier model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existClient = GetById(model.ID);

                if (existClient.IsDelete == false)
                {
                    existClient.IsDelete = true;
                    existClient.Delete_By = model.Delete_By;
                    existClient.Delete_Date = model.Delete_Date;

                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblSupplier", "Delete", $"Delete this {model.Company + ' ' + model.ID} name.", model.Edit_By);

                    await _context.SaveChangesAsync();

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewSupplier));
                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"Supplier has been already deleted",
                    };
                }

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Supplier details have been deleted successfully"
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Delete error {ex.Message}" };
            }
        }
        public async Task<ViewSupplier> GetDetailsById(int id)
        {
            return await (from a in _context.TblSuppliers
                          select new ViewSupplier()
                          {
                              ID = a.ID,
                              Company = a.Company,
                              Address = a.Address,
                              AdvanceCreditorLedger = a.AdvanceCreditorLedger,
                              Contact = a.Contact,
                              CreditorLedger = a.CreditorLedger,
                              Email = a.Email,
                              Fax = a.Fax,
                              IsDelete = a.IsDelete,
                              LedgerCode = a.LedgerCode,
                              Mobile = a.Mobile,
                              Tel = a.Tel,
                              Type = a.Type,
                              ImageURl = a.ImageURl,
                          }).SingleOrDefaultAsync(d => d.ID.Equals(id));

        }




    }
}
