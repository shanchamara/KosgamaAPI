using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Model;
using CommonStockManagementDatabase.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CommonStockManagementServices.Services
{

    public class UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, AppDbContext context, SentemailService sentEmailServies, AuditTrailService auditTrailService, IMemoryCache cache)
    {
        private readonly AppDbContext _context = context;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly JWT _jwt = jwt.Value;
        public const string AD_Server_IP = "inseegroup.com";
        private readonly SentemailService _sentEmailServies = sentEmailServies;
        private readonly AuditTrailService _auditTrailService = auditTrailService;
        private readonly IMemoryCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));


        private void InvalidateCache(string methodName)
        {
            _cache.Remove(methodName); // Invalidate cache for the specified method
        }

        public async Task<PaginationListData> GetAllPagination(int page, int pagecount, string searchTerm, string sort = null, string order = null)
        {
            var cacheKey = $"{nameof(PaginationListData)}";
            // Check if the result is already in the cache

            IQueryable<Account> query = null;
            if (_cache.TryGetValue(cacheKey, out PaginationListData cachedResult))
            {

                query = cachedResult.Account.AsQueryable();

                // Apply sorting based on sort and order parameters

                Expression<Func<Account, object>> defaultSort1 = x => x.Code;

                query = QueryHelper.ApplySort(query, sort, order, defaultSort1);


                // var filteredData1 = await query.Where(d => !d.Isdelete).ToListAsync();

                var filteredData1 = QueryHelper.SearchUtility<Account>.Search([.. query], searchTerm, c => c.FirstName, c => c.Email, c => c.LastName, c => c.PhoneNumber);

                // Project the filtered data to 
                var pagedData1 = filteredData1
                       .Select(a => new Account
                       {

                           Code = a.Code,
                           FirstName = a.FirstName,
                           Email = a.Email,
                           PhoneNumber = a.PhoneNumber,
                           LastName = a.LastName,
                           LastLoginDate = a.LastLoginDate,
                           LockoutEnabled = a.LockoutEnabled,
                           Role = a.Role,
                           Join_date = a.Join_date,
                           //Avatar = "avatars/300-6.jpg",
                           AccountStatus = a.AccountStatus,
                           ImageURl = a.ImageURl,
                           Isdeleted = a.Isdeleted
                       })
                       .ToList();

                // Prepare the pagination response

                var paginationListData1 = new PaginationListData
                {
                    Count = filteredData1.Count,
                    Account = pagedData1
                };

                return paginationListData1;
            }
            else
            {
                query = (from a in _context.Users
                         join d in _context.UserRoles on a.Id equals d.UserId into ad
                         from d in ad.DefaultIfEmpty()
                         join x in _context.Roles on d.RoleId equals x.Id into dx
                         from x in dx.DefaultIfEmpty()
                         orderby a.Id descending
                         where (string.IsNullOrEmpty(searchTerm) || a.Email.Contains(searchTerm) || a.PhoneNumber.Contains(searchTerm) || a.LastName.Contains(searchTerm)
                         || a.FirstName.Contains(searchTerm) || x.Name.Contains(searchTerm))
                         select new Account()
                         {
                             Code = a.Id,
                             FirstName = a.FirstName,
                             Email = a.Email,
                             PhoneNumber = a.PhoneNumber,
                             LastName = a.LastName,
                             LastLoginDate = a.LastLoginDate.ToString("yyyy-MM-dd") == "0001-01-01" ? "" : a.LastLoginDate.ToString("yyyy-MM-dd hh:mm:ss tt"),
                             LockoutEnabled = (DateTime.Now < a.LockoutEnd) ? true : false,
                             Role = x.Name,
                             Join_date = a.Join_date.Value.ToString("yyyy-MM-dd") == "0001-01-01" ? "" : a.Join_date.Value.ToString("yyyy-MM-dd"),
                             //Avatar = "avatars/300-6.jpg",
                             AccountStatus = (DateTime.Now < a.LockoutEnd) ? "Not Active" : "Active",
                             ImageURl = a.ImageURl,
                             Isdeleted = a.LockoutEnabled ? true : false,
                         }).AsQueryable();

            }

            // Apply sorting based on sort and order parameters

            Expression<Func<Account, object>> defaultSort = x => x.Code;

            query = QueryHelper.ApplySort(query, null, null, defaultSort);

            // Filter out deleted records and fetch the data

            var filteredData = await query.Where(d => !d.Isdeleted).ToListAsync();

            filteredData = QueryHelper.SearchUtility<Account>.Search([.. query], searchTerm, c => c.FirstName, c => c.Email, c => c.LastName, c => c.PhoneNumber);

            // Project the filtered data to 
            var pagedData = filteredData
              .Select(a => new Account
              {
                  Code = a.Code,
                  FirstName = a.FirstName,
                  Email = a.Email,
                  PhoneNumber = a.PhoneNumber,
                  LastName = a.LastName,
                  LastLoginDate = a.LastLoginDate,
                  LockoutEnabled = a.LockoutEnabled,
                  Role = a.Role,
                  Join_date = a.Join_date,
                  //Avatar = "avatars/300-6.jpg",
                  AccountStatus = a.AccountStatus,
                  ImageURl = a.ImageURl,
                  Isdeleted = a.Isdeleted
              })
              .ToList();

            // Prepare the pagination response

            var paginationListData = new PaginationListData
            {
                Count = filteredData.Count,
                Account = pagedData
            };

            // Cache the result

            var CacheResult = new PaginationListData
            {
                Count = filteredData.Count,
                Account = filteredData
            };
            _cache.Set(cacheKey, CacheResult, TimeSpan.FromMinutes(30)); // Cache for 10 minutes, adjust as needed

            return paginationListData;



        }

        public async Task<List<Account>> GetAllAdminAndUserOnly()
        {
            var dt = await (from a in _context.Users
                            where (a.LockoutEnabled == false)
                            join d in _context.UserRoles on a.Id equals d.UserId into ad
                            from d in ad.DefaultIfEmpty()
                            join x in _context.Roles on d.RoleId equals x.Id into dx
                            from x in dx.DefaultIfEmpty()
                            orderby a.Id descending
                            //where (x.Name == "Admin" || x.Name == "User") && a.LockoutEnabled == false
                            select new Account()
                            {
                                Code = a.Id,
                                FirstName = a.FirstName,
                                Email = a.Email,
                                PhoneNumber = a.PhoneNumber,
                                LockoutEnabled = (DateTime.Now < a.LockoutEnd) ? true : false,
                                Role = x.Name
                            }).ToListAsync();

            dt = dt.Where(d => d.Role == "Admin" || d.Role == "User" || d.Role == "Senior Maneger" || d.Role == "Manager").ToList();
            return dt;
        }

        public async Task<List<Account>> GetAllusersLowLevel()
        {
            var dt = await (from a in _context.Users
                            where (a.LockoutEnabled == false)
                            join d in _context.UserRoles on a.Id equals d.UserId into ad
                            from d in ad.DefaultIfEmpty()
                            join x in _context.Roles on d.RoleId equals x.Id into dx
                            from x in dx.DefaultIfEmpty()
                            orderby a.Id descending
                            //where (x.Name == "Admin" || x.Name == "User") && a.LockoutEnabled == false
                            select new Account()
                            {
                                Code = a.Id,
                                FirstName = a.FirstName,
                                Email = a.Email,
                                PhoneNumber = a.PhoneNumber,
                                LockoutEnabled = (DateTime.Now < a.LockoutEnd) ? true : false,
                                Role = x.Name
                            }).ToListAsync();

            dt = dt.Where(d => d.Role == "User" || d.Role == "Senior Maneger" || d.Role == "Junior" || d.Role == "Tax consultant").ToList();
            return dt;
        }

        public async Task<List<IdentityRole>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public AppUser GetByEmail(string email)
        {
            return _context.Users.SingleOrDefault(d => d.Email.Equals(email));
        }


        public async Task<Message<string>> UpdateAsync(EditAccount model)
        {
            var existUser = GetById(model.Code);
            var roles = await _userManager.GetRolesAsync(existUser);

            string[] imageProperties = { "ImageURl" };

            if (model.Image is not null)
            {
                var images = new[] { model.Image };
                SaveImages.SetImageUrl(model, images, imageProperties, "User");
            }

            existUser.FirstName = model.FirstName;
            existUser.LastName = model.LastName;
            existUser.PhoneNumber = model.PhoneNumber;
            existUser.LockoutEnd = (model.LockoutEnabled) ? new DateTime(9999, 12, 31) : DateTime.Now.AddDays(-1);
            existUser.Address = model.Address;
            existUser.NIC_no = model.NIC_no;
            existUser.Join_date = model.Join_date;
            existUser.Email = model.Email;
            existUser.Designation = model.Designation;
            existUser.LockoutEnabled = (model.LockoutEnabled) ? true : false;
            existUser.Employee_Number = model.Employee_Number;
            existUser.ImageURl = model.ImageURl;
            existUser.CompanyId = model.CompanyId;

            if (model.LockoutEnabled)
            {
                await _sentEmailServies.SentEmailUserAccountTemporarilyDisable(model.ModifiedBy, model.Code, "");
            }
            try
            {
                var result = await _userManager.UpdateAsync(existUser);
                if (result.Succeeded)
                {
                    if (roles.Count > 0)
                    {
                        if (model.Role != roles.First())
                        {
                            foreach (var role in roles)
                            {
                                await _userManager.RemoveFromRoleAsync(existUser, role);
                            }
                            await _userManager.AddToRoleAsync(existUser, model.Role);

                        }
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(existUser, model.Role);
                    }
                    InvalidateCache(nameof(PaginationListData));
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"User has been updated successfully",
                        Result = existUser.Id.ToString()

                    };

                }
                return new Message<string>()
                { Text = $"Update error {result?.Errors?.FirstOrDefault()?.Description}" };
            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Update error {ex.Message}" };
            }
        }

        public async Task<Message<string>> RegisterAsync(InsertAccount model)
        {
            try
            {
                if (model.Image != null && model.Image.Length > 0)
                {
                    if (model.ImageURl != "Select.png" && model.ImageURl != null && model.ImageURl != model.ImageURl2)
                    {
                        File.Delete(CommonResources.System_File_Path + "/User/" + model.ImageURl);
                    }

                    var type = model.Image.FileName.Split(".").Last();
                    model.ImageURl = string.Format("{0}.{1}", Guid.NewGuid().ToString(), type);
                    using (var fileStream = new FileStream(string.Format("{0}/{1}", CommonResources.System_File_Path + "/User/", model.ImageURl), FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    }
                    ;
                }
                else model.ImageURl ??= "Select.png";

                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = "123456",
                    ModifiedBy = model.ModifiedBy,
                    Address = model.Address,
                    NIC_no = model.NIC_no,
                    Join_date = model.Join_date,
                    Designation = model.Designation,
                    LockoutEnabled = true,
                    LockoutEnd = new DateTime(9999, 12, 31),
                    Employee_Number = model.Employee_Number,
                    ImageURl = model.ImageURl,
                    CompanyId = model.CompanyId
                };

                var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userWithSameEmail == null)
                {
                    // user.UserName = await CheckADAsync(model.Email.ToLower().Trim());

                    var result = await _userManager.CreateAsync(user, "123456");
                    if (result.Succeeded)
                    {
                        if (model.Role != null)
                        {
                            await _userManager.AddToRoleAsync(user, model.Role);
                        }

                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        var sentemail = await _sentEmailServies.UserRegistrationEmail(model.Email, model.FirstName + model.LastName, model.ModifiedBy, token);

                        InvalidateCache(nameof(PaginationListData));
                        return new Message<string>()
                        {
                            Status = "S",
                            Text = $"User has been added successfully.",
                            Result = user.Id.ToString()
                        };
                    }
                    else
                    {
                        return new Message<string>() { Text = result?.Errors?.First().Description };
                    }
                }
                return new Message<string>() { Text = $"Email {user.Email} is already registered." };
            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }

        public async Task<Message<string>> UserSignUpAsync(InsertAccount model)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                LastName = model.LastName,
                FirstName = model.FirstName,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = model.Password,
                Address = model.Address,
                NIC_no = model.NIC_no,
                Join_date = model.Join_date,
                Designation = model.Designation,
                LockoutEnabled = true,
                LockoutEnd = new DateTime(9999, 12, 31),
                AcceptTerms = true,
            };
            try
            {
                var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userWithSameEmail == null)
                {
                    // user.UserName = await CheckADAsync(model.Email.ToLower().Trim());

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        if (model.Role != null)
                        {
                            await _userManager.AddToRoleAsync(user, model.Role);
                        }

                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        var sentemail = await _sentEmailServies.UserSignupEmail(model.Email, model.FirstName + model.LastName, token);

                        return new Message<string>()
                        {
                            Status = "S",
                            Text = $"Your account has been created. Please Check your email",
                            Result = user.Id.ToString()
                        };
                    }
                    else
                    {
                        return new Message<string>() { Text = result?.Errors?.First().Description };
                    }
                }
                return new Message<string>() { Text = $"Email {user.Email} is already registered." };
            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }

        // Login method
        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            var authenticationModel = new AuthenticationModel();
            var IsAuthorized = false;
            int failedAttempts = 0;
            try
            {


                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    // Log audit trail
                    _auditTrailService.LogAudit("EntityName", "Update", $"Entity with ID {user.Id} was updated.", "UserId");


                    var userDetails = new AppUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        FirstName = user.FirstName,
                        PhoneNumber = user.PhoneNumber,
                    };
                    var signIn = await _signInManager.PasswordSignInAsync(user, model.Password, false, model.RememberMe);
                    var isLockedOut = await _userManager.IsLockedOutAsync(user);
                    if (isLockedOut)
                    {
                        await _sentEmailServies.SentEmailAccountLockNotification(user.Id);
                        authenticationModel.IsAuthenticated = false;
                        authenticationModel.Message = $"an account that has been temporarily suspended or blocked for a certain period of time {model.Email}.";
                        return authenticationModel;
                    }
                    else
                    {
                        if (signIn.Succeeded)
                        {
                            //update Succesfuly last login 

                            user.LastLoginDate = CommonResources.LocalDatetime(); // Update last login date
                            await _signInManager.UserManager.UpdateAsync(user);

                            if (user == null)
                            {
                                user = await _userManager.FindByEmailAsync(String.Format("{0}@siamcitycement.com", model.Email).ToLower());
                                if (user == null)
                                {

                                    authenticationModel.IsAuthenticated = false;
                                    authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                                    return authenticationModel;
                                }
                            }


                            //if (user.Email.ToLower().Trim().Equals("admin@overleap.com"))
                            //{
                            //    // overleadp
                            //    if (await _userManager.CheckPasswordAsync(user, model.Password))
                            //    {
                            //        IsAuthorized = true;
                            //    }
                            //} 
                            //if (model.Email.Split('@').Count() == 1 && await CheckUsernameinADAsync(model.Email, model.Password))
                            //{
                            //    IsAuthorized = true;
                            //}
                            //else if (await CheckEmailinADAsync(user, model.Password))
                            //{

                            IsAuthorized = true;
                            //}

                            if (IsAuthorized)
                            {
                                if (user.LockoutEnabled && user.LockoutEnd > DateTime.Now)
                                {
                                    authenticationModel.IsAuthenticated = false;
                                    authenticationModel.Message = $"User account blocked, Please contact system admin";
                                    return authenticationModel;
                                }

                                authenticationModel.IsAuthenticated = true;
                                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                                authenticationModel.Email = user.Email;
                                authenticationModel.UserName = user.UserName;
                                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                                authenticationModel.Roles = rolesList.ToList();


                                if (user.RefreshTokens.Any(a => a.IsActive))
                                {
                                    var activeRefreshToken = user.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                                    authenticationModel.RefreshToken = activeRefreshToken.Token;
                                    authenticationModel.RefreshTokenExpiration = activeRefreshToken.Expires;
                                }
                                else
                                {
                                    var refreshToken = CreateRefreshToken();
                                    authenticationModel.RefreshToken = refreshToken.Token;
                                    authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                                    user.RefreshTokens.Add(refreshToken);
                                    _context.Update(user);
                                    _context.SaveChanges();
                                }

                                return authenticationModel;
                            }
                        }
                        else
                        {
                            failedAttempts = await _userManager.GetAccessFailedCountAsync(user);
                            user.AccessFailedCount = failedAttempts == 0 ? 1 : failedAttempts + 1;
                            var updateUserTryfaildcont = await _userManager.UpdateAsync(user);
                            failedAttempts = await _userManager.GetAccessFailedCountAsync(user);

                            if (failedAttempts >= 3)
                            {
                                user.LockoutEnabled = true;
                                user.LockoutEnabled = (user.LockoutEnabled) ? true : false;
                                user.LockoutEnd = (user.LockoutEnabled) ? new DateTime(9999, 12, 31) : DateTime.Now.AddDays(-1);
                                updateUserTryfaildcont = await _userManager.UpdateAsync(user);

                                authenticationModel.IsAuthenticated = false;
                                authenticationModel.Message = $"Incorrect Credentials for user {model.Email} Please check the email address and password and try again. If you believe this is an error or have any questions, please contact the administrator.\r\n.";
                                return authenticationModel;
                            }
                        }
                    }

                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = $"Incorrect Credentials for user {model.Email}. You have {3 - failedAttempts} more attempts left before your account gets locked for security reasons.";
                    return authenticationModel;
                }
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"Incorrect Credentials for user {model.Email} Please check the email address and password and try again. If you believe this is an error or have any questions, please contact the administrator.\r\n.";
                return authenticationModel;
            }
            catch (Exception ex)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = ex.Message;
                return authenticationModel;
            }
        }


        public async Task<Message<string>> CopyRolesAsync(string sourceUserId, string targetUserId)
        {
            var sourceUser = await _userManager.FindByIdAsync(sourceUserId);
            var targetUser = await _userManager.FindByIdAsync(targetUserId);

            if (sourceUser == null || targetUser == null)
            {
                // Handle user not found scenario
                return new Message<string>()
                {
                    Status = "S",
                    Text = $"User doesn't have Roles.",
                    Result = ""
                };
            }

            var sourceRoles = await _userManager.GetRolesAsync(sourceUser);

            foreach (var role in sourceRoles)
            {
                // Add each role to the target user
                await _userManager.AddToRoleAsync(targetUser, role);
            }

            return new Message<string>()
            {
                Status = "S",
                Text = $"User Role Copy .",
                Result = ""
            };
        }

        public async Task<Message<string>> SentEmailPasswdResetLink(ConfirmRegistration model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return new Message<string>() { Status = "E", Text = "Invalid email address" };
                }

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var sentemail = await _sentEmailServies.SentPasswdResetLinkEmail(model.Email, user.FirstName, token);
                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Sent password reset link {sentemail}. Please check your email ",
                    Result = token
                };

            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }

        public async Task<AuthenticationModel> ResetPasswordandTokenAsync(ConfirmRegistration model)
        {
            var authenticationModel = new AuthenticationModel();
            var IsAuthorized = false;
            try
            {

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    authenticationModel.Message = $"Invalid email address  {model.Email}.";
                }
                else
                {
                    var result = await _userManager.ConfirmEmailAsync(user, model.Token);


                    if (result.Succeeded)
                    {


                        if (user == null)
                        {
                            user = await _userManager.FindByEmailAsync(String.Format("{0}@siamcitycement.com", model.Email).ToLower());
                            if (user == null)
                            {

                                authenticationModel.IsAuthenticated = false;
                                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                                return authenticationModel;
                            }
                        }
                        // user.LockoutEnabled= false;
                        IsAuthorized = true;


                        if (IsAuthorized)
                        {

                            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                            //authenticationModel.Roles = rolesList.ToList();

                            // var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                            var Resetpasswordresult = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                            if (rolesList.Count == 0)
                            {
                                await _userManager.AddToRoleAsync(user, "User");
                            }

                            if (Resetpasswordresult.Succeeded)
                            {

                                var signIn = await _signInManager.PasswordSignInAsync(user, model.NewPassword, false, lockoutOnFailure: false);
                                if (signIn.Succeeded)
                                {
                                    if (user.LockoutEnabled && user.LockoutEnd > DateTime.Now)
                                    {
                                        authenticationModel.IsAuthenticated = false;
                                        authenticationModel.Message = $"User account blocked, Please contact system admin";
                                        return authenticationModel;
                                    }

                                    authenticationModel.IsAuthenticated = true;
                                    JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                                    authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                                    authenticationModel.Email = user.Email;
                                    authenticationModel.UserName = user.UserName;
                                    rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                                    authenticationModel.Roles = rolesList.ToList();




                                    if (user.RefreshTokens.Any(a => a.IsActive))
                                    {
                                        var activeRefreshToken = user.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                                        authenticationModel.RefreshToken = activeRefreshToken.Token;
                                        authenticationModel.RefreshTokenExpiration = activeRefreshToken.Expires;
                                        _context.Update(user);
                                        _context.SaveChanges();
                                    }
                                    else
                                    {
                                        var refreshToken = CreateRefreshToken();
                                        authenticationModel.RefreshToken = refreshToken.Token;
                                        authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                                        user.RefreshTokens.Add(refreshToken);
                                        user.LockoutEnabled = false;
                                        _context.Update(user);
                                        _context.SaveChanges();
                                    }
                                }
                            }
                            return authenticationModel;

                        }
                    }
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";

                }
                return authenticationModel;

            }
            catch (Exception ex)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = ex.Message;
                return authenticationModel;
            }
        }


        public async Task<AuthenticationModel> ResetPasswordOnlyAuthorizedAsync(ConfirmRegistration model)
        {
            var authenticationModel = new AuthenticationModel();
            var IsAuthorized = false;
            try
            {

                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    authenticationModel.Message = $"Invalid email address  {model.Email}.";
                }
                else
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var result = await _userManager.ConfirmEmailAsync(user, token);


                    if (result.Succeeded)
                    {


                        if (user == null)
                        {
                            user = await _userManager.FindByEmailAsync(String.Format("{0}@siamcitycement.com", model.Email).ToLower());
                            if (user == null)
                            {

                                authenticationModel.IsAuthenticated = false;
                                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                                return authenticationModel;
                            }
                        }
                        // user.LockoutEnabled= false;
                        IsAuthorized = true;


                        if (IsAuthorized)
                        {
                            var Resetpasswordresult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                            if (Resetpasswordresult.Succeeded)
                            {


                                authenticationModel.IsAuthenticated = true;
                                //JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                                authenticationModel.Token = null;
                                authenticationModel.Email = user.Email;
                                authenticationModel.UserName = user.UserName;
                                //rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                                authenticationModel.Roles = null;
                                authenticationModel.Message = Resetpasswordresult.ToString();

                            }
                            else
                            {
                                authenticationModel.IsAuthenticated = false;
                                authenticationModel.Message = Resetpasswordresult.ToString();
                            }
                            return authenticationModel;

                        }
                    }
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";

                }
                return authenticationModel;

            }
            catch (Exception ex)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = ex.Message;
                return authenticationModel;
            }
        }


        public async Task<AuthenticationModel> ChangeEmailOnlyAuthorizedAsync(ConfirmChangeEmail model)
        {
            var authenticationModel = new AuthenticationModel();
            var IsAuthorized = false;
            try
            {

                var user = await _userManager.FindByEmailAsync(model.OldEmail);

                if (user == null)
                {
                    authenticationModel.Message = $"Invalid email address  {model.OldEmail}.";
                }
                else
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var result = await _userManager.ConfirmEmailAsync(user, token);


                    if (result.Succeeded)
                    {


                        if (user == null)
                        {
                            user = await _userManager.FindByEmailAsync(String.Format("{0}@siamcitycement.com", model.OldEmail).ToLower());
                            if (user == null)
                            {

                                authenticationModel.IsAuthenticated = false;
                                authenticationModel.Message = $"No Accounts Registered with {model.OldEmail}.";
                                return authenticationModel;
                            }
                        }
                        // user.LockoutEnabled= false;
                        IsAuthorized = true;


                        if (IsAuthorized)
                        {
                            var Newtoken = await _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
                            var Resetpasswordresult = await _userManager.ChangeEmailAsync(user, model.NewEmail, Newtoken);

                            if (Resetpasswordresult.Succeeded)
                            {
                                user.UserName = model.NewEmail;
                                user.NormalizedUserName = model.NewEmail;
                                var newuserupdate = await _userManager.UpdateAsync(user);

                                authenticationModel.IsAuthenticated = true;
                                authenticationModel.Token = null;
                                authenticationModel.Email = user.Email;
                                authenticationModel.UserName = user.UserName;
                                //rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                                authenticationModel.Roles = null;
                                authenticationModel.Message = Resetpasswordresult.ToString();

                            }

                            else
                            {
                                authenticationModel.IsAuthenticated = false;
                                authenticationModel.Message = Resetpasswordresult.ToString();
                            }
                            return authenticationModel;

                        }
                    }
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";

                }
                return authenticationModel;

            }
            catch (Exception ex)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = ex.Message;
                return authenticationModel;
            }
        }

        private static RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(1),
                    Created = DateTime.UtcNow
                };

            }
            ;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("CompanyId",Convert.ToString( user.CompanyId))
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        #region  Add user role
        // Add user role user Wise
        public async Task<Message<string>> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new Message<string>()
                {
                    Status = "S",
                    Text = $"No Accounts Registered with {model.Email}.",
                    Result = user.Id.ToString()
                };
            }

            var roleExists = _context.Roles.SingleOrDefault(d => d.Name.Equals(model.Role.ToLower()));
            var dr = await _context.UserRoles.SingleOrDefaultAsync(d => d.RoleId.Equals(roleExists.Id) && d.UserId.Equals(user.Id));

            if (dr != null)
            {
                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Role {model.Role} is already assigned.",
                    Result = user.Id.ToString()
                };
            }
            else
            {
                if (roleExists != null)
                {
                    var validRole = roleExists.Name;
                    await _userManager.AddToRoleAsync(user, validRole.ToString());
                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"Assign User role has been added successfully.",
                        Result = user.Id.ToString()
                    };
                }
            }
            return new Message<string>()
            {
                Status = "S",
                Text = $"Role {model.Role} not found.",
                Result = user.Id.ToString()
            };
        }

        // Dispaly User Wise user has Role

        public async Task<List<UserRoleModel>> UserHaveRole(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            List<UserRoleModel> userRoleModel = new();

            var dt = await (from a in _context.UserRoles
                            join r in _context.Roles on a.RoleId equals r.Id
                            select new UserRoleModel()
                            {
                                Id = a.RoleId,
                                Name = r.Name,
                                UserId = user.Id,
                                UserName = user.FirstName + user.LastName,
                            }).ToListAsync();


            foreach (var d in dt)
            {
                userRoleModel.Add(d);
            }
            return userRoleModel;
        }

        public async Task<Message<string>> DeleteUserRoleAsync(DeleteUserRoleModel model)
        {
            try
            {
                var roleExists = _context.UserRoles.SingleOrDefault(d => d.UserId.Equals(model.UserId) && d.RoleId.Equals(model.RoleId));
                _context.UserRoles.Remove(roleExists);
                await _context.SaveChangesAsync();

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"User Role has been deleted successfully",
                    Result = ""

                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Delete error {ex.Message}" };
            }
        }

        public async Task<List<string>> GetRolesById(string id)
        {
            var result = await _userManager.GetRolesAsync(_context.Users.Find(id));
            return result.ToList<string>();
        }

        #endregion

        public AppUser GetById(string id)
        {
            return _context.Users.Find(id);
        }



        public async Task<AuthenticationModel> RefreshTokenAsync(string token)
        {
            var authenticationModel = new AuthenticationModel();
            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"Token did not match any users.";
                return authenticationModel;
            }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"Token Not Active.";
                return authenticationModel;
            }

            //Revoke Current Refresh Token
            refreshToken.Revoked = DateTime.UtcNow;

            //Generate new Refresh Token and save to Database
            var newRefreshToken = CreateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            _context.Update(user);
            _context.SaveChanges();

            //Generates new jwt
            authenticationModel.IsAuthenticated = true;
            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
            authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authenticationModel.Email = user.Email;
            authenticationModel.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            authenticationModel.Roles = rolesList.ToList();
            authenticationModel.RefreshToken = newRefreshToken.Token;
            authenticationModel.RefreshTokenExpiration = newRefreshToken.Expires;
            return authenticationModel;
        }

        public async Task<bool> RevokeToken(string token)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            _context.Update(user);
            _context.SaveChanges();

            return true;
        }



        //View Lock account 

        #region View Lock account 
        public async Task<List<Account>> ViewLockaccount()
        {
            var dt = await (from a in _context.Users
                            join d in _context.UserRoles on a.Id equals d.UserId into ad
                            from d in ad.DefaultIfEmpty()
                            join x in _context.Roles on d.RoleId equals x.Id into dx
                            from x in dx.DefaultIfEmpty()
                            orderby a.Id descending
                            select new Account()
                            {
                                Code = a.Id,
                                FirstName = a.FirstName,
                                Email = a.Email,
                                PhoneNumber = a.PhoneNumber,
                                LockoutEnabled = (DateTime.Now < a.LockoutEnd) ? true : false,
                                Role = x.Name
                            }).Where(d => d.LockoutEnabled == true).ToListAsync();
            return dt;
        }

        public async Task<Message<string>> UpdateLockaccountAsync(EditAccount model)
        {
            var existUser = GetById(model.Code);
            var roles = await _userManager.GetRolesAsync(existUser);

            existUser.LockoutEnd = DateTime.Now.AddDays(-1);
            existUser.LockoutEnabled = false;
            existUser.AccessFailedCount = 0;

            if (model.LockoutEnabled)
            {
                await _sentEmailServies.SentEmailUserAccountTemporarilyDisable(model.ModifiedBy, model.Code, "");
            }
            try
            {
                var result = await _userManager.UpdateAsync(existUser);
                if (result.Succeeded)
                {
                    if (roles.Count > 0)
                    {
                        if (model.Role != roles.First())
                        {
                            foreach (var role in roles)
                            {
                                await _userManager.RemoveFromRoleAsync(existUser, role);
                            }
                            await _userManager.AddToRoleAsync(existUser, model.Role);

                        }
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(existUser, model.Role);
                    }

                    return new Message<string>()
                    {
                        Status = "S",
                        Text = $"User has been updated successfully",
                        Result = existUser.Id.ToString()

                    };
                }
                return new Message<string>()
                { Text = $"Update error {result?.Errors?.FirstOrDefault()?.Description}" };
            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Update error {ex.Message}" };
            }
        }



        // only disable Account 
        public async Task<AuthenticationModel> DisableAccountAsync(AccountLock model)
        {
            var authenticationModel = new AuthenticationModel();
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    user.LockoutEnabled = true;
                    user.LockoutEnd = new DateTime(9999, 12, 31);
                    authenticationModel.IsAuthenticated = false;
                    user.AccessFailedCount = 3;
                    await _userManager.UpdateAsync(user);
                    authenticationModel.Message = $"User has been updated successfully.";
                    return authenticationModel;
                }
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                return authenticationModel;

            }
            catch (Exception ex)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = ex.Message;
                return authenticationModel;
            }
        }

        public async Task<AuthenticationModel> DisableAccountAuthirizedAsync(AccountLock model)
        {
            var authenticationModel = new AuthenticationModel();
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    user.LockoutEnabled = true;
                    user.LockoutEnd = new DateTime(9999, 12, 31);
                    authenticationModel.IsAuthenticated = false;
                    user.AccessFailedCount = 3;
                    await _userManager.UpdateAsync(user);
                    authenticationModel.Message = $"Your account has been deactivated.";
                    return authenticationModel;
                }
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                return authenticationModel;

            }
            catch (Exception ex)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = ex.Message;
                return authenticationModel;
            }
        }


        public async Task<AuthenticationModel> DeleteAccountAsync(string code)
        {
            var authenticationModel = new AuthenticationModel();
            try
            {
                var user = await _userManager.FindByIdAsync(code);
                if (user != null)
                {
                    InvalidateCache(nameof(PaginationListData));
                    user.LockoutEnabled = true;
                    user.LockoutEnd = new DateTime(9999, 12, 31);
                    authenticationModel.IsAuthenticated = false;
                    user.AccessFailedCount = 3;
                    await _userManager.UpdateAsync(user);
                    authenticationModel.Message = $"User has been updated successfully.";
                    return authenticationModel;
                }
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {code}.";
                return authenticationModel;

            }
            catch (Exception ex)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = ex.Message;
                return authenticationModel;
            }
        }

        #endregion
    }
}
