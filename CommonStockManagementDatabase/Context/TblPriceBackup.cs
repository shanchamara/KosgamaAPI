using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    public class TblPriceBackup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("FkItemId")]
        public int FkItemId { get; set; }
        [ForeignKey("FkItemId")]
        public TblStock_Main TblItem { get; set; }

        [Column("FkCategoryId")]
        public int FkCategoryId { get; set; }
        [ForeignKey("FkCategoryId")]
        public TblItemCategory TblItemCategory { get; set; }

        [Precision(18, 2)]
        public decimal? LastPurchasePrice { get; set; }
        [Precision(18, 2)]
        public decimal? LastSellingPrice { get; set; }

        [Precision(18, 2)]
        public decimal? NewPurchasePrice { get; set; }
        [Precision(18, 2)]
        public decimal? NewSellingPrice { get; set; }

        [Column("FkBrandId")]
        public int FkBrandId { get; set; }
        [ForeignKey("FkBrandId")]
        public TblItemBrandName TblItemBrandName { get; set; }

        public DateTime PriceChangeBackupDate { get; set; }
        [Precision(18, 2)]
        public decimal? PercentageLastPurchasePrice { get; set; }
        [Precision(18, 2)]
        public decimal? PercentageSellingPrice { get; set; }
    }
}
