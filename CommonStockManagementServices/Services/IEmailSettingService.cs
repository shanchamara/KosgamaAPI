using Microsoft.EntityFrameworkCore;
using CommonStockManagementDatabase.Context;
using StockManagementApi.DatabaseConnection;
using CommonStockManagementDatabase.Model;

namespace CommonStockManagementServices.Services
{




    public class EmailSettingService
    {
        public EmailSettingService(AppDbContext context)
        {
            _context = context;
        }

        private readonly AppDbContext _context;

        public async Task<List<Emailsetting>> GetAll()
        {
            return await (from a in _context.Tblemailsetting
                          select new Emailsetting()
                          {
                              Email = a.Email,
                              Password = a.Password,
                              Port = a.port,
                              Host = a.host,
                              Isdeleted = a.Isdeleted,
                              Edit_By = a.Edit_By,
                              Delete_By = a.Delete_By,
                              Edit_Date = a.Edit_Date,
                              Delete_Date = (DateTime)a.Delete_Date,
                              YourDomain = a.YourDomain,
                          }).Where(d => d.Isdeleted.Equals(false)).ToListAsync();
        }

        public async Task<Emailsetting> GetDetailsById(int id)
        {
            return await (from a in _context.Tblemailsetting
                          select new Emailsetting()
                          {
                              Id = a.Id,
                              Host = a.host,
                              Email = a.Email,
                              Port = a.port,
                              Password = a.Password,
                              Isdeleted = a.Isdeleted,
                              Edit_By = a.Edit_By,
                              Delete_By = a.Delete_By,
                              Edit_Date = a.Edit_Date,
                              Delete_Date = (DateTime)a.Delete_Date,
                              YourDomain = a.YourDomain,
                          }).Where(d => d.Isdeleted.Equals(false) && d.Id.Equals(id)).SingleOrDefaultAsync();
        }


        public async Task<Message<string>> RegisterAsync(Insertemailsetting model)
        {
            try
            {
                var setting = new TblEmailsetting
                {
                    Email = model.Email,
                    Password = model.Password,
                    port = model.Port,
                    host = model.Host,
                    Edit_By = model.Edit_By,
                    Edit_Date = model.Edit_Date,
                    YourDomain = model.YourDomain,
                };

                _context.Tblemailsetting.Add(setting);
                await _context.SaveChangesAsync();

                return new Message<string>() { Status = "S", Text = $"Setting added successfully. Setting ID is {setting.Id}", Result = setting.Id.ToString() };

            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }

        public TblEmailsetting GetById(int id)
        {
            return _context.Tblemailsetting.SingleOrDefault(d => d.Id.Equals(id));
        }



        public async Task<Message<string>> UpdateAsync(Updateemailsetting model)
        {
            try
            {
                var existSetting = GetById(model.Id);
                existSetting.Email = model.Email != null ? model.Email : existSetting.Email;
                existSetting.Password = model.Password != null ? model.Password : existSetting.Password;
                existSetting.host = model.Host != null ? model.Host : existSetting.host;
                existSetting.port = model.Port != null ? model.Port : existSetting.port;
                existSetting.Isdeleted = model.Isdeleted;
                existSetting.Edit_By = model.Edit_By != null ? model.Edit_By : existSetting.Edit_By;
                existSetting.Delete_By = model.Delete_By;
                existSetting.Edit_Date = model.Edit_Date;
                existSetting.Delete_Date = model.Delete_Date;
                existSetting.YourDomain = model.YourDomain;
                await _context.SaveChangesAsync();


                return new Message<string>()
                {
                    Status = "S",
                    Text = $"Setting {existSetting.Id} details updated successfully",
                    Result = existSetting.Id.ToString()

                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Update error {ex.Message}" };
            }


        }
    }
}
