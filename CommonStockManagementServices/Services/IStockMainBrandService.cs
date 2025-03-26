using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Model;
using System.Linq.Expressions;

namespace CommonStockManagementServices.Services
{
    public class StockMainBrandService(AppDbContext context, AuditTrailService auditTrailService, IMemoryCache cache)
    {
        private readonly AppDbContext _context = context;
        private readonly AuditTrailService _auditTrailService = auditTrailService;
        private readonly IMemoryCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));


        private void InvalidateCache(string methodName)
        {
            _cache.Remove(methodName); // Invalidate cache for the specified method
        }


        public async Task<PaginationViewStockMainItemBrand> GetAllPagination(int page, int pagecount, string searchTerm, string sort = null, string order = null)
        {
            var cacheKey = $"{nameof(PaginationViewStockMainItemBrand)}";
            // Check if the result is already in the cache

            IQueryable<VwListItemBrand> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewStockMainItemBrand cachedResult))
            {

                query = cachedResult.IQueryData.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<VwListItemBrand, object>> defaultSort1 = x => x.Id;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<VwListItemBrand>.Search([.. query], searchTerm, c => c.Name, c => c.Description);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewStockMainItemBrand
                       {

                           Delete_By = a.Delete_By,
                           Delete_Date = a.Delete_Date,
                           Edit_By = a.Edit_By,
                           Edit_Date = a.Edit_Date,
                           Id = a.Id,
                           Name = a.Name,
                           Description = a.Description,
                           IsDelete = a.IsDelete,
                       })
                       .ToList();

                // Prepare the pagination response

                var paginationListData1 = new PaginationViewStockMainItemBrand
                {
                    Count = filteredData1.Count,
                    ViewStockMainItemBrands = pagedData1.Skip((page - 1) * pagecount).Take(pagecount).ToList()
                };

                return paginationListData1;
            }
            else
            {
                query = _context.VwListItemBrand.AsQueryable();

            }

            // Apply sorting based on sort and order parameters

            Expression<Func<VwListItemBrand, object>> defaultSort = x => x.Id;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => !d.IsDelete).ToListAsync();

            filteredData = QueryHelper.SearchUtility<VwListItemBrand>.Search(filteredData, null, c => c.Name, c => c.Description);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewStockMainItemBrand
                {

                    Delete_By = a.Delete_By,
                    Delete_Date = a.Delete_Date,
                    Edit_By = a.Edit_By,
                    Edit_Date = a.Edit_Date,
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    IsDelete = a.IsDelete,
                })
                .ToList();

            // Prepare the pagination response

            var paginationListData = new PaginationViewStockMainItemBrand
            {
                Count = filteredData.Count,
                ViewStockMainItemBrands = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList()
            };

            // Cache the result

            var CacheResult = new PaginationViewStockMainItemBrand
            {
                Count = filteredData.Count,
                IQueryData = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;

        }

        public async Task<PaginationViewStockMainItemBrand> GetAllPaginationWithoutPagination()
        {

            var cacheKey = $"{nameof(PaginationViewStockMainItemBrand)}";
            // Check if the result is already in the cache

            IQueryable<VwListItemBrand> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewStockMainItemBrand cachedResult))
            {

                query = cachedResult.IQueryData.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<VwListItemBrand, object>> defaultSort1 = x => x.Id;

                query = QueryHelper.ApplySort(query, null, null, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<VwListItemBrand>.Search([.. query], null, c => c.Name, c => c.Description);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewStockMainItemBrand
                       {

                           Delete_By = a.Delete_By,
                           Delete_Date = a.Delete_Date,
                           Edit_By = a.Edit_By,
                           Edit_Date = a.Edit_Date,
                           Id = a.Id,
                           Name = a.Name,
                           Description = a.Description,
                           IsDelete = a.IsDelete,
                       })
                       .ToList();

                // Prepare the pagination response

                var paginationListData1 = new PaginationViewStockMainItemBrand
                {
                    Count = filteredData1.Count,
                    ViewStockMainItemBrands = pagedData1
                };

                return paginationListData1;
            }
            else
            {
                query = _context.VwListItemBrand.AsQueryable();

            }

            // Apply sorting based on sort and order parameters

            Expression<Func<VwListItemBrand, object>> defaultSort = x => x.Id;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => !d.IsDelete).ToListAsync();

            filteredData = QueryHelper.SearchUtility<VwListItemBrand>.Search(filteredData, null, c => c.Name, c => c.Description);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewStockMainItemBrand
                {

                    Delete_By = a.Delete_By,
                    Delete_Date = a.Delete_Date,
                    Edit_By = a.Edit_By,
                    Edit_Date = a.Edit_Date,
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    IsDelete = a.IsDelete,
                })
                .ToList();

            // Prepare the pagination response

            var paginationListData = new PaginationViewStockMainItemBrand
            {
                Count = filteredData.Count,
                ViewStockMainItemBrands = pagedData
            };

            // Cache the result

            var CacheResult = new PaginationViewStockMainItemBrand
            {
                Count = filteredData.Count,
                IQueryData = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;
        }

        public async Task<Message<string>> RegisterAsync(InsertStockMainItemBrand model)
        {

            try
            {
                var transactionscope = _context.Database.BeginTransaction();


                var ChecktheisthereExsitingStockMainName = GetByName(model.Name);



                if (ChecktheisthereExsitingStockMainName == null)
                {
                    var main = new TblItemBrandName
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Edit_By = model.Edit_By,
                        Edit_Date = model.Edit_Date,
                    };
                    _context.TblItemBrandNames.Add(main);
                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblItemBrandName", "Insert", $"Insert {model.Name + ' '} Name.", model.Edit_By);

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewStockMainItemBrand));
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This Item Brand has been registered",
                    };
                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This Item Brand has been already registered"
                    };
                }

            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }

        public TblItemBrandName GetById(int id)
        {
            return _context.TblItemBrandNames.SingleOrDefault(d => d.Id.Equals(id));
        }

        public TblItemBrandName GetByName(string Name)
        {
            return _context.TblItemBrandNames.SingleOrDefault(d => d.Name.Equals(Name));
        }

        public async Task<Message<string>> UpdateAsync(UpdateStockMainItemBrand model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();


                var exist = GetById(model.Id);
                exist.Edit_By = model.Edit_By;
                exist.Edit_Date = model.Edit_Date;
                exist.Name = model.Name;
                exist.Description = model.Description;

                await _context.SaveChangesAsync();


                // Log audit trail
                _auditTrailService.LogAudit("TblItemBrandName", "Update", $"Update {model.Name + ' '} name.", model.Edit_By);

                transactionscope.Commit();
                InvalidateCache(nameof(PaginationViewStockMainItemBrand));
                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Item Brand has been updated successfully",
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Update error {ex.Message}" };
            }


        }

        public async Task<Message<string>> DeleteAsync(DeleteStockMainItemBrand model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existClient = GetById(model.Id);

                if (existClient.IsDelete == false)
                {
                    existClient.IsDelete = true;
                    existClient.Delete_By = model.Delete_By;
                    existClient.Delete_Date = model.Delete_Date;

                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblItemBrandName", "Delete", $"Delete this {model.Name + ' ' + model.Id} name.", model.Edit_By);

                    await _context.SaveChangesAsync();

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewStockMainItemBrand));
                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"Item Brand has been already deleted",
                    };
                }

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Item Brand has been deleted successfully"
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Delete error {ex.Message}" };
            }
        }
        public async Task<ViewStockMainItemBrand> GetDetailsById(int id)
        {
            return await (from a in _context.TblItemBrandNames
                          select new ViewStockMainItemBrand()
                          {
                              Id = a.Id,
                              Name = a.Name,
                              Description = a.Description,
                          }).SingleOrDefaultAsync(d => d.Id.Equals(id));

        }



    }
}
