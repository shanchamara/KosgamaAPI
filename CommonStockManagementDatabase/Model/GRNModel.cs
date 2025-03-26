using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Model
{
    public class GRNHeadModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Pono { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string GRNType { get; set; }
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
        public string LocationName { get; set; }
    }

    public class InsertGrnHead : GRNHeadModel { }
    public class UpdateGrnHead : GRNHeadModel { }
    public class DeleteGrnHead : GRNHeadModel { }
    public class ViewGrnHead : GRNHeadModel
    {
    }
    public class PaginationViewGrnHead
    {
        public int Count { get; set; }
        public List<ViewGrnHead> ViewGrnHeads { get; set; }
        public List<VwListGRNHeads> IQueryData { get; set; }

        public decimal? TotalAmount { get; set; } = 0.00m;
    }


    public class VwListGRNHeads : GRNHeadModel
    {
        [NotMapped]
        public new string Date { get; set; }
        public DateTime InvoiceDate { get; set; }

    }

    public class GRNBodyViewlistModel
    {
        public int Id { get; set; }
        public int ItemID { get; set; }
        public int GRnNo { get; set; }
        public int GRnBodyNo { get; set; }
        public string Code { get; set; }

        public decimal? Qty { get; set; }
        public decimal? FreeQty { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? Amount { get; set; }
        public string Batch { get; set; } = string.Empty;
        public decimal? Sellingprice { get; set; }
        public string UserID { get; set; }
        public string Item_name { get; set; }

        public string UnitName { get; set; }

        public string UnitSize { get; set; }

        public decimal? Qtypiece { get; set; }
    }
    public class InsertTempGrnBody : GRNBodyViewlistModel { }
    public class UpdateTempGrnBody : GRNBodyViewlistModel { }
    public class DeleteTempGrnBody : GRNBodyViewlistModel { }
    public class ViewTempGrnBody : GRNBodyViewlistModel { }

    public class PaginationViewGRNBody
    {
        public int Count { get; set; }
        public List<ViewTempGrnBody> ViewTempGrnBodies { get; set; }
        public decimal? ToatalAmount { get; set; } = 0.00m;
        public decimal? ToatalDiscount { get; set; } = 0.00m;
        public decimal? ToatalGross { get; set; } = 0.00m;

    }

}
