using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CommonStockManagementDatabase.Context;
using CommonStockManagementDatabase.Model;
using CommonStockManagementDatabase.Model;

namespace CommonStockManagementServices.Services
{



    public class UserRole(AppDbContext context, RoleManager<IdentityRole> roleManager)
    {
        readonly RoleManager<IdentityRole> roleManager = roleManager;
        private readonly AppDbContext _context = context;

        public async Task<PaginationListDatarole> GetAll(int page, int pagecount, string searchTerm)
        {
            List<UserRoleModel> staff_Categories = new();
            PaginationListDatarole paginationListDatas = new();

            var dt = await (from a in _context.Roles
                            orderby a.Id descending
                            where (string.IsNullOrEmpty(searchTerm) || a.Name.Contains(searchTerm) || a.NormalizedName.Contains(searchTerm))
                            select new UserRoleModel()
                            {
                                Id = a.Id,
                                Name = a.Name,
                                NormalizedName = a.NormalizedName

                            }).ToListAsync();

            foreach (var d in dt)
            {
                if (d.Name == "Administrator")
                {
                    d.Id = d.Id;
                    d.Name = d.Name;
                    d.NormalizedName = d.NormalizedName;
                    d.Status = "User Management / Customer Management / Customer Category Management / Request Management / Tax Management";
                }
                if (d.Name == "User")
                {
                    d.Id = d.Id;
                    d.Name = d.Name;
                    d.NormalizedName = d.NormalizedName;
                    d.Status = "Customer Management / Customer Category Management / Request Management / Tax Management";
                }
                if (d.Name == "Accountant")
                {
                    d.Id = d.Id;
                    d.Name = d.Name;
                    d.NormalizedName = d.NormalizedName;
                    d.Status = "Customer Management / Customer Category Management / Request Management / Tax Management";
                }
                staff_Categories.Add(d);
            }

            paginationListDatas.Count = staff_Categories.Count;
            paginationListDatas.Account = dt.Skip((page - 1) * pagecount).Take(pagecount).ToList();

            return paginationListDatas;
        }


        public async Task<Message<string>> RegisterAsync(IdentityRole model)
        {
            try
            {
                var result = await roleManager.CreateAsync(model);
                return new Message<string>() { Status = "S", Text = $" User Role has been added successfully.", Result = model.Id.ToString() };

            }
            catch (Exception ex)
            {
                return new Message<string>() { Status = "E", Text = ex.Message };
            }
        }



        public async Task<Message<string>> DeleteAsync(string id)
        {
            try
            {
                var role = await roleManager.FindByIdAsync(id);
                var result = await roleManager.DeleteAsync(role);

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"User Role has been deleted successfully",
                    Result = role.Id.ToString()

                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Delete error {ex.Message}" };
            }
        }

        public async Task<Message<string>> UpdateAsync(IdentityRole model)
        {
            try
            {
                var role = await roleManager.FindByIdAsync(model.Id);
                role.Name = model.Name;
                var result = await roleManager.UpdateAsync(role);

                return new Message<string>()
                {
                    Status = "S",
                    Text = $"User Role has been updated successfully",
                    Result = model.Id.ToString()

                };

            }
            catch (Exception ex)
            {
                return new Message<string>()
                { Text = $"Update error {ex.Message}" };
            }


        }


        public async Task<IdentityRole> GetDetailsById(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            var model = new IdentityRole()
            {
                Id = role.Id,
                Name = role.Name,

            };
            return model;
        }
    }
}
