using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CommonStockManagementDatabase.Model;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CommonStockManagementDatabase.Model
{


    public class AuthenticationModel
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
    public class JWT
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
    }

    public class Setting
    {
        public string DefaultUploadPath { get; set; }
    }

    public class AddRoleModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }

    public class DeleteUserRoleModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleId { get; set; }
    }

    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }

    public class InsertAccount : Account
    {
        public new virtual DateTime Join_date { get; set; }
    }
    public class PaginationListData
    {
        public int Count { get; set; }
        public List<Account> Account { get; set; }
    }
    public class Account
    {
        public virtual string FirstName { get; set; } = "";
        public virtual string LastName { get; set; } = "";
        public virtual string LastLoginDate { get; set; } = "";
        public virtual string Join_date { get; set; } = "";
        public virtual string Avatar { get; set; } = "";
        public virtual string AccountStatus { get; set; } = "";

        [EmailAddress]
        public virtual string Email { get; set; } = "";

        public virtual string Role { get; set; } = CommonResources.default_role.ToString();

        public string PhoneNumber { get; set; } = string.Empty;
        public virtual bool LockoutEnabled { get; set; } = false;
        public virtual string Password { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public virtual string Code { get; set; }
        public virtual string NIC_no { get; set; }
        public virtual string Address { get; set; }

        public virtual string Designation { get; set; }
        public virtual bool Isdeleted { get; set; } = false;
        public virtual string Edit_By { get; set; }
        public virtual string Delete_By { get; set; }
        public virtual DateTime Edit_Date { get; set; }
        public virtual DateTime? Delete_Date { get; set; }

        public virtual string Employee_Number { get; set; }
        public IFormFile Image { get; set; }
        public string ImageURl2 { get; set; }
        public string ImageURl { get; set; }
        public int? CompanyId { get; set; } = 0;
        public string CompanyName { get; set; }
    }

    public class EditAccount : Account
    {
        [Required]
        public override string Code { get; set; }

        [Required]
        public override bool LockoutEnabled { get; set; }
        public new virtual DateTime Join_date { get; set; }

    }
    public class EditAccountDetails : Account
    {
        [Required]
        public override string Code { get; set; }

        [Required]
        public override bool LockoutEnabled { get; set; }

        public new string Join_date { get; set; }
    }

    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }

    public class AccountLock
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
    }

    public class ConfirmRegistration
    {
        public string NewPassword { get; set; }
        public string CurrentPassword { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }

    public class ConfirmChangeEmail
    {
        public string NewEmail { get; set; }
        public string OldEmail { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class UserRoleModel : UsersName
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string NormalizedName { get; set; }

        public string Status { get; set; }

    }
    public class PaginationListDatarole
    {
        public int Count { get; set; }
        public List<UserRoleModel> Account { get; set; }
    }
    public class UsersName
    {
        public virtual string UserId { get; set; }

        public virtual string UserName { get; set; }
    }
}
