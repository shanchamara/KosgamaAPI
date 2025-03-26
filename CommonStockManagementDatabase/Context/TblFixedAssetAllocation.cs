using System.ComponentModel.DataAnnotations;

namespace CommonStockManagementDatabase.Context
{
    public class TblFixedAssetAllocation
    {
        [Key]
        public int Id { get; set; }


        [MaxLength(65535)] // Adjust the length if necessary
        public string Code { get; set; }

        public DateTime? Date { get; set; }

        [MaxLength(65535)] // Adjust the length if necessary
        public string Department { get; set; }

        [MaxLength(65535)] // Adjust the length if necessary
        public string Employee { get; set; }

        [MaxLength(65535)] // Adjust the length if necessary
        public string Comments { get; set; }
    }
}
