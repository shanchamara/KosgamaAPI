using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Model
{
    public class POSModel
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
        public int? LocationId { get; set; } = 0;
    }

    public class InsertPOSHead : POSModel { }
    public class UpdatePOSHead : POSModel { }
    public class DeletePOSHead : POSModel { }
    public class ViewPOSHead : POSModel
    {
    }
    public class PaginationViewPOSHead
    {
        public int Count { get; set; }
        public List<ViewPOSHead> ViewPOSHeads { get; set; }
        public List<VwListPOSHeads> IQueryData { get; set; }
        public decimal? ToatalAmount { get; set; } = 0.00m;
        public decimal? ToatalDiscount { get; set; } = 0.00m;
        public decimal? ToatalGross { get; set; } = 0.00m;
    }


    public class VwListPOSHeads : POSModel
    {
        [NotMapped]
        public new string Date { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string UserName { get; set; }

        public string LocationName { get; set; }
    }


    public class VwBestCustomers : POSModel
    {
        [NotMapped]
        public new string Date { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string UserName { get; set; }

        public string LocationName { get; set; }
    }



    public class POSBodyViewlistModel
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
        public string UnitName { get; set; }

        public string UnitSize { get; set; }

        public decimal? Qtypiece { get; set; }
    }
    public class InsertTempPOSBody : POSBodyViewlistModel { }
    public class UpdateTempPOSBody : POSBodyViewlistModel { }
    public class DeleteTempPOSBody : POSBodyViewlistModel { }
    public class ViewTempPOSBody : POSBodyViewlistModel { }

    public class PaginationViewPOSBody
    {
        public int Count { get; set; }
        public List<ViewTempPOSBody> ViewTempPOSBodies { get; set; }
        public decimal? ToatalAmount { get; set; } = 0.00m;
        public decimal? ToatalDiscount { get; set; } = 0.00m;
        public decimal? ToatalGross { get; set; } = 0.00m;
    }

}
