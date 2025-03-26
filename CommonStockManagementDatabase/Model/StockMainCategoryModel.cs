namespace CommonStockManagementDatabase.Model
{
    public class StockMainCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }
        public bool IsDelete { get; set; } = false;
    }

    public class InsertStockMainCategory : StockMainCategoryModel { }
    public class UpdateStockMainCategory : StockMainCategoryModel { }
    public class DeleteStockMainCategory : StockMainCategoryModel { }
    public class ViewStockMainCategory : StockMainCategoryModel { }
    public class PaginationViewStockMainCategory
    {
        public int Count { get; set; }
        public List<ViewStockMainCategory> ViewStockMainCategories { get; set; }
        public List<VwListItemCategory> AsQuerybleData { get; set; }
    }

    public class VwListItemCategory : StockMainCategoryModel
    {
    }


}
