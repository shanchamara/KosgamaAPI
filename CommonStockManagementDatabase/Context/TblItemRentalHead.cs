using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblItemRentalHead")]
    public class TblItemRentalHead
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime? SysDate { get; set; }
        [Column("FKClientId")]
        public int? FKClientId { get; set; }
        [ForeignKey("FKClientId")]
        public TblClient TblClients { get; set; }

        public DateTime? RentalStartDate { get; set; }
        public DateTime? RentalEndDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Discount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Gross { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AdvancePay { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Balance { get; set; }

        public string Description { get; set; }

        public bool IsSettle { get; set; } = false;

        [Column("FKLocationId")]
        public int? FKLocationId { get; set; }  // Fk Key
        [ForeignKey("FKLocationId")]
        public TblCompanyDetails TblCompanyDetails { get; set; }
        public bool IsDelete { get; set; } = false;

        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }
    }
}
