namespace CommonStockManagementDatabase.Model
{
    public class StockMainItemUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; } = false;
        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }
    }

    public class InsertStcokMainItemUnit : StockMainItemUnit { }
    public class UpdateStcokMainItemUnit : StockMainItemUnit { }
    public class DeleteStcokMainItemUnit : StockMainItemUnit { }
    public class ViewStcokMainItemUnit : StockMainItemUnit { }
    public class PaginationViewStcokMainItemUnit
    {
        public int Count { get; set; }
        public List<ViewStcokMainItemUnit> ViewStcokMainItemUnits { get; set; }
        public List<VwListItemUnit> IQueryData { get; set; }
    }

    public class VwListItemUnit : StockMainItemUnit { }
}
