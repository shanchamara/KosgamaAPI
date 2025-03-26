using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Model
{
    public class StockMainModel
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemCode { get; set; }

        //[Precision(18, 2)]
        public string UnitSize { get; set; }
        [Precision(18, 2)]
        public decimal? MaxLevel { get; set; }
        [Precision(18, 2)]
        public decimal? MinLevel { get; set; }
        [Precision(18, 2)]
        public decimal? ReorderLevel { get; set; }

        public int FkUnitId { get; set; }
        public string UnitName { get; set; }

        public int FkCategoryId { get; set; }
        public string CategoryName { get; set; }

        [Precision(18, 2)]
        public decimal? LastPurchasePrice { get; set; } = 0.00m;
        [Precision(18, 2)]
        public decimal? SellingPrice { get; set; }

        public int? FkBrandId { get; set; }
        public string BrandName { get; set; }

       
        public string ImageUrl { get; set; }
        public bool IsDelete { get; set; } = false;
        public bool IsItemCode { get; set; } = false;

        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }
        public IFormFile Image { get; set; }
        public string ImageURl2 { get; set; }

        public decimal? BalanceQty { get; set; }

        public string RefInv { get; set; }

        public decimal? PurchaseQuantity { get; set; }
    }

    public class InsertStockMainModel : StockMainModel { }
    public class UpdateStockMainModel : StockMainModel { }
    public class DeleteStockMainModel : StockMainModel { }
    public class ViewStockMainModel : StockMainModel { }
    public class PaginationViewStockMain
    {
        public int Count { get; set; }
        public List<ViewStockMainModel> ViewStockMainModels { get; set; }
        public List<VWAllActiveItemList> IQueryData { get; set; }
        public List<ViewStockMainModel> IQueryData1 { get; set; }
    }

    public class VWAllActiveItemList : StockMainModel
    {
        [NotMapped]
        public new bool IsItemCode { get; set; }
        [NotMapped]
        public new decimal? PurchaseQuantity { get; set; }
        [NotMapped]
        public new string RefInv { get; set; }
        [NotMapped]
        public new IFormFile Image { get; set; }
        public int Id { get; set; }


        [NotMapped]
        public new string Delete_By { get; set; }

        [NotMapped]
        public new DateTime Delete_Date { get; set; }

        [NotMapped]
        public new string Edit_By { get; set; }
        [NotMapped]
        public new DateTime Edit_Date { get; set; }

        [NotMapped]
        public new string ImageURl2 { get; set; }

        [NotMapped]
        public new string BalanceQty { get; set; }

    }

    public class VWAllActiveANDAvailableItemList : StockMainModel
    {
        [NotMapped]
        public new bool IsItemCode { get; set; }
        [NotMapped]
        public new decimal? PurchaseQuantity { get; set; }
        [NotMapped]
        public new string RefInv { get; set; }
        [NotMapped]
        public new IFormFile Image { get; set; }
        public int Id { get; set; }

        [NotMapped]
        public new string Delete_By { get; set; }

        [NotMapped]
        public new DateTime Delete_Date { get; set; }

        [NotMapped]
        public new string Edit_By { get; set; }
        [NotMapped]
        public new DateTime Edit_Date { get; set; }

        [NotMapped]
        public new string ImageURl2 { get; set; }

        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }


    public class VWAllActiveANDAvailableItemListForSupplierreturn : StockMainModel
    {
        public string GRNRefInv { get; set; }
        public string SRNRefInv { get; set; }
        public decimal? RetrunQty { get; set; }
        public decimal? PurchaseQty { get; set; }

        [NotMapped]
        public new string RefInv { get; set; }
        [NotMapped]
        public new IFormFile Image { get; set; }
        public int Id { get; set; }

        [NotMapped]
        public new string Delete_By { get; set; }

        [NotMapped]
        public new DateTime Delete_Date { get; set; }

        [NotMapped]
        public new string Edit_By { get; set; }
        [NotMapped]
        public new DateTime Edit_Date { get; set; }

        [NotMapped]
        public new string ImageURl2 { get; set; }

        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }


    public class VWAllActiveItemReorderLevelList : StockMainModel
    {
        [NotMapped]
        public new bool IsItemCode { get; set; }
        [NotMapped]
        public new decimal? PurchaseQuantity { get; set; }
        [NotMapped]
        public new string RefInv { get; set; }
        [NotMapped]
        public new IFormFile Image { get; set; }


        [NotMapped]
        public new int ID { get; set; }
        [NotMapped]
        public new string Delete_By { get; set; }

        [NotMapped]
        public new DateTime Delete_Date { get; set; }

        [NotMapped]
        public new string Edit_By { get; set; }
        [NotMapped]
        public new DateTime Edit_Date { get; set; }

        [NotMapped]
        public new string ImageURl2 { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
    }


    public class ChangeItemPriceByCategoryWiseViewModel
    {
        public int FkCategoryId { get; set; }

        public int FkBrandId { get; set; }

        public DateTime? PriceChangeBackupDate { get; set; }
        public string Edit_By { get; set; }
        [Precision(18, 2)]
        public decimal? PercentageLastPurchasePrice { get; set; }
        [Precision(18, 2)]
        public decimal? PercentageSellingPrice { get; set; }


        public DateTime PriceChangeBackupDatestring { get; set; }
    }

    public class VwAllPriceBackupHistory
    {

        public int FkCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }

        [Precision(18, 2)]
        public decimal? LastPurchasePrice { get; set; }
        [Precision(18, 2)]
        public decimal? LastSellingPrice { get; set; }

        [Precision(18, 2)]
        public decimal? NewPurchasePrice { get; set; }
        [Precision(18, 2)]
        public decimal? NewSellingPrice { get; set; }

        public int FkBrandId { get; set; }

        public DateTime PriceChangeBackupDate { get; set; }

        [NotMapped]
        public string PriceChangeBackupDatestring { get; set; }
        [Precision(18, 2)]
        public decimal? PercentageLastPurchasePrice { get; set; }
        [Precision(18, 2)]
        public decimal? PercentageSellingPrice { get; set; }
    }

    public class PaginationViewVwAllPriceBackupHistory
    {
        public int Count { get; set; }
        public List<VwAllPriceBackupHistory> VwAllPriceBackupHistories { get; set; }
    }

}
