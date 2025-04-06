using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Model;
using System.Linq.Expressions;

namespace CommonStockManagementServices.Services
{
    public class StockMainService(AppDbContext context, AuditTrailService auditTrailService, IMemoryCache cache)
    {
        private readonly AppDbContext _context = context;
        private readonly AuditTrailService _auditTrailService = auditTrailService;
        private readonly IMemoryCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));

        private void InvalidateCache(string methodName)
        {
            _cache.Remove(methodName); // Invalidate cache for the specified method
        }

        public async Task<PaginationViewStockMain> GetAllPagination(int page, int pagecount, string searchTerm, string sort = null, string order = null)
        {

            var cacheKey = $"{nameof(PaginationViewStockMain.IQueryData)}";
            // Check if the result is already in the cache

            IQueryable<VWAllActiveItemList> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewStockMain cachedResult))
            {

                query = cachedResult.IQueryData.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<VWAllActiveItemList, object>> defaultSort1 = x => x.Id;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<VWAllActiveItemList>.Search([.. query], searchTerm, c => c.ItemCode.ToString(), c => c.ItemName, c => c.CategoryName, c => c.Id.ToString(),
                c => c.BrandName);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewStockMainModel
                       {

                           Delete_By = a.Delete_By,
                           Delete_Date = a.Delete_Date,
                           ID = a.ID,
                           ItemCode = a.ItemCode,
                           LastPurchasePrice = a.LastPurchasePrice,
                           ItemDescription = a.ItemDescription,
                           IsDelete = a.IsDelete,
                           MaxLevel = a.MaxLevel,
                           MinLevel = a.MinLevel,
                           ItemName = a.ItemName,
                           SellingPrice = a.SellingPrice,
                           ReorderLevel = a.ReorderLevel,
                           UnitName = a.UnitName,
                           BrandName = a.BrandName,
                           CategoryName = a.CategoryName,
                           FkBrandId = a.FkBrandId,
                           FkCategoryId = a.FkCategoryId,
                           UnitSize = a.UnitSize,
                           FkUnitId = a.FkUnitId,
                           ImageUrl = a.ImageUrl,
                       })
                       .ToList();

                // Prepare the pagination response
                var paginationListData1 = new PaginationViewStockMain
                {
                    Count = filteredData1.Count,
                    ViewStockMainModels = pagedData1.Skip((page - 1) * pagecount).Take(pagecount).ToList(),

                };

                return paginationListData1;
            }
            else
            {
                query = _context.VWAllActiveItemList.AsQueryable();
            }

            // Apply sorting based on sort and order parameters

            Expression<Func<VWAllActiveItemList, object>> defaultSort = x => x.Id;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => !d.IsDelete).ToListAsync();

            filteredData = QueryHelper.SearchUtility<VWAllActiveItemList>.Search(filteredData, searchTerm, c => c.ItemCode.ToString(), c => c.ItemName, c => c.CategoryName, c => c.Id.ToString(),
                c => c.BrandName);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewStockMainModel
                {
                    Delete_By = a.Delete_By,
                    Delete_Date = a.Delete_Date,
                    ID = a.ID,
                    ItemCode = a.ItemCode,
                    LastPurchasePrice = a.LastPurchasePrice,
                    ItemDescription = a.ItemDescription,
                    IsDelete = a.IsDelete,
                    MaxLevel = a.MaxLevel,
                    MinLevel = a.MinLevel,
                    ItemName = a.ItemName,
                    SellingPrice = a.SellingPrice,
                    ReorderLevel = a.ReorderLevel,
                    UnitName = a.UnitName,
                    BrandName = a.BrandName,
                    CategoryName = a.CategoryName,
                    FkBrandId = a.FkBrandId,
                    FkCategoryId = a.FkCategoryId,
                    UnitSize = a.UnitSize,
                    FkUnitId = a.FkUnitId,
                    ImageUrl = a.ImageUrl,
                })
                .ToList();

            // Prepare the pagination response
            var paginationListData = new PaginationViewStockMain
            {
                Count = filteredData.Count,
                ViewStockMainModels = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList()
            };

            // Cache the result

            var CacheResult = new PaginationViewStockMain
            {
                Count = filteredData.Count,
                IQueryData = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;

        }


        // Get All Price for change 
        public async Task<PaginationViewStockMain> GetAllPaginationGroupByCategory_Brand_Type(int page, int pagecount, string searchTerm, string sort = null, string order = null, int catId = 0, int brandId = 0, int typeId = 0)
        {
            var cacheKey = $"{nameof(PaginationViewStockMain.IQueryData1)}";
            // Check if the result is already in the cache

            IQueryable<ViewStockMainModel> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewStockMain cachedResult))
            {

                query = cachedResult.IQueryData1.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<ViewStockMainModel, object>> defaultSort1 = x => x.ID;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = query.Where(a => !a.IsDelete && (brandId == 0 || a.FkBrandId == brandId) &&
                              (catId == 0 || a.FkCategoryId == catId)).ToList();

                filteredData1 = QueryHelper.SearchUtility<ViewStockMainModel>.Search([.. filteredData1], null, null);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewStockMainModel
                       {

                           ID = a.ID,
                           ItemCode = a.ItemCode,
                           LastPurchasePrice = a.LastPurchasePrice,
                           ItemDescription = a.ItemDescription,
                           IsDelete = a.IsDelete,
                           MaxLevel = a.MaxLevel,
                           MinLevel = a.MinLevel,
                           ItemName = a.ItemName,
                           SellingPrice = a.SellingPrice,
                           ReorderLevel = a.ReorderLevel,
                           UnitName = a.UnitName,
                           BrandName = a.BrandName,
                           CategoryName = a.CategoryName,
                           FkBrandId = a.FkBrandId,
                           FkCategoryId = a.FkCategoryId,
                           UnitSize = a.UnitSize,
                           FkUnitId = a.FkUnitId,
                           ImageUrl = a.ImageUrl,
                       })
                       .ToList();

                // Prepare the pagination response
                var paginationListData1 = new PaginationViewStockMain
                {
                    Count = filteredData1.Count,
                    ViewStockMainModels = pagedData1.Skip((page - 1) * pagecount).Take(pagecount).ToList(),

                };

                return paginationListData1;
            }
            else
            {
                query = (from a in _context.VWAllActiveItemList
                         select new ViewStockMainModel()
                         {
                             ID = a.ID,
                             ItemCode = a.ItemCode,
                             LastPurchasePrice = a.LastPurchasePrice,
                             ItemDescription = a.ItemDescription,
                             IsDelete = a.IsDelete,
                             MaxLevel = a.MaxLevel,
                             MinLevel = a.MinLevel,
                             ItemName = a.ItemName,
                             SellingPrice = a.SellingPrice,
                             ReorderLevel = a.ReorderLevel,
                             UnitName = a.UnitName,
                             BrandName = a.BrandName,
                             CategoryName = a.CategoryName,
                             FkBrandId = a.FkBrandId,
                             FkCategoryId = a.FkCategoryId,
                             UnitSize = a.UnitSize,
                             FkUnitId = a.FkUnitId,
                             ImageUrl = a.ImageUrl,
                         }).AsQueryable();
            }

            // Apply sorting based on sort and order parameters

            Expression<Func<ViewStockMainModel, object>> defaultSort = x => x.ID;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(a => !a.IsDelete && (brandId == 0 || a.FkBrandId == brandId) &&
                               (catId == 0 || a.FkCategoryId == catId)).ToListAsync();

            filteredData = QueryHelper.SearchUtility<ViewStockMainModel>.Search(filteredData, null, null);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewStockMainModel
                {
                    ID = a.ID,
                    ItemCode = a.ItemCode,
                    LastPurchasePrice = a.LastPurchasePrice,
                    ItemDescription = a.ItemDescription,
                    IsDelete = a.IsDelete,
                    MaxLevel = a.MaxLevel,
                    MinLevel = a.MinLevel,
                    ItemName = a.ItemName,
                    SellingPrice = a.SellingPrice,
                    ReorderLevel = a.ReorderLevel,
                    UnitName = a.UnitName,
                    BrandName = a.BrandName,
                    CategoryName = a.CategoryName,
                    FkBrandId = a.FkBrandId,
                    FkCategoryId = a.FkCategoryId,
                    UnitSize = a.UnitSize,
                    FkUnitId = a.FkUnitId,
                    ImageUrl = a.ImageUrl,
                })
                .ToList();

            // Prepare the pagination response
            var paginationListData = new PaginationViewStockMain
            {
                Count = filteredData.Count,
                ViewStockMainModels = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList()
            };

            // Cache the result

            var CacheResult = new PaginationViewStockMain
            {
                Count = filteredData.Count,
                IQueryData1 = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;

        }


        public async Task<PaginationViewStockMain> GetAllPaginationWithOutPagination(int page, int pagecount, string searchTerm, string sort = null, string order = null, int locationId = 0)
        {
            var cacheKey = $"{nameof(PaginationViewStockMain.IQueryData1)}";
            // Check if the result is already in the cache

            IQueryable<ViewStockMainModel> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewStockMain cachedResult))
            {

                query = cachedResult.IQueryData1.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<ViewStockMainModel, object>> defaultSort1 = x => x.ID;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<ViewStockMainModel>.Search([.. query], searchTerm, c => c.ItemCode.ToString(), c => c.ItemName, c => c.CategoryName, c => c.ID.ToString(),
                c => c.BrandName);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewStockMainModel
                       {
                           LocationId = a.LocationId,
                           ID = a.ID,
                           ItemCode = a.ItemCode,
                           LastPurchasePrice = a.LastPurchasePrice,
                           ItemDescription = a.ItemDescription,
                           IsDelete = a.IsDelete,
                           MaxLevel = a.MaxLevel,
                           MinLevel = a.MinLevel,
                           ItemName = a.ItemName,
                           SellingPrice = a.SellingPrice,
                           ReorderLevel = a.ReorderLevel,
                           UnitName = a.UnitName,
                           BrandName = a.BrandName,
                           CategoryName = a.CategoryName,
                           FkBrandId = a.FkBrandId,
                           FkCategoryId = a.FkCategoryId,
                           UnitSize = a.UnitSize,
                           FkUnitId = a.FkUnitId,
                           ImageUrl = a.ImageUrl,
                           BalanceQty = a.BalanceQty,
                       }).
                       Where(d => d.LocationId.Equals(locationId))
                       .ToList();

                // Prepare the pagination response
                var paginationListData1 = new PaginationViewStockMain
                {
                    Count = filteredData1.Count,
                    ViewStockMainModels = pagedData1,

                };

                return paginationListData1;
            }
            else
            {
                query = (from a in _context.VWAllActiveANDAvailableItemList //VWAllActiveANDAvailableItemList
                         orderby a.Id descending
                         select new ViewStockMainModel()
                         {
                             ID = a.ID,
                             ItemCode = a.ItemCode,
                             LastPurchasePrice = a.LastPurchasePrice,
                             ItemDescription = a.ItemDescription,
                             IsDelete = a.IsDelete,
                             MaxLevel = a.MaxLevel,
                             MinLevel = a.MinLevel,
                             ItemName = a.ItemName,
                             SellingPrice = a.SellingPrice,
                             ReorderLevel = a.ReorderLevel,
                             UnitName = a.UnitName,
                             BrandName = a.BrandName,
                             CategoryName = a.CategoryName,
                             FkBrandId = a.FkBrandId,
                             FkCategoryId = a.FkCategoryId,
                             UnitSize = a.UnitSize,
                             FkUnitId = a.FkUnitId,
                             ImageUrl = a.ImageUrl,
                             BalanceQty = a.BalanceQty,
                             LocationId = a.LocationId
                         }).ToList().AsQueryable();
            }

            // Apply sorting based on sort and order parameters

            Expression<Func<ViewStockMainModel, object>> defaultSort = x => x.ID;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = query.Where(d => !d.IsDelete).ToList();

            filteredData = QueryHelper.SearchUtility<ViewStockMainModel>.Search(filteredData, null, null);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewStockMainModel
                {
                    ID = a.ID,
                    ItemCode = a.ItemCode,
                    LastPurchasePrice = a.LastPurchasePrice,
                    ItemDescription = a.ItemDescription,
                    IsDelete = a.IsDelete,
                    MaxLevel = a.MaxLevel,
                    MinLevel = a.MinLevel,
                    ItemName = a.ItemName,
                    SellingPrice = a.SellingPrice,
                    ReorderLevel = a.ReorderLevel,
                    UnitName = a.UnitName,
                    BrandName = a.BrandName,
                    CategoryName = a.CategoryName,
                    FkBrandId = a.FkBrandId,
                    FkCategoryId = a.FkCategoryId,
                    UnitSize = a.UnitSize,
                    FkUnitId = a.FkUnitId,
                    ImageUrl = a.ImageUrl,
                    BalanceQty = a.BalanceQty,
                })
                .Where(d => d.LocationId.Equals(locationId))
                .ToList();

            // Prepare the pagination response

            var paginationListData = new PaginationViewStockMain
            {
                Count = filteredData.Count,
                ViewStockMainModels = pagedData
            };

            // Cache the result

            var CacheResult = new PaginationViewStockMain
            {
                Count = filteredData.Count,
                IQueryData1 = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;


        }


        public async Task<PaginationViewStockMain> GetAllPaginationWithOutPaginationForSupplierReturn(int page, int pagecount, string searchTerm, string sort = null, string order = null, string invoiceNo = null, int LocationId = 0)
        {
            var cacheKey = $"{nameof(PaginationViewStockMain.IQueryData1)}";
            // Check if the result is already in the cache

            IQueryable<ViewStockMainModel> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewStockMain cachedResult))
            {

                query = cachedResult.IQueryData1.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<ViewStockMainModel, object>> defaultSort1 = x => x.ID;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<ViewStockMainModel>.Search([.. query], searchTerm, c => c.ItemCode.ToString(), c => c.ItemDescription, c => c.ItemName);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new ViewStockMainModel
                       {
                           ID = a.ID,
                           ItemCode = a.ItemCode,
                           LastPurchasePrice = a.LastPurchasePrice,
                           ItemDescription = a.ItemDescription,
                           IsDelete = a.IsDelete,
                           MaxLevel = a.MaxLevel,
                           MinLevel = a.MinLevel,
                           ItemName = a.ItemName,
                           SellingPrice = a.SellingPrice,
                           ReorderLevel = a.ReorderLevel,
                           UnitName = a.UnitName,
                           BrandName = a.BrandName,
                           CategoryName = a.CategoryName,
                           FkBrandId = a.FkBrandId,
                           FkCategoryId = a.FkCategoryId,
                           UnitSize = a.UnitSize,
                           FkUnitId = a.FkUnitId,
                           ImageUrl = a.ImageUrl,
                           BalanceQty = a.BalanceQty,
                           RefInv = a.RefInv,
                           PurchaseQuantity = a.PurchaseQuantity,
                       })
                       .ToList();

                // Prepare the pagination response
                var paginationListData1 = new PaginationViewStockMain
                {
                    Count = filteredData1.Count,
                    ViewStockMainModels = pagedData1,

                };

                return paginationListData1;
            }
            else
            {

                //var dt = _context.VWAllActiveANDAvailableItemList.Where(d => d.LocationId == 1).ToList();

                query = (from a in _context.TblGRNBodies
                         join b in _context.TblGRNHeads on a.Grnno equals b.Id
                         join itemhave in _context.VWAllActiveANDAvailableItemList on a.ItemID equals itemhave.ID
                         where b.FKLocationId == LocationId && itemhave.LocationId == LocationId
                         select new ViewStockMainModel()
                         {
                             ID = itemhave.ID,
                             ItemCode = itemhave.ItemCode,
                             LastPurchasePrice = itemhave.LastPurchasePrice,
                             ItemDescription = itemhave.ItemDescription,
                             IsDelete = itemhave.IsDelete,
                             MaxLevel = itemhave.MaxLevel,
                             MinLevel = itemhave.MinLevel,
                             ItemName = itemhave.ItemName,
                             SellingPrice = itemhave.SellingPrice,
                             ReorderLevel = itemhave.ReorderLevel,
                             UnitName = itemhave.UnitName,
                             BrandName = itemhave.BrandName,
                             CategoryName = itemhave.CategoryName,
                             FkBrandId = itemhave.FkBrandId,
                             FkCategoryId = itemhave.FkCategoryId,
                             UnitSize = itemhave.UnitSize,
                             FkUnitId = itemhave.FkUnitId,
                             ImageUrl = itemhave.ImageUrl,
                             BalanceQty = itemhave.BalanceQty,
                             RefInv = b.RefInv,
                             PurchaseQuantity = a.Qty,

                         }).AsQueryable();
            }

            // Apply sorting based on sort and order parameters

            Expression<Func<ViewStockMainModel, object>> defaultSort = x => x.ID;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => !d.IsDelete && d.RefInv.Equals(invoiceNo)).ToListAsync();

            filteredData = QueryHelper.SearchUtility<ViewStockMainModel>.Search(filteredData, null, null);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new ViewStockMainModel
                {
                    ID = a.ID,
                    ItemCode = a.ItemCode,
                    LastPurchasePrice = a.LastPurchasePrice,
                    ItemDescription = a.ItemDescription,
                    IsDelete = a.IsDelete,
                    MaxLevel = a.MaxLevel,
                    MinLevel = a.MinLevel,
                    ItemName = a.ItemName,
                    SellingPrice = a.SellingPrice,
                    ReorderLevel = a.ReorderLevel,
                    UnitName = a.UnitName,
                    BrandName = a.BrandName,
                    CategoryName = a.CategoryName,
                    FkBrandId = a.FkBrandId,
                    FkCategoryId = a.FkCategoryId,
                    UnitSize = a.UnitSize,
                    FkUnitId = a.FkUnitId,
                    ImageUrl = a.ImageUrl,
                    BalanceQty = a.BalanceQty,
                    RefInv = a.RefInv,
                    PurchaseQuantity = a.PurchaseQuantity,
                })
                .ToList();

            // Prepare the pagination response

            var paginationListData = new PaginationViewStockMain
            {
                Count = filteredData.Count,
                ViewStockMainModels = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList()
            };

            // Cache the result

            var CacheResult = new PaginationViewStockMain
            {
                Count = filteredData.Count,
                IQueryData1 = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;

        }



        public async Task<Message<string>> RegisterAsync(InsertStockMainModel model)
        {

            try
            {
                var transactionscope = _context.Database.BeginTransaction();


                var ChecktheisthereExsitingStockMainName = GetByName(model.ItemName, model.ItemCode, model.UnitSize);

                if (model.Image != null && model.Image.Length > 0)
                {
                    if (model.ImageUrl != "Select.png" && model.ImageUrl != null && model.ImageUrl != model.ImageURl2)
                    {
                        File.Delete(CommonResources.System_File_Path + "/StockMain/" + model.ImageUrl);
                    }

                    var type = model.Image.FileName.Split(".").Last();
                    model.ImageUrl = string.Format("{0}.{1}", Guid.NewGuid().ToString(), type);
                    using (var fileStream = new FileStream(string.Format("{0}/{1}", CommonResources.System_File_Path + "/StockMain/", model.ImageUrl), FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    }
                    ;
                }
                else model.ImageUrl ??= "Select.png";


                if (ChecktheisthereExsitingStockMainName == null)
                {
                    var main = new TblStock_Main
                    {
                        ItemName = model.ItemName,
                        ItemCode = model.ItemCode,
                        ImageUrl = model.ImageUrl,
                        FkUnitId = model.FkUnitId,
                        UnitSize = model.UnitSize,
                        //FkModelTypeId = model.FkModelTypeId,
                        FkBrandId = model.FkBrandId,
                        FkCategoryId = model.FkCategoryId,
                        ItemDescription = model.ItemDescription,
                        LastPurchasePrice = model.LastPurchasePrice,
                        MaxLevel = model.MaxLevel,
                        SellingPrice = model.SellingPrice,
                        MinLevel = model.MinLevel,
                        ReorderLevel = model.ReorderLevel,
                        Edit_By = model.Edit_By,
                        Edit_Date = model.Edit_Date,
                    };
                    _context.TblStock_Mains.Add(main);
                    await _context.SaveChangesAsync();

                    main.ItemCode = model.IsItemCode == true ? Convert.ToString(main.ID) : model.ItemCode; // Assume you have a method to generate the ItemCode based on the ID

                    _context.TblStock_Mains.Update(main);
                    await _context.SaveChangesAsync();
                    // Log audit trail
                    _auditTrailService.LogAudit("TblStock_Mains", "Insert", $"Insert {model.ItemName + ' '} Name.", model.Edit_By);

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewStockMain.IQueryData));
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This Stock Main has been registered",
                    };
                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"This Stock Main has been already registered"
                    };
                }

            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }

        public TblStock_Main GetById(int id)
        {
            return _context.TblStock_Mains.SingleOrDefault(d => d.ID.Equals(id));
        }

        public TblStock_Main GetByName(string Name, string unitsize, string Code)
        {
            return _context.TblStock_Mains.SingleOrDefault(d => d.ItemName.Equals(Name) && d.ItemCode.Equals(Code) && d.UnitSize.Equals(unitsize));
        }

        public async Task<Message<string>> UpdateAsync(UpdateStockMainModel model)
        {
            try
            {
                var transactionscope = _context.Database.BeginTransaction();

                if (model.Image != null && model.Image.Length > 0)
                {
                    if (model.ImageUrl != "Select.png" && model.ImageUrl != null && model.ImageUrl != model.ImageURl2)
                    {
                        File.Delete(CommonResources.System_File_Path + "/StockMain/" + model.ImageUrl);
                    }

                    var type = model.Image.FileName.Split(".").Last();
                    model.ImageUrl = string.Format("{0}.{1}", Guid.NewGuid().ToString(), type);
                    using (var fileStream = new FileStream(string.Format("{0}/{1}", CommonResources.System_File_Path + "/StockMain/", model.ImageUrl), FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    }
                    ;
                }
                else model.ImageUrl ??= "Select.png";

                var exist = GetById(model.ID);
                exist.Edit_By = model.Edit_By;
                exist.Edit_Date = model.Edit_Date;
                exist.LastPurchasePrice = model.LastPurchasePrice;
                exist.SellingPrice = model.SellingPrice;
                exist.FkBrandId = model.FkBrandId;
                exist.FkCategoryId = model.FkCategoryId;
                exist.UnitSize = model.UnitSize;
                exist.FkUnitId = model.FkUnitId;
                //exist.FkModelTypeId = model.FkModelTypeId;
                exist.ItemName = model.ItemName;
                exist.ItemDescription = model.ItemDescription;
                exist.ImageUrl = model.ImageUrl;
                exist.ItemCode = model.IsItemCode == true ? Convert.ToString(model.ID) : model.ItemCode;
                exist.MinLevel = model.MinLevel;
                exist.MaxLevel = model.MaxLevel;
                exist.ReorderLevel = model.ReorderLevel;

                await _context.SaveChangesAsync();


                // Log audit trail
                _auditTrailService.LogAudit("TblStock_Mains", "Update", $"Update {model.ItemName + ' '} name.", model.Edit_By);


                transactionscope.Commit();

                InvalidateCache(nameof(PaginationViewStockMain.IQueryData));
                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Stock Main has been updated successfully",
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Update error {ex.Message}" };
            }


        }

        public async Task<Message<string>> DeleteAsync(DeleteStockMainModel model)
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
                    _auditTrailService.LogAudit("TblStock_Mains", "Delete", $"Delete this {model.ItemName + ' ' + model.ID} name.", model.Edit_By);

                    await _context.SaveChangesAsync();

                    transactionscope.Commit();
                    InvalidateCache(nameof(PaginationViewStockMain.IQueryData));
                }
                else
                {
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"Stock Main has been already deleted",
                    };
                }

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Stock Main details have been deleted successfully"
                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Delete error {ex.Message}" };
            }
        }
        public async Task<ViewStockMainModel> GetDetailsById(int id)
        {
            return await (from a in _context.TblStock_Mains.Include(d => d.TblItemBrandName).Include(c => c.TblItemCategory).Include(u => u.TblItemUnit).Include(m => m.TblItemModelType)
                          select new ViewStockMainModel()
                          {
                              ID = a.ID,
                              ItemCode = a.ItemCode,
                              LastPurchasePrice = a.LastPurchasePrice,
                              ItemDescription = a.ItemDescription,
                              IsDelete = a.IsDelete,
                              MaxLevel = a.MaxLevel,
                              MinLevel = a.MinLevel,
                              ItemName = a.ItemName,
                              SellingPrice = a.SellingPrice,
                              ReorderLevel = a.ReorderLevel,
                              UnitName = a.TblItemUnit.Name,
                              BrandName = a.TblItemBrandName.Name,
                              CategoryName = a.TblItemCategory.Name,
                              //ModelTypeName = a.TblItemModelType.Name,
                              FkBrandId = a.FkBrandId,
                              FkCategoryId = a.FkCategoryId,
                              //FkModelTypeId = a.FkModelTypeId,
                              UnitSize = a.UnitSize,
                              FkUnitId = a.FkUnitId,
                              ImageUrl = a.ImageUrl,
                          }).SingleOrDefaultAsync(d => d.ID.Equals(id));

        }


        public async Task<Message<string>> PercentagePriceChangeItemWise(ChangeItemPriceByCategoryWiseViewModel model)
        {
            try
            {

                using var transaction = _context.Database.BeginTransaction();

                var ListOfItems = await _context.TblStock_Mains.Where(d => d.IsDelete.Equals(false) && d.FkCategoryId.Equals(model.FkCategoryId) && d.FkBrandId.Equals(model.FkBrandId)).ToListAsync();

                foreach (var d in ListOfItems)
                {
                    decimal? NewPurchasePrice = (d.LastPurchasePrice * model.PercentageLastPurchasePrice) / 100 + d.LastPurchasePrice;
                    decimal? NewSellingPrice = (d.SellingPrice * model.PercentageSellingPrice) / 100 + d.SellingPrice;

                    var tblPriceBackup = new TblPriceBackup
                    {
                        FkBrandId = model.FkBrandId,
                        FkCategoryId = model.FkCategoryId,
                        FkItemId = d.ID,
                        LastPurchasePrice = d.LastPurchasePrice,
                        LastSellingPrice = d.SellingPrice,
                        PercentageLastPurchasePrice = model.PercentageLastPurchasePrice,
                        PercentageSellingPrice = model.PercentageSellingPrice,
                        PriceChangeBackupDate = (DateTime)model.PriceChangeBackupDate,
                        NewPurchasePrice = NewPurchasePrice,
                        NewSellingPrice = NewSellingPrice
                    };
                    _context.TblPriceBackups.Add(tblPriceBackup);

                    var LoadSingleItem = _context.TblStock_Mains.Where(a => a.ID == d.ID).FirstOrDefault();
                    LoadSingleItem.LastPurchasePrice = NewPurchasePrice;
                    LoadSingleItem.SellingPrice = NewSellingPrice;



                }
                _context.SaveChanges();
                transaction.Commit();
                InvalidateCache(nameof(PaginationViewVwAllPriceBackupHistory));
                return new Message<string>()
                {
                    Status = "S",
                    Text = $"The prices of all items have been changed",
                };


            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }


        }


        public async Task<PaginationViewVwAllPriceBackupHistory> GetAllPriceBackUphistoryList(int page, int pagecount, string searchTerm, string sort = null, string order = null, string invoiceNo = null)
        {

            var cacheKey = $"{nameof(PaginationViewVwAllPriceBackupHistory)}";
            // Check if the result is already in the cache

            IQueryable<VwAllPriceBackupHistory> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationViewVwAllPriceBackupHistory cachedResult))
            {

                query = cachedResult.VwAllPriceBackupHistories.ToList().AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<VwAllPriceBackupHistory, object>> defaultSort1 = x => x.PriceChangeBackupDate;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<VwAllPriceBackupHistory>.Search([.. query], searchTerm, null);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new VwAllPriceBackupHistory
                       {
                           PriceChangeBackupDatestring = a.PriceChangeBackupDatestring,
                           BrandName = a.BrandName,
                           CategoryName = a.CategoryName,
                           FkBrandId = a.FkBrandId,
                           FkCategoryId = a.FkCategoryId,
                           LastPurchasePrice = a.LastPurchasePrice,
                           LastSellingPrice = a.LastSellingPrice,
                           NewPurchasePrice = a.NewPurchasePrice,
                           NewSellingPrice = a.NewSellingPrice,
                           PercentageLastPurchasePrice = a.PercentageLastPurchasePrice,
                           PercentageSellingPrice = a.PercentageSellingPrice,
                           PriceChangeBackupDate = a.PriceChangeBackupDate
                       })
                       .ToList();

                // Prepare the pagination response

                var paginationListData1 = new PaginationViewVwAllPriceBackupHistory
                {
                    Count = filteredData1.Count,
                    VwAllPriceBackupHistories = pagedData1.Skip((page - 1) * pagecount).Take(pagecount).ToList(),

                };

                return paginationListData1;
            }
            else
            {
                query = (from a in _context.VwAllPriceBackupHistorys
                             //.AsEnumerable()
                         select new VwAllPriceBackupHistory()
                         {
                             PriceChangeBackupDate = a.PriceChangeBackupDate,
                             PriceChangeBackupDatestring = a.PriceChangeBackupDate.ToString("yyyy-MM-dd"),
                             BrandName = a.BrandName,
                             CategoryName = a.CategoryName,
                             FkBrandId = a.FkBrandId,
                             FkCategoryId = a.FkCategoryId,
                             LastPurchasePrice = a.LastPurchasePrice,
                             LastSellingPrice = a.LastSellingPrice,
                             NewPurchasePrice = a.NewPurchasePrice,
                             NewSellingPrice = a.NewSellingPrice,
                             PercentageLastPurchasePrice = a.PercentageLastPurchasePrice,
                             PercentageSellingPrice = a.PercentageSellingPrice,
                         }).ToList().AsQueryable();


            }

            // Apply sorting based on sort and order parameters

            Expression<Func<VwAllPriceBackupHistory, object>> defaultSort = x => x.PriceChangeBackupDate;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = query.ToList();

            filteredData = QueryHelper.SearchUtility<VwAllPriceBackupHistory>.Search(filteredData, null, null);

            // Project the filtered data to 
            var pagedData = filteredData
                .Select(a => new VwAllPriceBackupHistory
                {
                    PriceChangeBackupDatestring = a.PriceChangeBackupDate.ToString("yyyy-MM-dd"),
                    BrandName = a.BrandName,
                    CategoryName = a.CategoryName,
                    FkBrandId = a.FkBrandId,
                    FkCategoryId = a.FkCategoryId,
                    LastPurchasePrice = a.LastPurchasePrice,
                    LastSellingPrice = a.LastSellingPrice,
                    NewPurchasePrice = a.NewPurchasePrice,
                    NewSellingPrice = a.NewSellingPrice,
                    PercentageLastPurchasePrice = a.PercentageLastPurchasePrice,
                    PercentageSellingPrice = a.PercentageSellingPrice,
                })
                .ToList();

            // Prepare the pagination response

            var paginationListData = new PaginationViewVwAllPriceBackupHistory
            {
                Count = filteredData.Count,
                VwAllPriceBackupHistories = pagedData.Skip((page - 1) * pagecount).Take(pagecount).ToList()
            };


            _cache.Set(cacheKey, paginationListData, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;

        }


        public async Task<Message<string>> RecoveryPriceChangeItemWise(ChangeItemPriceByCategoryWiseViewModel model)
        {
            try
            {

                using var transaction = _context.Database.BeginTransaction();



                var RecoveryDetails = await _context.TblPriceBackups.Where(d => d.PriceChangeBackupDate.Date.Equals(Convert.ToDateTime(model.PriceChangeBackupDate.Value)) && d.FkCategoryId.Equals(model.FkCategoryId) && d.FkBrandId.Equals(model.FkBrandId)).ToListAsync();



                foreach (var d in RecoveryDetails)
                {

                    var ListOfItems = await _context.TblStock_Mains.FirstOrDefaultAsync(x => x.IsDelete.Equals(false) && x.FkCategoryId.Equals(model.FkCategoryId) && x.FkBrandId.Equals(model.FkBrandId) && x.ID.Equals(d.FkItemId));

                    ListOfItems.LastPurchasePrice = d.LastPurchasePrice;
                    ListOfItems.SellingPrice = d.LastSellingPrice;

                }


                _context.TblPriceBackups.RemoveRange(RecoveryDetails);

                _context.SaveChanges();
                transaction.Commit();

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"The prices of all items have been changed",
                };


            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }


        }

    }
}
