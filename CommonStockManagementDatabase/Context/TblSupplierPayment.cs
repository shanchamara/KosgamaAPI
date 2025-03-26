using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblSupplierPayment")]
    public class TblSupplierPayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("FKSupplierID")]
        public int FKSupplierID { get; set; }
        [ForeignKey("FKSupplierID")]
        public TblSupplier TblSupplier { get; set; }

        public DateTime Date { get; set; }
        public string Ref_invoive { get; set; }
        public int GRNNo { get; set; }

        public double Total { get; set; }
        public double Pay { get; set; }
        public double Balance { get; set; }
    }
}
