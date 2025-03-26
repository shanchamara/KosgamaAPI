namespace CommonStockManagementDatabase.Model
{
    public class CompanyDetailsModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string TelPhone1 { get; set; }
        public string TelPhone2 { get; set; }
        public bool Isdelete { get; set; } = false;
        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }
    }

    public class InsertCompanyDetails : CompanyDetailsModel { }
    public class UpdateCompanyDetailsts : CompanyDetailsModel { }
    public class DeleteCompanyDetails : CompanyDetailsModel { }
    public class ViewCompanyDetails : CompanyDetailsModel { }
    public class PaginationViewCompanyDetails
    {
        public int Count { get; set; }
        public List<ViewCompanyDetails> ViewCompanyDetails { get; set; }
    }
}
