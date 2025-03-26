using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblGINBodyTemp")]
    public class TblGINBodyTemp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ItemID { get; set; }
        public int GINNo { get; set; }
        public int GINBodyNo { get; set; }
        public string Code { get; set; }
        public string ItemName { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Qty { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UnitCost { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Cost { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Discount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Sellingprice { get; set; }
        public DateTime? ExpDate { get; set; }
        public string UserID { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
    }
}
