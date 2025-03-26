using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblSupplier")]
    public class TblSupplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Company { get; set; }
        public string Contact { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string CreditorLedger { get; set; }
        public string AdvanceCreditorLedger { get; set; }
        public string Type { get; set; }
        public string LedgerCode { get; set; }
        public bool IsDelete { get; set; } = false;
        public string ImageURl { get; set; }

        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }


        public ICollection<TblSupplier> FkSuppliers { get; set; }

        public ICollection<TblFixedAssets> FKTblFixedAssets { get; set; }
        // public ICollection<TblSupplierReturnNoteHead> TblSupplierReturnNoteHeads { get; set; }

        public ICollection<TblSupplierPayment> TblSupplierPayments { get; set; }
        public ICollection<TblSupplierDueReturnValue> TblSupplierDueReturnValues { get; set; }
    }
}
