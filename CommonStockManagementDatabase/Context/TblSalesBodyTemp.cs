using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblStockSalesNoteBodyTemp")]
    public class TblSalesBodyTemp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string ItemeNameOrBarandName { get; set; }
        public int? ItemId { get; set; }
        public int? LotId { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public double Qty { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Unit_Cost { get; set; }
        public string UserID { get; set; }

    }
}
