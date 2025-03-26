using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Model;
using System.Linq.Expressions;

namespace CommonStockManagementServices.Services
{

    public class DatabaseBackupServices(AppDbContext context, AuditTrailService auditTrailService, IMemoryCache cache)
    {
        private readonly AppDbContext _context = context;
        private readonly AuditTrailService _auditTrailService = auditTrailService;
        private readonly IMemoryCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));


        private void InvalidateCache(string methodName)
        {
            _cache.Remove(methodName); // Invalidate cache for the specified method
        }


        public async Task<PaginationViewDatabaseBackup> GetAllPagination(int page, int pagecount, string searchTerm, string sort = null, string order = null)
        {
            var cacheKey = $"{nameof(PaginationViewDatabaseBackup)}";
            // Check if the result is already in the cache

            IQueryable<ViewDatabaseBackup> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewDatabaseBackup cachedResult))
            {

                query = cachedResult.ViewDatabaseBackup.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<ViewDatabaseBackup, object>> defaultSort1 = x => x.ID;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<ViewDatabaseBackup>.Search([.. query], searchTerm, null);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewDatabaseBackup
                       {
                           Delete_By = a.Delete_By,
                           Delete_Date = a.Delete_Date,
                           Edit_By = a.Edit_By,
                           Edit_Date = a.Edit_Date,
                           IsDelete = a.IsDelete,
                           DatabaseName = a.DatabaseName,
                           Date = a.Date,
                           Time = a.Time,
                           ID = a.ID,
                           Reason = a.Reason,
                           TagDiscription = a.TagDiscription,
                           UserName = a.UserName,
                           Avatar = a.Avatar
                       })
                       .ToList();

                // Prepare the pagination response

                var paginationListData1 = new PaginationViewDatabaseBackup
                {
                    Count = filteredData1.Count,
                    ViewDatabaseBackup = pagedData1.Skip((page - 1) * pagecount).Take(pagecount).ToList()
                };

                return paginationListData1;
            }
            else
            {
                query = (from x in _context.TblDatabaseBackupHistory
                         join c in _context.Users on x.Edit_By equals c.Id
                         select new ViewDatabaseBackup()
                         {
                             TagDiscription = x.TagDiscription,
                             Reason = x.Reason,
                             ID = x.ID,
                             DatabaseName = x.DatabaseName,
                             Time = x.DateTime.ToString("hh.mm : tt").ToLower(),
                             IsDelete = x.IsDelete,
                             Date = x.DateTime.ToString("yyyy-MMM-dd"),
                             Delete_By = x.Delete_By,
                             Delete_Date = x.Delete_Date,
                             Edit_By = x.Edit_By,
                             Edit_Date = x.Edit_Date,
                             UserName = x.UserName,
                             Avatar = c.ImageURl
                         }).AsQueryable();

            }

            // Apply sorting based on sort and order parameters

            Expression<Func<ViewDatabaseBackup, object>> defaultSort = x => x.ID;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => !d.IsDelete).ToListAsync();

            filteredData = QueryHelper.SearchUtility<ViewDatabaseBackup>.Search(filteredData, null, null);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewDatabaseBackup
                {
                    Delete_By = a.Delete_By,
                    Delete_Date = a.Delete_Date,
                    Edit_By = a.Edit_By,
                    Edit_Date = a.Edit_Date,
                    IsDelete = a.IsDelete,
                    DatabaseName = a.DatabaseName,
                    Date = a.Date,
                    Time = a.Time,
                    ID = a.ID,
                    Reason = a.Reason,
                    TagDiscription = a.TagDiscription,
                    UserName = a.UserName,
                    Avatar = a.Avatar
                })
                .ToList();

            // Prepare the pagination response

            var paginationListData = new PaginationViewDatabaseBackup
            {
                Count = filteredData.Count,
                ViewDatabaseBackup = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList()
            };

            // Cache the result

            var CacheResult = new PaginationViewDatabaseBackup
            {
                Count = filteredData.Count,
                ViewDatabaseBackup = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;

        }


        public async Task<Message<string>> RegisterAsync(InsertDatabaseBackup model)
        {

            try
            {
                var transactionscope = _context.Database.BeginTransaction();


                var main = new TblDatabaseBackupHistory
                {
                    DatabaseName = model.DatabaseName,
                    DateTime = Convert.ToDateTime(model.Date),
                    Reason = model.Reason,
                    TagDiscription = model.TagDiscription,
                    Edit_By = model.Edit_By,
                    Edit_Date = model.Edit_Date,
                    UserName = model.UserName,
                };
                _context.TblDatabaseBackupHistory.Add(main);
                await _context.SaveChangesAsync();

                // Log audit trail
                _auditTrailService.LogAudit("TblDatabaseBackupHistory", "Insert", $"Insert {model.Reason + ' '} Name.", model.Edit_By);

                transactionscope.Commit();
                InvalidateCache(nameof(PaginationViewDatabaseBackup));

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"This Database Backup  has been registered",
                };

            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }

        public TblDatabaseBackupHistory GetById(int id)
        {
            return _context.TblDatabaseBackupHistory.SingleOrDefault(d => d.ID.Equals(id));
        }


        public async Task<Message<string>> DeleteAsync(DeleteDatabaseBackup model)
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
                    _auditTrailService.LogAudit("TblDatabaseBackupHistory", "Delete", $"Delete this {model.Reason + ' ' + model.ID} name.", model.Edit_By);

                    await _context.SaveChangesAsync();

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewDatabaseBackup));
                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"Database Backup  has been already deleted",
                    };
                }

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Database Backup  has been deleted successfully"
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Delete error {ex.Message}" };
            }
        }
        public async Task<ViewDatabaseBackup> GetDetailsById(int id)
        {
            return await (from x in _context.TblDatabaseBackupHistory
                          select new ViewDatabaseBackup()
                          {
                              TagDiscription = x.TagDiscription,
                              Reason = x.Reason,
                              ID = x.ID,
                              DatabaseName = x.DatabaseName,
                              Time = x.DateTime.ToString("yyyy-MM-dd"),
                              IsDelete = x.IsDelete,
                              Date = x.DateTime.ToString("yyyy-MM-dd"),
                              Delete_By = x.Delete_By,
                              Delete_Date = x.Delete_Date,
                              Edit_By = x.Edit_By,
                              Edit_Date = x.Edit_Date,
                              UserName = x.UserName,
                          }).SingleOrDefaultAsync(d => d.ID.Equals(id));

        }



    }
}
