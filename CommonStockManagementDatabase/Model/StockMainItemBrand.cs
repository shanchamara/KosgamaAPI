namespace CommonStockManagementDatabase.Model
{
    public class StockMainItemBrand
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

    public class InsertStockMainItemBrand : StockMainItemBrand { }
    public class UpdateStockMainItemBrand : StockMainItemBrand { }
    public class DeleteStockMainItemBrand : StockMainItemBrand { }
    public class ViewStockMainItemBrand : StockMainItemBrand { }
    public class PaginationViewStockMainItemBrand
    {
        public int Count { get; set; }
        public List<ViewStockMainItemBrand> ViewStockMainItemBrands { get; set; }
        public List<VwListItemBrand> IQueryData { get; set; }
    }

    public class VwListItemBrand : StockMainItemBrand { }
}
