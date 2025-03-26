using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblItemRentalDetailsTemp")]
    public class TblItemRentalDetailsTemp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }

        public int? RentalItemBodyId { get; set; } = 0;
        public string ItemName { get; set; }
        public int ItemID { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Qty { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DayCost { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalCost { get; set; }

        public string UserID { get; set; }


    }
}
