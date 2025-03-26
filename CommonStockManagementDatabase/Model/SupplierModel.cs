using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Model
{
    public class SupplierModel
    {
        public int ID { get; set; }
        public string Company { get; set; }
        public string Contact { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string CreditorLedger { get; set; }
        public string AdvanceCreditorLedger { get; set; }
        public string Type { get; set; }
        public string LedgerCode { get; set; }
        public bool IsDelete { get; set; } = false;
        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }

        public string ImageURl { get; set; }
        public IFormFile Image { get; set; }
    }
    public class InsertSupplier : SupplierModel { }
    public class UpdateSupplier : SupplierModel { }
    public class DeleteSupplier : SupplierModel { }
    public class ViewSupplier : SupplierModel { }
    public class PaginationViewSupplier
    {
        public int Count { get; set; }
        public List<ViewSupplier> ViewSuppliers { get; set; }
        public List<VwListSupplier> IQueryData { get; set; }
    }

    public class VwListSupplier : SupplierModel
    {
        [NotMapped]
        public new IFormFile Image { get; set; }

    }


}
