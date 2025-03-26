using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonStockManagementDatabase.Model;
using CommonStockManagementServices.Services;

namespace CommonStockManagementAPI.Controllers
{
    [ApiController]

    public class AccountController(UserService userService, CompanyDetailsServices companyDetailsServices) : ControllerBase
    {
        private const string API_ROUTE_NAME = "api/accounts";
        private readonly UserService _userService = userService;
        private readonly CompanyDetailsServices _companyDetailsServices = companyDetailsServices;

        #region Profile Management

        // New User Account Registration
        //[Authorize]
        [HttpPost]
        [Route(API_ROUTE_NAME + "/Insert")]
        public async Task<IActionResult> Insert([FromForm] InsertAccount model)
        {
            try
            {
                return Ok(await _userService.RegisterAsync(new InsertAccount()
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Role = model.Role,
                    ModifiedBy = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    ModifiedDateTime = DateTime.Now,
                    Address = model.Address,
                    Designation = model.Designation,
                    Join_date = model.Join_date,
                    NIC_no = model.NIC_no,
                    Employee_Number = model.Employee_Number,
                    CompanyId = model.CompanyId,
                }));
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/UserSignUp")]
        public async Task<IActionResult> UserSignUp([FromBody] InsertAccount model)
        {
            try
            {
                return Ok(await _userService.UserSignUpAsync(new InsertAccount()
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Role = "User",
                    Password = model.Password,
                    //ModifiedBy = User.Identities.First().Claims.Single(s => s.Type == "uid").Value,
                    ModifiedDateTime = CommonResources.LocalDatetime(),
                    Address = model.Address,
                    Designation = model.Designation,
                    Join_date = model.Join_date == Convert.ToDateTime("1/1/0001 12:00:00 AM") ? CommonResources.LocalDatetime() : model.Join_date,
                    NIC_no = model.NIC_no,

                }));
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string search = null, string sort = null, string order = null)
        {
            try
            {
                var Data = await _userService.GetAllPagination(page, items_per_page, search, sort, order);

                var paginationHelper = new PaginationHelper<Account>(items_per_page, Data.Count);
                var paginationInfo = paginationHelper.GetPaginationInfo(page);



                var payload = new Payload
                {
                    Pagination = paginationInfo,
                };

                var response = new DataResponse<List<Account>>
                {
                    Data = Data.Account,
                    Payload = payload
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }


        // Profile Management

        //[Authorize(Policy = "OnlyAdmin")]
        [Authorize]
        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAccountDetailByID/{id}")]
        public async Task<IActionResult> GetAccountDetailByID(string id)
        {
            try
            {
                if (id != "null")
                {
                    var user = _userService.GetById(id);
                    var roles = await _userService.GetRolesById(id);
                    var userModel = new EditAccountDetails()
                    {
                        Code = user.Id,
                        FirstName = user.FirstName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        LockoutEnabled = DateTime.Now < user.LockoutEnd ? true : false,
                        Role = roles.FirstOrDefault(),
                        Address = user.Address,
                        Designation = user.Designation,
                        Join_date = user.Join_date.Value.Date.ToString("yyyy-MM-dd"),
                        NIC_no = user.NIC_no,
                        LastName = user.LastName,
                        LastLoginDate = user.LastLoginDate.ToString("yyyy-MM-dd") == "0001-01-01" ? "" : user.LastLoginDate.ToString("yyyy-MM-dd hh:mm:ss tt"),
                        ImageURl = user.ImageURl,
                        CompanyId = user.CompanyId,

                    };
                    return Ok(new DataResponse<EditAccountDetails> { Data = userModel });
                }
                return Ok(new DataResponse<EditAccountDetails> { Data = null });
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [HttpGet]
        [Route(API_ROUTE_NAME + "/loggeduser")]
        public async Task<IActionResult> GetLoggedUserDetails()
        {
            try
            {
                var user = _userService.GetById(User.Identities.First().Claims.Single(s => s.Type == "uid").Value);
                var roles = await _userService.GetRolesById(User.Identities.First().Claims.Single(s => s.Type == "uid").Value);
                var CompanyDetails = user.CompanyId.HasValue ? _companyDetailsServices.GetById(user.CompanyId.Value) : null;
                var userModel = new EditAccountDetails()
                {
                    Code = user.Id,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    LockoutEnabled = DateTime.Now < user.LockoutEnd ? true : false,
                    Role = roles.FirstOrDefault(),
                    Address = user.Address,
                    Designation = user.Designation,
                    Join_date = user.Join_date.Value.Date.ToString("yyyy-MM-dd"),
                    NIC_no = user.NIC_no,
                    LastName = user.LastName,
                    LastLoginDate = user.LastLoginDate.ToString("yyyy-MM-dd") == "0001-01-01" ? "" : user.LastLoginDate.ToString("yyyy-MM-dd hh:mm:ss tt"),
                    ImageURl = user.ImageURl,
                    CompanyId = user.CompanyId,
                    CompanyName = CompanyDetails.CompanyName
                };
                return Ok(new Message<EditAccountDetails> { Status = "S", Result = userModel });
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [Authorize(policy: "OnlyAdmin")]
        [HttpPut]
        [Route(API_ROUTE_NAME + "/Update")]
        public async Task<IActionResult> Update([FromForm] EditAccount model)
        {
            try
            {
                model.ModifiedBy = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;
                model.ModifiedDateTime = DateTime.Now;
                return Ok(await _userService.UpdateAsync(model));

            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        #endregion

        #region Signin

        [HttpPost]
        [Route(API_ROUTE_NAME + "/token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            var result = await _userService.GetTokenAsync(model);
            if (result.IsAuthenticated)
            {
                SetRefreshTokenInCookie(result.RefreshToken);
            }
            return Ok(result);
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        #endregion

        #region Assign User Role 
        // Assign User Role 
        [HttpPost]
        [Authorize]
        [Route(API_ROUTE_NAME + "/addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            var result = await _userService.AddRoleAsync(model);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetUserRole/{UserId}")]
        public async Task<IActionResult> GetUserRole(string UserId)
        {
            var result = await _userService.UserHaveRole(UserId);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        [Route(API_ROUTE_NAME + "/DeleteUserRole")]
        public async Task<IActionResult> DeleteUserRole(DeleteUserRoleModel model)
        {
            return Ok(await _userService.DeleteUserRoleAsync(model));
        }

        [HttpPost]
        [Authorize]
        [Route(API_ROUTE_NAME + "/CopyRoles/{sourceUserId}/{targetUserId}")]
        public async Task<IActionResult> CopyRoles(string sourceUserId, string targetUserId)
        {
            return Ok(await _userService.CopyRolesAsync(sourceUserId, targetUserId));
        }

        #endregion

        #region View List of Account Lock out USers 

        // View List of Account Lock out USers 
        [Authorize]
        [HttpGet]
        [Route(API_ROUTE_NAME + "/ViewLockaccount")]
        public async Task<IActionResult> GetAllViewLockaccount()
        {
            try
            {
                return Ok(new Message<List<Account>> { Status = "S", Result = await _userService.ViewLockaccount() });
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        [Route(API_ROUTE_NAME + "/EnablelockAccount")]
        public async Task<IActionResult> UpdateLockAccount([FromBody] EditAccount model)
        {
            try
            {
                model.ModifiedBy = User.Identities.First().Claims.Single(s => s.Type == "uid").Value;
                model.ModifiedDateTime = DateTime.Now;
                return Ok(await _userService.UpdateLockaccountAsync(model));

            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        [Route(API_ROUTE_NAME + "/DisableUserAccount")]
        public async Task<IActionResult> DisableUserAccount([FromBody] AccountLock model)
        {

            var result = await _userService.DisableAccountAsync(model);
            if (result.IsAuthenticated)
            {
                SetRefreshTokenInCookie(result.RefreshToken);
            }
            return Ok(result);
        }

        [Authorize]
        [HttpDelete]
        [Route(API_ROUTE_NAME + "/DeleteUserAccount/{code}")]
        public async Task<IActionResult> DeleteUserAccount(string code)
        {

            var result = await _userService.DeleteAccountAsync(code);
            if (result.IsAuthenticated)
            {
                SetRefreshTokenInCookie(result.RefreshToken);
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route(API_ROUTE_NAME + "/DisableUserAccountAuthorized")]
        public async Task<IActionResult> DisableUserAccountAuthorized([FromBody] AccountLock model)
        {

            var result = await _userService.DisableAccountAuthirizedAsync(model);
            return Ok(result);
        }

        #endregion


        #region forgetpassword and Reset Link 
        // forgetpassword and Reset Link 

        [HttpPost]
        [Route(API_ROUTE_NAME + "/ForgetPassword")]
        public async Task<IActionResult> SendEmail(ConfirmRegistration model)
        {
            var result = await _userService.SentEmailPasswdResetLink(model);

            return Ok(result);
        }

        [HttpPost]
        [Route(API_ROUTE_NAME + "/ResetPasswordConfirm")]
        public async Task<IActionResult> ResetPassword(ConfirmRegistration model)
        {
            var result = await _userService.ResetPasswordandTokenAsync(model);
            if (result.IsAuthenticated)
            {
                SetRefreshTokenInCookie(result.RefreshToken);
            }
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route(API_ROUTE_NAME + "/ResetPasswordAuthorizedOnly")]
        public async Task<IActionResult> ResetPasswordAuthorizedOnly(ConfirmRegistration model)
        {
            var result = await _userService.ResetPasswordOnlyAuthorizedAsync(model);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route(API_ROUTE_NAME + "/ChangeEmailAuthorizedOnly")]
        public async Task<IActionResult> ChangeEmailAuthorizedOnly(ConfirmChangeEmail model)
        {
            var result = await _userService.ChangeEmailOnlyAuthorizedAsync(model);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route(API_ROUTE_NAME + "/ChangeDisableUserAccountAuthorizedOnly")]
        public async Task<IActionResult> ChangeDisableUserAccountAuthorizedOnly(ConfirmChangeEmail model)
        {
            var result = await _userService.ChangeEmailOnlyAuthorizedAsync(model);
            return Ok(result);
        }
        #endregion  
    }
}
