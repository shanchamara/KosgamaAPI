using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonStockManagementDatabase.Model
{
    public class ClientModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public string Email { get; set; }


        public string Nic { get; set; }
        public string Mobile { get; set; }
        public string Tel { get; set; }
        public string Type { get; set; }
        public bool Isdelete { get; set; } = false;
        public string ImageURl { get; set; }

        public string Dr { get; set; }

        public string Cr { get; set; }
        public string Edit_By { get; set; }
        public string Delete_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public DateTime? Delete_Date { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public IFormFile Image { get; set; }

    }

    public class InsertClient : ClientModel { }
    public class UpdateClient : ClientModel { }
    public class DeleteClient : ClientModel { }
    public class ViewClient : ClientModel { }
    public class PaginationViewClient
    {
        public int Count { get; set; }
        public List<ViewClient> ViewClients { get; set; }
        public List<VWListClient> IQueryData { get; set; }
    }

    public class VWListClient : ClientModel
    {
        [NotMapped]
        public new IFormFile Image { get; set; }



    }

}
