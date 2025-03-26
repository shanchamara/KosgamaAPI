using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Model;
using System;
using System.Linq.Expressions;

namespace CommonStockManagementServices.Services
{

    public class CompanyDetailsServices(AppDbContext context, AuditTrailService auditTrailService, IMemoryCache cache)
    {
        private readonly AppDbContext _context = context;
        private readonly AuditTrailService _auditTrailService = auditTrailService;
        private readonly IMemoryCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));

        private void InvalidateCache(string methodName)
        {
            _cache.Remove(methodName); // Invalidate cache for the specified method
        }

        public async Task<PaginationViewCompanyDetails> GetAllPagination(int page, int pagecount, string searchTerm, string sort = null, string order = null)
        {
            var cacheKey = $"{nameof(PaginationViewCompanyDetails)}";
            // Check if the result is already in the cache

            if (_cache.TryGetValue(cacheKey, out PaginationViewCompanyDetails cachedResult))
            {
                return GetPaginatedResult(cachedResult.ViewCompanyDetails.AsQueryable(), page, pagecount, searchTerm, sort, order);
            }
            else
            {
                // Project the filtered data to 
                var pagedData1 = await (from a in _context.TblCompanyDetails
                                        select new ViewCompanyDetails()
                                        {
                                            Address = a.Address,
                                            CompanyName = a.CompanyName,
                                            Delete_By = a.Delete_By,
                                            Delete_Date = a.Delete_Date,
                                            Edit_By = a.Edit_By,
                                            Edit_Date = a.Edit_Date,
                                            Id = a.Id,
                                            Isdelete = a.Isdelete,
                                            TelPhone1 = a.TelPhone1,
                                            TelPhone2 = a.TelPhone2,
                                        }).ToListAsync();

                // Prepare the pagination response

                var query = pagedData1.AsQueryable();

                var cacheResult = new PaginationViewCompanyDetails
                {
                    Count = query.ToList().Count,
                    ViewCompanyDetails = [.. query]
                };
                _cache.Set(cacheKey, cacheResult, TimeSpan.FromMinutes(30));

                return GetPaginatedResult(query, page, pagecount, searchTerm, sort, order);
            }
        }


        private static PaginationViewCompanyDetails GetPaginatedResult(IQueryable<ViewCompanyDetails> query, int page, int pagecount, string searchTerm, string sort, string order)
        {
            Expression<Func<ViewCompanyDetails, object>> defaultSort = x => x.Id;
            query = QueryHelper.ApplySort(query, sort, order, defaultSort);

            ////bool searchable = active == "active" ? false : true;

            //bool? isActive = active switch
            //{
            //    "active" => true,
            //    "inactive" => false,
            //    _ => null
            //};

            var filteredData = query.Where(d => /*isActive == null ||*/ d.Isdelete.Equals(false)).ToList();

            filteredData = QueryHelper.SearchUtility<ViewCompanyDetails>.Search([.. filteredData], searchTerm, c => c.CompanyName, c => c.Address).ToList();

            List<ViewCompanyDetails> pagedData;
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


            return new PaginationViewCompanyDetails
            {
                Count = query.Count(),
                ViewCompanyDetails = pagedData
            };
        }


        public async Task<Message<string>> RegisterAsync(InsertCompanyDetails model)
        {

            try
            {
                var transactionscope = _context.Database.BeginTransaction();


                var ChecktheisthereExsitingStockMainName = GetByName(model.CompanyName);



                if (ChecktheisthereExsitingStockMainName == null)
                {
                    var main = new TblCompanyDetails
                    {
                        CompanyName = model.CompanyName,
                        Address = model.Address,
                        TelPhone2 = model.TelPhone2,
                        TelPhone1 = model.TelPhone1,
                        Edit_By = model.Edit_By,
                        Edit_Date = model.Edit_Date,
                    };
                    _context.TblCompanyDetails.Add(main);
                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblCompanyDetails", "Insert", $"Insert {model.CompanyName + ' '} Name.", model.Edit_By);
                    InvalidateCache(nameof(PaginationViewCompanyDetails));
                    transactionscope.Commit();

                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This Company Details has been registered",
                    };
                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This Company Details has been already registered"
                    };
                }

            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }

        public TblCompanyDetails GetById(int id)
        {
            return _context.TblCompanyDetails.SingleOrDefault(d => d.Id.Equals(id));
        }

        public TblCompanyDetails GetByName(string Name)
        {
            return _context.TblCompanyDetails.SingleOrDefault(d => d.CompanyName.Equals(Name));
        }

        public async Task<Message<string>> UpdateAsync(UpdateCompanyDetailsts model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();


                var exist = GetById(model.Id);
                exist.Edit_By = model.Edit_By;
                exist.Edit_Date = model.Edit_Date;
                exist.CompanyName = model.CompanyName;
                exist.Address = model.Address;
                exist.TelPhone2 = model.TelPhone2;
                exist.TelPhone1 = model.TelPhone1;

                await _context.SaveChangesAsync();

                InvalidateCache(nameof(PaginationViewCompanyDetails));
                // Log audit trail
                _auditTrailService.LogAudit("TblCompanyDetails", "Update", $"Update {model.CompanyName + ' '} name.", model.Edit_By);

                transactionscope.Commit();


                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Company Details has been updated successfully",
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Update error {ex.Message}" };
            }


        }

        public async Task<Message<string>> DeleteAsync(DeleteCompanyDetails model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existClient = GetById(model.Id);

                if (existClient.Isdelete == false)
                {
                    existClient.Isdelete = true;
                    existClient.Delete_By = model.Delete_By;
                    existClient.Delete_Date = model.Delete_Date;

                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblCompanyDetails", "Delete", $"Delete this {model.CompanyName + ' ' + model.Id} name.", model.Edit_By);

                    await _context.SaveChangesAsync();

                    transactionscope.Commit();

                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"Company Details has been already deleted",
                    };
                }

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Company Details has been deleted successfully"
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Delete error {ex.Message}" };
            }
        }
        public async Task<ViewCompanyDetails> GetDetailsById(int id)
        {
            return await (from a in _context.TblCompanyDetails
                          select new ViewCompanyDetails()
                          {
                              Address = a.Address,
                              CompanyName = a.CompanyName,
                              Delete_By = a.Delete_By,
                              Delete_Date = a.Delete_Date,
                              Edit_By = a.Edit_By,
                              Edit_Date = a.Edit_Date,
                              Id = a.Id,
                              Isdelete = a.Isdelete,
                              TelPhone1 = a.TelPhone1,
                              TelPhone2 = a.TelPhone2,
                          }).SingleOrDefaultAsync(d => d.Id.Equals(id));

        }



    }
}
