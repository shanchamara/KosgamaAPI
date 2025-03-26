using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblGRNHead")]
    public class TblGRNHead
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Pono { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string GRNType { get; set; }
        public string RefInv { get; set; }
        public string Created { get; set; }
        [Column("FKSupplier_ID")]
        public int? FKSupplier_ID { get; set; }
        [ForeignKey("FKSupplier_ID")]
        public TblSupplier FKTblSupplier { get; set; }
        public double? Total { get; set; }
        public double? Discount { get; set; }
        public double? Gross { get; set; }

        public bool IsDelete { get; set; } = false;

        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }

        [Column("FKLocationId")]
        public int? FKLocationId { get; set; }  // Fk Key
        [ForeignKey("FKLocationId")]
        public TblCompanyDetails TblCompanyDetails { get; set; }
        public ICollection<TblGRNBody> TblGRNBodies { get; set; }

    }
}
