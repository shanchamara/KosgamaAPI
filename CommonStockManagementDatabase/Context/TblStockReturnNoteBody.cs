using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblStockReturnNoteBody")]
    public class TblStockReturnNoteBody
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("SRNno")]
        public int? SRNno { get; set; }
        [ForeignKey("SRNno")]
        public TblStockReturnNoteHead TblStockRequestNoteHead { get; set; }

        public string UnitSize { get; set; }
        public string UnitName { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Qtypiece { get; set; }
        public int ItemID { get; set; }
        public string Code { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Qty { get; set; }

        [Column(TypeName = "decimal(18, 2)")]

        public decimal? UnitCost { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Price { get; set; }
        public DateTime? ExpDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DisCount { get; set; } = 0;

        public bool IsDelete { get; set; } = false;

        [Column("FKLocationId")]
        public int? FKLocationId { get; set; }  // Fk Key
        [ForeignKey("FKLocationId")]
        public TblCompanyDetails TblCompanyDetails { get; set; }
    }
}
