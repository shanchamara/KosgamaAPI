using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Model;

namespace CommonStockManagementServices.Services
{

    public class AuditTrailService
    {
        private readonly AppDbContext _dbContext;

        public AuditTrailService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void LogAudit(string entityName, string action, string details, string userId)
        {
            var auditTrail = new TblAuditTrail
            {
                EntityName = entityName,
                Action = action,
                Details = details,
                Timestamp = CommonResources.LocalDatetime(),
                UserId = userId
            };

            _dbContext.TblAuditTrails.Add(auditTrail);
            _dbContext.SaveChanges();
        }
    }

}
