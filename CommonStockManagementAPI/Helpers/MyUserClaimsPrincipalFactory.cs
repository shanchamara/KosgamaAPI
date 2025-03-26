using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using CommonStockManagementDatabase.Context;
using System.Security.Claims;

namespace CommonStockManagementAPI.Helpers
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole>
    {
        //public MyUserClaimsPrincipalFactory(
        //    UserManager<Context.DbUser> userManager,
        //    IOptions<IdentityOptions> optionsAccessor)
        //    : base(userManager, optionsAccessor)
        //{
        //}

        public MyUserClaimsPrincipalFactory(
       UserManager<AppUser> userManager,
       RoleManager<IdentityRole> roleManager,
       IOptions<IdentityOptions> optionsAccessor)
       : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);


            //using (var con = new AppDbContext())
            //{
            //    Role = con.Users.Where(d => d.Id.Equals(user.Id)).
            //              FirstOrDefault().Id;
            //};

            // await _userManager.AddClaimAsync(user, new Claim("MyClaimType", "MyClaimValue"));
            identity.AddClaim(new Claim("FirstName", "mmmmmmm"));
            //identity.AddClaim(new Claim("Usertype", Role ?? "NA"));
            return identity;
        }
    }
}
