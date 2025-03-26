namespace CommonStockManagementDatabase.Model
{
    public class DatabaseBackupModel
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string DatabaseName { get; set; }
        public string Reason { get; set; }
        public string TagDiscription { get; set; }
        public bool IsDelete { get; set; } = false;
        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }
        public string Avatar { get; set; }
        public string UserName { get; set; }
    }

    public class InsertDatabaseBackup: DatabaseBackupModel { }
    public class UpdateDatabaseBackup : DatabaseBackupModel { }
    public class DeleteDatabaseBackup : DatabaseBackupModel { }
    public class ViewDatabaseBackup : DatabaseBackupModel { }
    public class PaginationViewDatabaseBackup
    {
        public int Count { get; set; }
        public List<ViewDatabaseBackup> ViewDatabaseBackup { get; set; }
    }
}
