namespace CommonStockManagementDatabase.Model
{
    public class Emailsetting
    {
        public virtual int Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string Host { get; set; }
        public virtual int Port { get; set; }
        public string YourDomain { get; set; }
        public virtual bool Isdeleted { get; set; } = false;
        public virtual string Edit_By { get; set; }
        public virtual string Delete_By { get; set; }
        public virtual DateTime Edit_Date { get; set; }
        public virtual DateTime? Delete_Date { get; set; }
    }

    public class Insertemailsetting : Emailsetting
    {

    }

    public class Updateemailsetting : Emailsetting
    {

    }
}
