using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblFixedAssets")]
    public class TblFixedAssets
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Model { get; set; }

        public string Code { get; set; }
        public string Make { get; set; }
        public string PerCode { get; set; }
        public int Serial { get; set; }
        public int? GRNNo { get; set; }
        public string Description { get; set; }
        [Column("FKSupplier_ID")]
        public int? FKSupplier_ID { get; set; }
        [ForeignKey("FKSupplier_ID")]
        public TblSupplier FKTblSupplier { get; set; }

        [Column("FKLocationId")]
        public int? FKLocationId { get; set; }  // Fk Key
        [ForeignKey("FKLocationId")]
        public TblCompanyDetails TblCompanyDetails { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public double? PurchasePrice { get; set; }
        public string Name { get; set; }
        public int? Qty { get; set; }
        public double? Cost { get; set; }
        public DateTime? Warrent_ex { get; set; }
        public string Naration { get; set; }
        public string Status { get; set; }

    }
}
