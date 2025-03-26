using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblSales")]
    public class TblSales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime? Date { get; set; }

        public int Invo { get; set; }  // Fk Key


        [Column(TypeName = "VARCHAR(100)")]
        public string Type { get; set; }

        public int StockId { get; set; } // Fk key

        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public int LotId { get; set; }  // Fk Key

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Unit_Cost { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Unit_Price { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Unit_Profit { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Qty { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }
    }
}
