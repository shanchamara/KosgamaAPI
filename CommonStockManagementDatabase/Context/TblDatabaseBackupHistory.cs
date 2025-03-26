using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblDatabaseBackupHistory")]
    public class TblDatabaseBackupHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public string DatabaseName { get; set; }
        public string Reason { get; set; }
        public string UserName { get; set; }
        public string TagDiscription { get; set; }
        public bool IsDelete { get; set; } = false;
        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }
    }
}
