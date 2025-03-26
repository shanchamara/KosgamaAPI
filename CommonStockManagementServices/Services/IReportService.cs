using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Model;
using System.ComponentModel.Design;

namespace CommonStockManagementServices.Services
{
    public class IReportService(AppDbContext context, AuditTrailService auditTrailService, IMemoryCache cache)
    {
        private readonly AppDbContext _context = context;
        private readonly AuditTrailService _auditTrailService = auditTrailService;
        private readonly IMemoryCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));

        public List<VWAllActiveItemReorderLevelList> ListReportReorderLevelItemAsync(int CompanyId)
        {
            var dt = _context.VWAllActiveItemReorderLevelLists.Where(d => d.LocationID == CompanyId).ToList();
            return dt;
        }

        public ViewCompanyDetails GetCompanyDetailsForPrint(int CompanyId)
        {
            try
            {

                // Fetch company details from the database
                var dt = _context.TblCompanyDetails
                .Where(a => a.Id == CompanyId) // Fetch details for company with Id equals 1
                    .Select(a => new ViewCompanyDetails
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
                    })
                    .SingleOrDefault(); // Retrieve single or default company details

                return dt; // Return fetched company details
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public async Task<List<VWAllActiveItemReorderLevelList>> ListReportReorderLevelItemAsyncTop10(int CompanyId)
        {
            var dts = await (from a in _context.VWAllActiveItemReorderLevelLists
                             where a.LocationID == CompanyId
                             select new VWAllActiveItemReorderLevelList()
                             {
                                 BrandName = a.BrandName,
                                 CategoryName = a.CategoryName,
                                 ItemCode = a.ItemCode,
                                 ItemName = a.ItemName,
                                 ReorderLevel = a.ReorderLevel,
                                 //ModelTypeName = a.ModelTypeName,
                             }).AsNoTracking().Take(10).ToListAsync();
            return dts;

        }


        public List<VWAllActiveItemList> ListAvailableItemDetailsItemList()
        {
            var dt = _context.VWAllActiveItemList.ToList();

            return dt;

        }


        public List<VWAllActiveANDAvailableItemList> VWAllActiveANDAvailableItemList(int CompanyId)
        {
            var dt = _context.VWAllActiveANDAvailableItemList.Where(d => d.LocationId == CompanyId).ToList();

            return dt;

        }

        public List<VwListPOSHeads> VWAllActiveInvoiceHeadForReportMonthWise(int Month)
        {
            var dt = _context.VwActiveInvoicesHeads.Where(d => d.InvoiceDate.Month.Equals(Month)).ToList();

            return dt;

        }


        public List<VwListPOSHeads> ListInvoiceHeadSForReportDaily(string Day)
        {
            var dt = _context.VwActiveInvoicesHeads.ToList();
            var dtss = dt.Where(d => d.InvoiceDate.Date.ToString("dd") == Day).ToList();
            return dtss;
        }

        public List<VwListPOSHeads> ListInvoiceHeadSForReportYear(int Year)
        {
            var dt = _context.VwActiveInvoicesHeads.Where(d => d.InvoiceDate.Year.Equals(Year)).ToList();

            return dt;

        }

        public async Task<List<ViewAllPOsInvoiceItemForReport>> ViewAllPOsInvoiceItemForReportAsync(int CompanyId)
        {
            var dt = await _context.ViewAllPOsInvoiceItemForReport.Where(d => d.LocationId == CompanyId).ToListAsync();

            return dt;

        }

        public async Task<List<ViewAllPOsInvoiceItem>> ViewAllPOsInvoiceItems()
        {
            var dt = await _context.ViewAllPOsInvoiceItems.ToListAsync();

            return dt;

        }


        public async Task<List<VwBestCustomers>> ViewAllBestCustomersForReportAsync(int CompanyId)
        {
            var dt = await (from s in _context.VwBestCustomers
                            where s.LocationId == CompanyId
                            select new VwBestCustomers
                            {
                                Created = s.Created,
                                Customer = s.Customer,
                                Date = s.Date,
                                Description = s.Description,
                                Discount = s.Discount,
                                FKClientId = s.FKClientId,
                                Gross = s.Gross,
                                Id = s.Id,
                                LocationId = s.LocationId,
                                LocationName = s.LocationName,
                                RefInv = s.RefInv,
                                Total = s.Total,
                                Type = s.Type,
                                UserName = s.UserName,

                            }).ToListAsync();

            return dt;

        }

        public async Task<List<ViewAllPurcheseAndRevenue>> ViewAllPurcheseAndRevenueForReportAsync(int CompanyId)
        {
            try
            {
                var dt = _context.ViewAllPurcheseAndRevenue.AsQueryable();
                return dt.Where(d => d.LocationId.Equals(CompanyId)).ToList();
            }
            catch (Exception)
            {
                throw;
            }

        }


        public async Task<ViewAllPurcheseAndRevenue> ViewAllPurcheseAndRevenueTotalWiseForReportAsync(int CompanyId)
        {
            var dts = await (from a in _context.ViewAllPurcheseAndRevenue
                             where a.LocationId == CompanyId
                             select new ViewAllPurcheseAndRevenue()
                             {
                                 PurcheseCost = a.PurcheseCost,
                                 NetProfit = a.NetProfit,
                                 SalesCost = a.SalesCost,
                                 SalesReturnCost = a.SalesReturnCost,
                                 PurcheseReturnCost = a.PurcheseReturnCost,
                             }).AsNoTracking().ToListAsync();

            decimal totalPurcheseCost = (decimal)dts.Sum(item => item.PurcheseCost);
            decimal totalNetProfit = (decimal)dts.Sum(item => item.NetProfit);
            decimal totalSalesCost = (decimal)dts.Sum(item => item.SalesCost);
            decimal totalSalesReturnCost = (decimal)dts.Sum(item => item.SalesReturnCost);
            decimal PurcheseReturnCost = (decimal)dts.Sum(item => item.PurcheseReturnCost);

            ViewAllPurcheseAndRevenue dtd = new()
            {
                PurcheseCost = totalPurcheseCost,
                SalesReturnCost = totalSalesReturnCost,
                SalesCost = totalSalesCost,
                NetProfit = totalNetProfit,
                PurcheseReturnCost = PurcheseReturnCost

            };

            return dtd;

        }

        public async Task<ViewModelGetDailyMonthlyYearlySales> ViewAllViewAllPOsInvoiceItemForReportPageForReportAsync(int CompanyId)
        {

            var dts = await (from a in _context.ViewAllPOsInvoiceItemForReportPage
                             where a.LocationId == CompanyId
                             select new ViewAllPOsInvoiceItemForReportPage()
                             {
                                 SalesCost = (a.SalesCost - a.SalesDisCount) - (a.SalesReturn - a.ReturnDisCount),
                                 Date = a.Date,
                             }).AsNoTracking().ToListAsync();


            var date = CommonResources.LocalDatetime().Date.ToString("MM");

            decimal DailySales = (decimal)dts.Where(d => d.Date.Date.ToString("dd") == CommonResources.LocalDatetime().Date.ToString("dd")).Sum(d => d.SalesCost);
            decimal YealySales = (decimal)dts.Where(d => d.Date.Date.ToString("yyyy") == CommonResources.LocalDatetime().Date.ToString("yyyy")).Sum(d => d.SalesCost);
            decimal MonthSales = (decimal)dts.Where(d => d.Date.Date.ToString("MM") == CommonResources.LocalDatetime().Date.ToString("MM")).Sum(d => d.SalesCost);


            ViewModelGetDailyMonthlyYearlySales dtd = new()
            {
                Daily = DailySales,
                Monthly = MonthSales,
                Yearly = YealySales


            };

            return dtd;

        }



    }
}
