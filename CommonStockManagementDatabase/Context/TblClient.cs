using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    public class TblClient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Area { get; set; }

        public string Email { get; set; }

        public string Nic { get; set; }
        public string Mobile { get; set; }
        public string Tel { get; set; }
        public string Type { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public bool Isdelete { get; set; } = false;
        public string ImageURl { get; set; }
        public string Dr { get; set; }
        public string Cr { get; set; }
        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }

        // relationShip

        //public ICollection<DbPtHistory> DbPtHistories { get; set; }
        //public ICollection<DbPtInvestigationsDone> DbPtInvestigationsDones { get; set; }
        //public ICollection<DbPtPrescription> DbPtPrescriptions { get; set; }
        //public ICollection<DbPtManagement> DbPtManagements { get; set; }
        //public ICollection<DbPtAttend> DbPtAttends { get; set; }

        //public ICollection<TblCashierDueInvoiceHead> TblCashierDueInvoiceHeads { get; set; }




    }
}
