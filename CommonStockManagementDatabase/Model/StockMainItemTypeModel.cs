namespace CommonStockManagementDatabase.Model
{
    public class StockMainItemTypeModel
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

    public class InsertStockMainItemTypeModel : StockMainItemTypeModel { }
    public class UpdateStockMainItemTypeModel : StockMainItemTypeModel { }
    public class DeleteStockMainItemTypeModel : StockMainItemTypeModel { }
    public class ViewStockMainItemTypeModel : StockMainItemTypeModel { }
    public class PaginationViewStockMainItemTypeModel
    {
        public int Count { get; set; }
        public List<ViewStockMainItemTypeModel> ViewStockMainItemTypeModels { get; set; }
        public List<VwListItemModelType> IQueryData { get; set; }
    }


    public class VwListItemModelType : StockMainItemTypeModel { }
}
