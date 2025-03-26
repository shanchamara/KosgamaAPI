using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblSupplierDueReturnValue")]
    public class TblSupplierDueReturnValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("FksupplierID")]
        public int FksupplierID { get; set; }
        [ForeignKey("FksupplierID")]
        public TblSupplier TblSupplier { get; set; }

        public DateTime Date { get; set; }

        public string Ref_invoice { get; set; }
        public double Amount { get; set; }

    }
}
