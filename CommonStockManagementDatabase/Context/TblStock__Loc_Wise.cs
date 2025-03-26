using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Context
{
    [Table("TblStock__Loc_Wise")]
    public class TblStock__Loc_Wise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int StockId { get; set; }  // FK Key
        [Column(TypeName = "VARCHAR(50)")]
        public string StockTable { get; set; }
        public int? LotId { get; set; }  // FK Key

        public int LocationID { get; set; }  // FK Key

        public int Qty { get; set; }
    }
}
