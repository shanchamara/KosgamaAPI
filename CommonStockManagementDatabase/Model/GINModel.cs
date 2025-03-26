namespace CommonStockManagementDatabase.Model
{
    public class GINModel
    {
        public int GINId { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string Created { get; set; }
        public string Description { get; set; }
        public double? Total { get; set; }
        public double? Discount { get; set; }
        public double? Gross { get; set; }
        public int? FKLocationId { get; set; }
        public bool IsDelete { get; set; } = false;

        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }
    }

    public class InsertGINModel : GINModel { }
    public class UpdateGINModel : GINModel { }
    public class DeleteGINModel : GINModel { }
    public class ViewGINModel : GINModel { }
    public class PaginationViewGINModel
    {
        public int Count { get; set; }
        public List<ViewGINModel> ViewGINModels { get; set; }
        public decimal? TotalAmount { get; set; } = 0.00m;
    }


    public class GINBodyModel
    {
        public int Id { get; set; }
        public int? GINId { get; set; }
        public int GINNo { get; set; }
        public int GINBodyNo { get; set; }
        public int ItemID { get; set; }

        public string ItemName { get; set; }
        public string Code { get; set; }

        public decimal? Qty { get; set; }

        public decimal? UnitCost { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Price { get; set; }
        public DateTime? ExpDate { get; set; }
        public decimal? DisCount { get; set; } = 0;
        public string UserID { get; set; }
        public bool IsDelete { get; set; } = false;

        public decimal? Amount { get; set; }
    }


    public class InsertGINbodyModel : GINBodyModel { }
    public class UpdateGINBodyModel : GINBodyModel { }
    public class DeleteGINBodyModel : GINBodyModel { }
    public class ViewGINBodyModel : GINBodyModel { }
    public class PaginationViewGINBodyModel
    {
        public int Count { get; set; }
        public List<ViewGINBodyModel> ViewGINBodyModels { get; set; }
        public decimal? ToatalAmount { get; set; } = 0.00m;
        public decimal? ToatalDiscount { get; set; } = 0.00m;
        public decimal? ToatalGross { get; set; } = 0.00m;
    }

    public class VwListGINHead
    {
        public int GINId { get; set; }

        public string Type { get; set; }
        public string Created { get; set; }

        public double? Total { get; set; }
        public double? Discount { get; set; }
        public double? Gross { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; } = false;
        public int? FKLocationId { get; set; }
        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }
        public DateTime Date { get; set; }


    }
}
