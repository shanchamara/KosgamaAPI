using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Model
{
    public class SRNHeadModel
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string SRNType { get; set; }
        public string RefInv { get; set; }
        public string Created { get; set; }
        public string Supplier { get; set; }
        public int FKSupplier_ID { get; set; }
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

    public class InsertSRNHead : SRNHeadModel { }
    public class UpdateSRNHead : SRNHeadModel { }
    public class DeleteSRNHead : SRNHeadModel { }
    public class ViewSRNHead : SRNHeadModel
    {
    }
    public class PaginationViewSRNHead
    {
        public int Count { get; set; }
        public List<ViewSRNHead> ViewSRNHeads { get; set; }
        public List<VwListSRNHeads> IQueryData { get; set; }
        public decimal? ToatalAmount { get; set; } = 0.00m;
    }


    public class VwListSRNHeads : SRNHeadModel
    {
        [NotMapped]
        public new string Date { get; set; }
        public DateTime InvoiceDate { get; set; }

        public string LocationName { get; set; }
    }

    public class SRNBodyViewlistModel
    {
        public int Id { get; set; }
        public int ItemID { get; set; }
        public int SRNNo { get; set; }
        public int SRNBodyNo { get; set; }
        public string Code { get; set; }

        public decimal? Qty { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? Amount { get; set; }
        public string Batch { get; set; } = string.Empty;
        public decimal? Sellingprice { get; set; }
        public string UserID { get; set; }
        public string ItemName { get; set; }
        public string RefInv { get; set; }
        public string UnitName { get; set; }

        public string UnitSize { get; set; }

        public decimal? Qtypiece { get; set; }
    }
    public class InsertTempSRNBody : SRNBodyViewlistModel { }
    public class UpdateTempSRNBody : SRNBodyViewlistModel { }
    public class DeleteTempSRNBody : SRNBodyViewlistModel { }
    public class ViewTempSRNBody : SRNBodyViewlistModel { }

    public class PaginationViewSRNBody
    {
        public int Count { get; set; }
        public List<ViewTempSRNBody> ViewTempSRNBodies { get; set; }
        public decimal? ToatalAmount { get; set; } = 0.00m;
        public decimal? ToatalDiscount { get; set; } = 0.00m;
        public decimal? ToatalGross { get; set; } = 0.00m;
    }

}
