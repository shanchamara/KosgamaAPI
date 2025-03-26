using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using CommonStockManagementDatabase.Context;
using System.Web;

namespace CommonStockManagementServices.Services
{

    public class SentemailService
    {
        private readonly AppDbContext _context;

        public SentemailService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> UserRegistrationEmail(string email, string name, string ModifiedBy, string Token)
        {
            try
            {
                var getEmailSettingDetails = await _context.Tblemailsetting.SingleOrDefaultAsync(d => d.Id == 1);
                var GetEmployee = _context.Users.SingleOrDefault(d => d.Id == ModifiedBy);

                if (getEmailSettingDetails != null)
                {

                    var Message = new MimeMessage();
                    Message.From.Add(new MailboxAddress("ABc Company,\r\nChartered Accountants", getEmailSettingDetails.Email));
                    Message.To.Add(new MailboxAddress(" ABc Company,\r\nChartered Accountants", email));
                    Message.Subject = " Your account information for  ABc Company,\r\nChartered Accountants  ";

                    Message.Body = new TextPart("plain")
                    {
                        Text = $"Dear {name},\r\n\r\nWe are delighted to inform you that your registration with ABc Company,Chartered Accountants is now complete. You can now log in to your account and start using our services.\r\n\r\nHere are your login details:\r\nEmail : {email}\r\n\r\nPlease note that for security reasons, we recommend that you change your password as soon as you log in for the first time. You can do this by visiting your account settings.\r\n\r\nTo log in to your account, please click on the following link {getEmailSettingDetails.YourDomain}/auth/registration?token={HttpUtility.UrlEncode(Token)}. \r\n\r\nIf you have any issues logging in, please contact our support team by sending an email to {getEmailSettingDetails.Email} .\r\n\r\nBest regards,\r\n\r\n{GetEmployee.FirstName}\r\n ABc Company, Chartered Accountants"
                    };

                    // DataBase Backup 

                    //var TransactionDetails = new TblTransactionDetails
                    //{
                    //    Date = CommonResources.LocalDatetime().Date,
                    //    Table_Name = "TblTaxComputation",
                    //    Transaction_details = $"Dear {name},\r\n\r\nWe are delighted to inform you that your registration with ABc Company,Chartered Accountants is now complete. You can now log in to your account and start using our services.\r\n\r\nHere are your login details:\r\nEmail : {email}\r\nPassword: {"123456"}\r\n\r\nPlease note that for security reasons, we recommend that you change your password as soon as you log in for the first time. You can do this by visiting your account settings.\r\n\r\n\r\nTo log in to your account, please click on the following link {getEmailSettingDetails.YourDomain}/auth/registration?token={HttpUtility.UrlEncode(Token)}. If you have any issues logging in, please contact our support team by sending an email to {getEmailSettingDetails.Email} .\r\n\r\nBest regards,\r\n\r\n{GetEmployee.FirstName}\r\n ABc Company, Chartered Accountants"

                    //};
                    //_context.TblTransactionDetails.Add(TransactionDetails);
                    //_context.SaveChanges();

                    using (var client = new SmtpClient())
                    {
                        client.Connect(
                                       getEmailSettingDetails.host
                                      , getEmailSettingDetails.port
                                      , MailKit.Security.SecureSocketOptions.StartTls);

                        client.Authenticate(getEmailSettingDetails.Email, getEmailSettingDetails.Password);

                        client.Send(Message);
                        client.Disconnect(true);
                    };
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Successfuly sent mail";
        }

        public async Task<string> UserSignupEmail(string email, string name, string Token)
        {
            try
            {
                var getEmailSettingDetails = await _context.Tblemailsetting.SingleOrDefaultAsync(d => d.Id == 1);
                var GetEmployee = _context.Users.SingleOrDefault(d => d.Id == "754b08ce-980f-4493-9edd-4258ada4a059");

                if (getEmailSettingDetails != null)
                {

                    var Message = new MimeMessage();
                    Message.From.Add(new MailboxAddress("ABc Company,\r\nChartered Accountants", getEmailSettingDetails.Email));
                    Message.To.Add(new MailboxAddress(" ABc Company,\r\nChartered Accountants", email));
                    Message.Subject = " Your account information for  ABc Company,\r\nChartered Accountants  ";

                    Message.Body = new TextPart("plain")
                    {
                        Text = $"Dear {name},\r\n\r\nWe are delighted to inform you that your registration with ABc Company,Chartered Accountants is now complete. You can now log in to your account and start using our services.\r\n\r\nHere are your login details:\r\nEmail : {email}\r\n\r\nPlease note that for security reasons, we recommend that you change your password as soon as you log in for the first time. You can do this by visiting your account settings.\r\n\r\nTo log in to your account, please click on the following link {getEmailSettingDetails.YourDomain}/auth/registration?token={HttpUtility.UrlEncode(Token)}. \r\n\r\nIf you have any issues logging in, please contact our support team by sending an email to {getEmailSettingDetails.Email} .\r\n\r\nBest regards,\r\n\r\n{GetEmployee.FirstName}\r\n ABc Company, Chartered Accountants"
                    };


                    using (var client = new SmtpClient())
                    {
                        client.Connect(
                                       getEmailSettingDetails.host
                                      , getEmailSettingDetails.port
                                      , MailKit.Security.SecureSocketOptions.StartTls);

                        client.Authenticate(getEmailSettingDetails.Email, getEmailSettingDetails.Password);

                        client.Send(Message);
                        client.Disconnect(true);
                    };
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Successfuly sent mail";
        }

        public async Task<string> SentPasswdResetLinkEmail(string email, string name, string token)
        {
            try
            {
                var getEmailSettingDetails = await _context.Tblemailsetting.SingleOrDefaultAsync(d => d.Id == 1);



                if (getEmailSettingDetails != null)
                {
                    var Message = new MimeMessage();
                    Message.From.Add(new MailboxAddress("ABc Company,\r\nChartered Accountants", getEmailSettingDetails.Email));
                    Message.To.Add(new MailboxAddress(" ABc Company,\r\nChartered Accountants", email));
                    Message.Subject = "Password Reset Request for Your Account ";

                    Message.Body = new TextPart("plain")
                    {
                        Text = $"Dear {name},\r\n\r\nWe have received a request to reset the password for your account with us. If you did not request this, please ignore this email.\r\n\r\nTo reset your password, please click on the link below or copy and paste it into your browser:\r\n\r\n {getEmailSettingDetails.YourDomain}/auth/registration?token={HttpUtility.UrlEncode(token)}  \r\n\r\nIf you have any issues or questions, please contact us at {getEmailSettingDetails.Email} .\r\nThank you,\r\nABc Company,\r\nChartered Accountants team"
                    };

                    // DataBase Backup 

                    //var TransactionDetails = new TblTransactionDetails
                    //{
                    //    Date = CommonResources.LocalDatetime().Date,
                    //    Table_Name = "TblTaxComputation",
                    //    Transaction_details = $"Dear {name},\r\n\r\nWe have received a request to reset the password for your account with us. If you did not request this, please ignore this email.\r\n\r\nTo reset your password, please click on the link below or copy and paste it into your browser:\r\n\r\n {getEmailSettingDetails.YourDomain}/auth/registration?token={HttpUtility.UrlEncode(token)}  \r\n\r\nIf you have any issues or questions, please contact us at {getEmailSettingDetails.Email} .\r\nThank you,\r\nABc Company,\r\nChartered Accountants team"

                    //};
                    //_context.TblTransactionDetails.Add(TransactionDetails);
                    //_context.SaveChanges();

                    using (var client = new SmtpClient())
                    {
                        client.Connect(
                                       getEmailSettingDetails.host
                                      , getEmailSettingDetails.port
                                      , MailKit.Security.SecureSocketOptions.StartTls);

                        client.Authenticate(getEmailSettingDetails.Email, getEmailSettingDetails.Password);

                        client.Send(Message);
                        client.Disconnect(true);
                    };
                }


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Successfuly sent mail";
        }


        public async Task<string> SentEmailUserAccountTemporarilyDisable(string FKStaff_id, string AssigntoUserId, string message)
        {
            var RetrunEmployeeName = "";
            try
            {
                var getEmailSettingDetails = await _context.Tblemailsetting.SingleOrDefaultAsync(d => d.Id == 1);
                var GetEmployee = _context.Users.SingleOrDefault(d => d.Id == FKStaff_id);
                var GetToUser = _context.Users.SingleOrDefault(d => d.Id == AssigntoUserId);


                RetrunEmployeeName = GetToUser.FirstName;

                if (getEmailSettingDetails != null)
                {

                    var Message = new MimeMessage();
                    Message.From.Add(new MailboxAddress("ABc Company,\r\nChartered Accountants", getEmailSettingDetails.Email));
                    Message.To.Add(new MailboxAddress("ABc Company,\r\nChartered Accountants", GetToUser.Email));
                    Message.Subject = $"Deactivate the User ";


                    Message.Body = new TextPart("plain")
                    {
                        Text = $"Dear {RetrunEmployeeName},\r\n\r\nI am writing to inform you that the Administrator has deactivated you, and you are no longer a user of this system.\r\n\r\nI appreciate the effort you put into the tasks and your willingness to contribute to the team. Please let me know if you have any questions or concerns regarding this matter.\r\n\r\nThank you for your understanding and cooperation.\r\n\r\nBest regards,\r\n\r\n{GetEmployee.FirstName}\r\n\r\nABc Company, Chartered Accountants"
                    };

                    // DataBase Backup 

                    //var TransactionDetails = new TblTransactionDetails
                    //{
                    //    Date = CommonResources.LocalDatetime().Date,
                    //    Table_Name = "TblTaxComputation",
                    //    Transaction_details = $"Dear {RetrunEmployeeName} ,\r\n\r\nI am writing to inform you that the Administrator has deactivated you, and you are no longer a user of this system.\r\n\r\nI appreciate the effort you put into the tasks and your willingness to contribute to the team. Please let me know if you have any questions or concerns regarding this matter.\r\n\r\nThank you for your understanding and cooperation.\r\n\r\nBest regards,\r\n\r\n{GetEmployee.FirstName}\r\n\r\nABc Company"

                    //};
                    //_context.TblTransactionDetails.Add(TransactionDetails);
                    //_context.SaveChanges();

                    using (var client = new SmtpClient())
                    {
                        client.Connect(
                                       getEmailSettingDetails.host
                                      , getEmailSettingDetails.port
                                      , MailKit.Security.SecureSocketOptions.StartTls);

                        client.Authenticate(getEmailSettingDetails.Email, getEmailSettingDetails.Password);

                        client.Send(Message);
                        client.Disconnect(true);
                    };
                }


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return RetrunEmployeeName;
        }


        public async Task<string> SentEmailAccountLockNotification(string AssigntoUserId)
        {
            var RetrunEmployeeName = "";
            try
            {
                var getEmailSettingDetails = await _context.Tblemailsetting.SingleOrDefaultAsync(d => d.Id == 1);
                var GetToUser = _context.Users.SingleOrDefault(d => d.Id == AssigntoUserId);


                RetrunEmployeeName = GetToUser.FirstName;

                if (getEmailSettingDetails != null)
                {

                    var Message = new MimeMessage();
                    Message.From.Add(new MailboxAddress("ABc Company,\r\nChartered Accountants", getEmailSettingDetails.Email));
                    Message.To.Add(new MailboxAddress("ABc Company,\r\nChartered Accountants", GetToUser.Email));
                    Message.Subject = $"Account Lock Notification";


                    Message.Body = new TextPart("plain")
                    {
                        Text = $"Dear {GetToUser.FirstName + " " + GetToUser.LastName} ,\r\n\r\nWe hope this email finds you well.\r\n\r\nWe regret to inform you that your account has been temporarily locked due to multiple incorrect password attempts. As a security measure to protect your account and personal information, our system automatically locks the account after three consecutive failed login attempts.\r\n\r\nBest regards,\r\n[Your Company/Organization Name] Support Team"
                    };

                    // DataBase Backup 

                    //var TransactionDetails = new TblTransactionDetails
                    //{
                    //    Date = CommonResources.LocalDatetime().Date,
                    //    Table_Name = "TblTaxComputation",
                    //    Transaction_details = $"Dear {RetrunEmployeeName} ,\r\n\r\nI am writing to inform you that the Administrator has deactivated you, and you are no longer a user of this system.\r\n\r\nI appreciate the effort you put into the tasks and your willingness to contribute to the team. Please let me know if you have any questions or concerns regarding this matter.\r\n\r\nThank you for your understanding and cooperation.\r\n\r\nBest regards,\r\n\r\n{GetEmployee.FirstName}\r\n\r\nABc Company"

                    //};
                    //_context.TblTransactionDetails.Add(TransactionDetails);
                    //_context.SaveChanges();

                    using (var client = new SmtpClient())
                    {
                        client.Connect(
                                       getEmailSettingDetails.host
                                      , getEmailSettingDetails.port
                                      , MailKit.Security.SecureSocketOptions.StartTls);

                        client.Authenticate(getEmailSettingDetails.Email, getEmailSettingDetails.Password);

                        client.Send(Message);
                        client.Disconnect(true);
                    };
                }


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return RetrunEmployeeName;
        }
    }
}
