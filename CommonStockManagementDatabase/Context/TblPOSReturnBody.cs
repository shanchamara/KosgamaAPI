using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblPOSReturnBody")]
    public class TblPOSReturnBody
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("POSReturnNO")]
        public int? POSReturnNO { get; set; }

        [ForeignKey("POSReturnNO")]
        public TblPOSReturnHead TblPOSHead { get; set; }

        public int POSBodyKeyNo { get; set; }
        public string POSInvoiceNO { get; set; }

        public int ItemID { get; set; }
        public string Code { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Qty { get; set; }
        [Column(TypeName = "decimal(18, 2)")]

        public decimal? FreeQty { get; set; }
        [Column(TypeName = "decimal(18, 2)")]

        public decimal? UnitCost { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Price { get; set; }
        public DateTime? ExpDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DisCount { get; set; } = 0;

        public string UnitSize { get; set; }
        public string UnitName { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Qtypiece { get; set; }

        public bool IsDelete { get; set; } = false;

        [Column("FKLocationId")]
        public int? FKLocationId { get; set; }  // Fk Key
        [ForeignKey("FKLocationId")]
        public TblCompanyDetails TblCompanyDetails { get; set; }
    }
}
