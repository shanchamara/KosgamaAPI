using CommonStockManagementDatabase.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Model;
using System.Linq.Expressions;

namespace CommonStockManagementServices.Services
{

    public class ClientService(AppDbContext context, AuditTrailService auditTrailService, IMemoryCache cache)
    {
        private readonly AppDbContext _context = context;
        private readonly AuditTrailService _auditTrailService = auditTrailService;
        private readonly IMemoryCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));

        private void InvalidateCache(string methodName)
        {
            _cache.Remove(methodName); // Invalidate cache for the specified method
        }

        public async Task<PaginationViewClient> GetAllPagination(int page, int pagecount, string searchTerm, string sort = null, string order = null)
        {

            var cacheKey = $"{nameof(PaginationViewClient)}";
            // Check if the result is already in the cache

            IQueryable<VWListClient> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewClient cachedResult))
            {

                query = cachedResult.IQueryData.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<VWListClient, object>> defaultSort1 = x => x.ID;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<VWListClient>.Search([.. query], searchTerm, c => c.FirstName, c => c.Email, c => c.Tel, c => c.Mobile, c => c.LastName);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewClient
                       {

                           Delete_By = a.Delete_By,
                           Delete_Date = a.Delete_Date,
                           Edit_By = a.Edit_By,
                           Edit_Date = a.Edit_Date,
                           ID = a.ID,
                           Email = a.Email,
                           LastName = a.LastName,
                           FirstName = a.FirstName,
                           Address = a.Address,
                           Cr = a.Cr,
                           Dr = a.Dr,
                           Mobile = a.Mobile,
                           Tel = a.Tel,
                           ImageURl = a.ImageURl,
                           Isdelete = a.Isdelete,
                           Nic = a.Nic,
                           Title = a.Title,
                           Type = a.Type,

                       })
                       .ToList();

                // Prepare the pagination response

                var paginationListData1 = new PaginationViewClient
                {
                    Count = filteredData1.Count,
                    ViewClients = pagedData1.Skip((page - 1) * pagecount).Take(pagecount).ToList()
                };

                return paginationListData1;
            }
            else
            {
                query = _context.VWListClients.AsQueryable();

            }

            // Apply sorting based on sort and order parameters

            Expression<Func<VWListClient, object>> defaultSort = x => x.ID;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => !d.Isdelete).ToListAsync();

            filteredData = QueryHelper.SearchUtility<VWListClient>.Search([.. query], searchTerm, c => c.FirstName, c => c.Email, c => c.Tel, c => c.Mobile, c => c.LastName);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewClient
                {

                    Delete_By = a.Delete_By,
                    Delete_Date = a.Delete_Date,
                    Edit_By = a.Edit_By,
                    Edit_Date = a.Edit_Date,
                    ID = a.ID,
                    Email = a.Email,
                    LastName = a.LastName,
                    FirstName = a.FirstName,
                    Address = a.Address,
                    Cr = a.Cr,
                    Dr = a.Dr,
                    Mobile = a.Mobile,
                    Tel = a.Tel,
                    ImageURl = a.ImageURl,
                    Isdelete = a.Isdelete,
                    Nic = a.Nic,
                    Title = a.Title,
                    Type = a.Type,
                })
                .ToList();

            // Prepare the pagination response

            var paginationListData = new PaginationViewClient
            {
                Count = filteredData.Count,
                ViewClients = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList()
            };

            // Cache the result

            var CacheResult = new PaginationViewClient
            {
                Count = filteredData.Count,
                IQueryData = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;
        }

        public async Task<PaginationViewClient> GetAllWithOutPagination()
        {

            var cacheKey = $"{nameof(PaginationViewClient)}";
            // Check if the result is already in the cache

            IQueryable<VWListClient> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewClient cachedResult))
            {

                query = cachedResult.IQueryData.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<VWListClient, object>> defaultSort1 = x => x.ID;

                query = QueryHelper.ApplySort(query, null, null, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<VWListClient>.Search([.. query], null, c => c.FirstName, c => c.Email, c => c.Tel, c => c.Mobile, c => c.LastName);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewClient
                       {

                           Delete_By = a.Delete_By,
                           Delete_Date = a.Delete_Date,
                           Edit_By = a.Edit_By,
                           Edit_Date = a.Edit_Date,
                           ID = a.ID,
                           Email = a.Email,
                           LastName = a.LastName,
                           FirstName = a.FirstName,
                           Address = a.Address,
                           Cr = a.Cr,
                           Dr = a.Dr,
                           Mobile = a.Mobile,
                           Tel = a.Tel,
                           ImageURl = a.ImageURl,
                           Isdelete = a.Isdelete,
                           Nic = a.Nic,
                           Title = a.Title,
                           Type = a.Type,

                       })
                       .ToList();

                // Prepare the pagination response

                var paginationListData1 = new PaginationViewClient
                {
                    Count = filteredData1.Count,
                    ViewClients = pagedData1
                };

                return paginationListData1;
            }
            else
            {
                query = _context.VWListClients.AsQueryable();

            }

            // Apply sorting based on sort and order parameters

            Expression<Func<VWListClient, object>> defaultSort = x => x.ID;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => !d.Isdelete).ToListAsync();

            filteredData = QueryHelper.SearchUtility<VWListClient>.Search([.. query], null, c => c.FirstName, c => c.Email, c => c.Tel, c => c.Mobile, c => c.LastName);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewClient
                {

                    Delete_By = a.Delete_By,
                    Delete_Date = a.Delete_Date,
                    Edit_By = a.Edit_By,
                    Edit_Date = a.Edit_Date,
                    ID = a.ID,
                    Email = a.Email,
                    LastName = a.LastName,
                    FirstName = a.FirstName,
                    Address = a.Address,
                    Cr = a.Cr,
                    Dr = a.Dr,
                    Mobile = a.Mobile,
                    Tel = a.Tel,
                    ImageURl = a.ImageURl,
                    Isdelete = a.Isdelete,
                    Nic = a.Nic,
                    Title = a.Title,
                    Type = a.Type,
                })
                .ToList();

            // Prepare the pagination response

            var paginationListData = new PaginationViewClient
            {
                Count = filteredData.Count,
                ViewClients = pagedData
            };

            // Cache the result

            var CacheResult = new PaginationViewClient
            {
                Count = filteredData.Count,
                IQueryData = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;
        }

        public async Task<Message<string>> RegisterAsync(InsertClient model)
        {

            try
            {

                var transactionscope = _context.Database.BeginTransaction();


                var ChecktheisthereExsitingCustomerName = GetByName(model.FirstName, model.LastName);

                if (model.Image != null && model.Image.Length > 0)
                {
                    if (model.ImageURl != "Select.png" && model.ImageURl != null)
                    {
                        File.Delete(CommonResources.System_File_Path + "/Customer/" + model.ImageURl);
                    }

                    var type = model.Image.FileName.Split(".").Last();
                    model.ImageURl = string.Format("{0}.{1}", Guid.NewGuid().ToString(), type);
                    using (var fileStream = new FileStream(string.Format("{0}/{1}", CommonResources.System_File_Path + "/Customer/", model.ImageURl), FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    };
                }
                else model.ImageURl ??= "Select.png";

                if (ChecktheisthereExsitingCustomerName == null)
                {
                    var TblClient = new TblClient
                    {
                        Address = model.Address,
                        Tel = model.Tel,
                        Title = model.Title,
                        Nic = model.Nic,
                        Dr = model.Dr,
                        Cr = model.Cr,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        ImageURl = model.ImageURl,
                        Mobile = model.Mobile,
                        Type = model.Type,
                        Edit_By = model.Edit_By,
                        Edit_Date = model.Edit_Date,
                        RegistrationDate = CommonResources.LocalDatetime().Date
                    };
                    _context.TblClients.Add(TblClient);
                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblClients", "Insert", $"Insert {model.FirstName + ' ' + model.LastName} Name.", model.Edit_By);

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewClient));
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This Client has been registered",
                    };
                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This Client Name has been already registered"
                    };
                }

            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }

        public TblClient GetById(int id)
        {
            return _context.TblClients.SingleOrDefault(d => d.ID.Equals(id));
        }

        public TblClient GetByName(string Name, string LastName)
        {
            return _context.TblClients.SingleOrDefault(d => d.FirstName.Equals(Name) && d.LastName.Equals(LastName));
        }

        public async Task<Message<string>> UpdateAsync(UpdateClient model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                if (model.Image != null && model.Image.Length > 0)
                {
                    if (model.ImageURl != "Select.png" && model.ImageURl != null)
                    {
                        File.Delete(CommonResources.System_File_Path + "/Customer/" + model.ImageURl);
                    }

                    var type = model.Image.FileName.Split(".").Last();
                    model.ImageURl = string.Format("{0}.{1}", Guid.NewGuid().ToString(), type);
                    using (var fileStream = new FileStream(string.Format("{0}/{1}", CommonResources.System_File_Path + "/Customer/", model.ImageURl), FileMode.Create))
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
                    else
                        model.ImageURl ??= "Select.png";

                }


                var existingClient = GetById(model.ID);
                existingClient.Address = model.Address;
                existingClient.Tel = model.Tel;
                existingClient.Title = model.Title;
                existingClient.Nic = model.Nic;
                existingClient.Dr = model.Dr;
                existingClient.Cr = model.Cr;
                existingClient.Email = model.Email;
                existingClient.FirstName = model.FirstName;
                existingClient.LastName = model.LastName;
                existingClient.ImageURl = model.ImageURl;
                existingClient.Mobile = model.Mobile;
                existingClient.Type = model.Type;
                existingClient.Edit_By = model.Edit_By;
                existingClient.Edit_Date = model.Edit_Date;

                await _context.SaveChangesAsync();


                // Log audit trail
                _auditTrailService.LogAudit("TblClients", "Update", $"Update {model.FirstName + ' ' + model.LastName} name.", model.Edit_By);

                transactionscope.Commit();
                InvalidateCache(nameof(PaginationViewClient));
                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Client has been updated successfully",
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Update error {ex.Message}" };
            }


        }

        public async Task<Message<string>> DeleteAsync(DeleteClient model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                var existClient = GetById(model.ID);

                if (existClient.Isdelete == false)
                {
                    if (model.ImageURl != "Select.png" && model.ImageURl != null)
                    {
                        File.Delete(CommonResources.System_File_Path + "/Customer/" + model.ImageURl);
                    }

                    existClient.Isdelete = true;
                    existClient.Delete_By = model.Delete_By;
                    existClient.Delete_Date = model.Delete_Date;

                    await _context.SaveChangesAsync();

                    // Log audit trail
                    _auditTrailService.LogAudit("TblClient", "Delete", $"Delete this {model.FirstName + ' ' + model.LastName} name.", model.Edit_By);

                    await _context.SaveChangesAsync();

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewClient));
                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"Client has been already deleted",
                    };
                }

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Client details have been deleted successfully"
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Delete error {ex.Message}" };
            }
        }
        public async Task<ViewClient> GetDetailsById(int id)
        {
            var dr = await (from a in _context.TblClients
                            select new ViewClient()
                            {
                                ID = a.ID,
                                Email = a.Email,
                                LastName = a.LastName,
                                FirstName = a.FirstName,
                                Address = a.Address,
                                Cr = a.Cr,
                                Dr = a.Dr,
                                Mobile = a.Mobile,
                                Tel = a.Tel,
                                ImageURl = a.ImageURl,
                                Isdelete = a.Isdelete,
                                Nic = a.Nic,
                                Title = a.Title,
                                Type = a.Type,
                            }).SingleOrDefaultAsync(d => d.ID.Equals(id));
            return dr;
        }

    }
}
