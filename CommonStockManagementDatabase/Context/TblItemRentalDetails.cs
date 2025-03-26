using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblItemRentalDetails")]
    public class TblItemRentalDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? FKHeadId { get; set; }
        [ForeignKey("FKHeadId")]
        public TblItemRentalHead TblItemRentalHead { get; set; }
        public int ItemID { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Qty { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DayCost { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalCost { get; set; }
        [Column("FKLocationId")]
        public int? FKLocationId { get; set; }  // Fk Key
        [ForeignKey("FKLocationId")]
        public TblCompanyDetails TblCompanyDetails { get; set; }



    }
}
