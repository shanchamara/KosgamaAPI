using Microsoft.AspNetCore.Identity;
using CommonStockManagementDatabase.Model;

namespace CommonStockManagementDatabase.Context
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDateTime { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public string Address { get; set; }
        public DateTime? Join_date { get; set; }
        public string Designation { get; set; }
        public string NIC_no { get; set; }
        public DateTime LastLoginDate { get; set; }

        public bool AcceptTerms { get; set; } = false;
        public string Employee_Number { get; set; }
        public string ImageURl { get; set; }

        public int? CompanyId { get; set; } = 0;
    }



}
