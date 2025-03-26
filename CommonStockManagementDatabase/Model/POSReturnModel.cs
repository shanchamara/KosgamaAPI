using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Model
{
    public class POSReturnModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public string RefInv { get; set; }
        public string Created { get; set; }
        public string Customer { get; set; }
        public int FKClientId { get; set; }
        public double? Total { get; set; }
        public double? Discount { get; set; }
        public double? Gross { get; set; }

        public bool IsDelete { get; set; } = false;

        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }
        public int LocationId { get; set; }
    }

    public class InsertPOSReturnHead : POSReturnModel { }
    public class UpdatePOSReturnHead : POSReturnModel { }
    public class DeletePOSReturnHead : POSReturnModel { }
    public class ViewPOSReturnHead : POSReturnModel
    {
    }
    public class PaginationViewPOSReturnHead
    {
        public int Count { get; set; }
        public List<ViewPOSReturnHead> ViewPOSReturnHeads { get; set; }
        public List<VwListPOSReturnHeads> IQueryData { get; set; }
        public decimal? ToatalAmount { get; set; } = 0.00m;
    }


    public class VwListPOSReturnHeads : POSReturnModel
    {
        [NotMapped]
        public new string Date { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string UserName { get; set; }

        public string LocationName { get; set; }
    }



    public class POSReturnBodyViewlistModel
    {
        public int Id { get; set; }
        public int ItemID { get; set; }
        public int POSBodyNo { get; set; }

        public string Code { get; set; }

        public decimal? Qty { get; set; }
        public decimal? FreeQty { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Sellingprice { get; set; }
        public string UserID { get; set; }
        public string ItemName { get; set; }
        public string RefInv { get; set; }

        public string UnitName { get; set; }

        public string UnitSize { get; set; }

        public decimal? Qtypiece { get; set; }
    }
    public class InsertTempPOSReturnBody : POSReturnBodyViewlistModel { }
    public class UpdateTempPOSReturnBody : POSReturnBodyViewlistModel { }
    public class DeleteTempPOSReturnBody : POSReturnBodyViewlistModel { }
    public class ViewTempPOSReturnBody : POSReturnBodyViewlistModel { }

    public class PaginationViewPOSReturnBody
    {
        public int Count { get; set; }
        public List<ViewTempPOSReturnBody> ViewTempPOSReturnBodies { get; set; }
        public decimal? ToatalAmount { get; set; } = 0.00m;
        public decimal? ToatalDiscount { get; set; } = 0.00m;
        public decimal? ToatalGross { get; set; } = 0.00m;
    }


    public class ViewAllPOsInvoiceItem
    {

        public string INVNo { get; set; }

        public int BodyId { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public string ItemName { get; set; }
        public int ItemID { get; set; }
        public decimal? Qty { get; set; }
        public decimal? FreeQty { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? Cost { get; set; }
        public decimal? DisCount { get; set; }
        public decimal? Price { get; set; }
        public decimal? ReturnQTY { get; set; }

        public decimal? TotalQty { get; set; }

        public string LocationName { get; set; }
        public int LocationId { get; set; }

        public string UnitName { get; set; }
        public string UnitSize { get; set; }


        [NotMapped]
        public decimal? Qtypiece { get; set; }



    }


    public class ViewAllPOsInvoiceItemForReportPage
    {
        public string INVNo { get; set; }

        public int BodyId { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public string ItemName { get; set; }
        public int ItemID { get; set; }
        public decimal? Qty { get; set; }
        public decimal? FreeQty { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? SalesReturn { get; set; }
        public decimal? SalesCost { get; set; }
        public decimal? ReturnDisCount { get; set; }
        public decimal? SalesDisCount { get; set; }
        public decimal? Price { get; set; }
        public decimal? ReturnQTY { get; set; }

        public decimal? TotalQty { get; set; }
        public DateTime Date { get; set; }
        public int LocationName { get; set; }
        public int LocationId { get; set; }
    }


    public class ViewModelGetDailyMonthlyYearlySales
    {
        public decimal? Daily { get; set; } = default;
        public decimal? Yearly { get; set; } = default;
        public decimal? Monthly { get; set; } = default;
    }

    public class ViewAllPOsInvoiceItemForReport
    {
        public string INVNo { get; set; }

        public int BodyId { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public string ItemName { get; set; }
        public int ItemID { get; set; }
        public decimal? Qty { get; set; }
        public decimal? FreeQty { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? Cost { get; set; }
        public decimal? DisCount { get; set; }
        public decimal? Price { get; set; }
        public decimal? ReturnQTY { get; set; }

        public decimal? TotalQty { get; set; }
        public string LocationName { get; set; }
        public int LocationId { get; set; }
    }


    public class ViewAllPurcheseAndRevenue
    {
        public int CurrentYear { get; set; }

        public string PurcheseMONTH { get; set; }

        public decimal? PurcheseCost { get; set; }
        public decimal? SalesCost { get; set; }
        public decimal? SalesReturnCost { get; set; }
        public decimal? NetProfit { get; set; }
        public decimal? PurcheseReturnCost { get; set; }

        public string LocationName { get; set; }
        public int LocationId { get; set; }
    }


    public class PaginationViewPOSInvoiceItemData
    {
        public int Count { get; set; }
        public List<ViewAllPOsInvoiceItem> ViewAllPOsInvoiceItems { get; set; }
    }



}
