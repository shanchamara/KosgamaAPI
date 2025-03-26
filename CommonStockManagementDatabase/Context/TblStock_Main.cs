using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblStock_Main")]
    public class TblStock_Main
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemCode { get; set; }

        // [Precision(18, 2)]
        public string UnitSize { get; set; }
        [Precision(18, 2)]
        public decimal? MaxLevel { get; set; }
        [Precision(18, 2)]
        public decimal? MinLevel { get; set; }
        [Precision(18, 2)]
        public decimal? ReorderLevel { get; set; }

        [Column("FkUnitId")]
        public int FkUnitId { get; set; }
        [ForeignKey("FkUnitId")]
        public TblItemUnit TblItemUnit { get; set; }

        [Column("FkCategoryId")]
        public int FkCategoryId { get; set; }
        [ForeignKey("FkCategoryId")]
        public TblItemCategory TblItemCategory { get; set; }
        [Precision(18, 2)]
        public decimal? LastPurchasePrice { get; set; }
        [Precision(18, 2)]
        public decimal? SellingPrice { get; set; }

        [Column("FkBrandId")]
        public int? FkBrandId { get; set; }
        [ForeignKey("FkBrandId")]
        public TblItemBrandName TblItemBrandName { get; set; }

        [Column("FkModelTypeId")]
        public int? FkModelTypeId { get; set; }
        [ForeignKey("FkModelTypeId")]
        public TblItemModelType TblItemModelType { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDelete { get; set; } = false;

        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }
    }
}
