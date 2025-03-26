using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockManagementApi.DatabaseConnection;
using CommonStockManagementDatabase.Model;

namespace CommonStockManagementDatabase.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        // Incloud the Compnay Details 

        public DbSet<TblCompanyDetails> TblCompanyDetails { get; set; }

        // Data Table Initial 

        public DbSet<TblEmailsetting> Tblemailsetting { get; set; }
        public DbSet<TblAuditTrail> TblAuditTrails { get; set; }
        public DbSet<TblSupplier> TblSuppliers { get; set; }

        // STock Mian Table
        public DbSet<TblStock_Main> TblStock_Mains { get; set; }

        public DbSet<TblItemUnit> TblItemUnits { get; set; }
        public DbSet<TblItemCategory> TblItemCategories { get; set; }
        public DbSet<TblItemModelType> TblItemModelTypes { get; set; }
        public DbSet<TblItemBrandName> TblItemBrandNames { get; set; }

        public DbSet<TblDatabaseBackupHistory> TblDatabaseBackupHistory { get; set; }


        public DbSet<TblClient> TblClients { get; set; }

        public DbSet<TblGRNHead> TblGRNHeads { get; set; }
        public DbSet<TblGRNBody> TblGRNBodies { get; set; }
        public DbSet<TblGRNBodyTemp> TblGRNBodyTemps { get; set; }

        // Good IssueNote 
        public DbSet<TblGINBody> TblGINBodies { get; set; }
        public DbSet<TblGINBodyTemp> TblGINBodyTemps { get; set; }
        public DbSet<TblGINHead> TblGINHead { get; set; }


        public DbSet<TblPOSHead> TblPOSHeads { get; set; }
        public DbSet<TblPOSBody> TblPOSBodies { get; set; }
        public DbSet<TblPOSBodyTemp> TblPOSBodyTemps { get; set; }


        public DbSet<TblPOSReturnHead> TblPOSReturnHeads { get; set; }
        public DbSet<TblPOSReturnBody> TblPOSReturnBodies { get; set; }
        public DbSet<TblPOSReturnBodyTemp> TblPOSReturnBodyTemps { get; set; }


        public DbSet<TblStockReturnNoteHead> TblStockReturnNoteHeads { get; set; }
        public DbSet<TblStockReturnNoteBody> TblStockReturnNoteBodies { get; set; }
        public DbSet<TblStockReturnNoteBodyTemp> TblStockReturnNoteBodyTemps { get; set; }


        public DbSet<TblPriceBackup> TblPriceBackups { get; set; }

        // rantal Items 

        public DbSet<TblItemRentalDetails> TblItemRentalDetails { get; set; }
        public DbSet<TblItemRentalDetailsTemp> TblItemRentalDetailsTemp { get; set; }
        public DbSet<TblItemRentalHead> TblItemRentalHeads { get; set; }


        #region SqlQueryViews

        // public DbSet<VWItemPriceBackup> VWItemPriceBackup { get; set; }
        public DbSet<VWAllActiveItemList> VWAllActiveItemList { get; set; }
        public DbSet<VWListClient> VWListClients { get; set; }
        public DbSet<VwListSupplier> VwListSuppliers { get; set; }
        public DbSet<VwListItemCategory> VwListItemCategory { get; set; }
        public DbSet<VwListItemUnit> VwListItemUnit { get; set; }
        public DbSet<VwListItemBrand> VwListItemBrand { get; set; }
        public DbSet<VwListItemModelType> VwListItemModelType { get; set; }
        public DbSet<VwListGRNHeads> VwListGRNHeads { get; set; }
        public DbSet<VwListSRNHeads> VwListSRNHeads { get; set; }
        public DbSet<VwListGINHead> VwListGINHead { get; set; }
        public DbSet<VwListPOSHeads> VwListPOSHeads { get; set; }
        public DbSet<VwBestCustomers> VwBestCustomers { get; set; }
        public DbSet<VwListPOSReturnHeads> VwListPOSReturnHeads { get; set; }
        public DbSet<VwListPOSHeads> VwActiveInvoicesHeads { get; set; }
        public DbSet<VWAllActiveItemReorderLevelList> VWAllActiveItemReorderLevelLists { get; set; }


        // For the list, View the Quantity balance for each item 
        public DbSet<VWAllActiveANDAvailableItemList> VWAllActiveANDAvailableItemList { get; set; }
        public DbSet<VWAllActiveANDAvailableItemListForSupplierreturn> VWAllActiveANDAvailableItemListForSupplierreturns { get; set; }
        public DbSet<ViewAllPOsInvoiceItem> ViewAllPOsInvoiceItems { get; set; }
        public DbSet<ViewAllPOsInvoiceItemForReport> ViewAllPOsInvoiceItemForReport { get; set; }
        public DbSet<ViewAllPurcheseAndRevenue> ViewAllPurcheseAndRevenue { get; set; }
        public DbSet<ViewAllPOsInvoiceItemForReportPage> ViewAllPOsInvoiceItemForReportPage { get; set; }
        public DbSet<VwAllPriceBackupHistory> VwAllPriceBackupHistorys { get; set; }

        #endregion


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "bf8f6d4e-86cb-483a-9b74-e9d80733077f", Name = "User", NormalizedName = "User".ToUpper(), ConcurrencyStamp = "81f756e6-77d7-4982-9864-ca2321ffc562" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "36c9d5b8-e498-4969-bad4-96a4aef6dd00", Name = "Admin", NormalizedName = "Admin".ToUpper(), ConcurrencyStamp = "b4bdef3a-30ad-44c7-9f18-5c3a4d1167e1" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "36c9d5b8-e498-4969-kuiq-96a4aef6dd00", Name = "Manager", NormalizedName = "Manager".ToUpper(), ConcurrencyStamp = "b4bdef3a-30ad-44c7-9f18-5c3a4d1167e1" });

            // Rename Entity Framework table
            builder.Entity<AppUser>().ToTable("TblUsers");
            builder.Entity<IdentityUserRole<string>>().ToTable("Tbl_User_Role");
            builder.Entity<IdentityUserLogin<string>>().ToTable("Tbl_User_Login");
            builder.Entity<IdentityUserClaim<string>>().ToTable("Tbl_User_Claims");
            builder.Entity<IdentityRole>().ToTable("Tbl_Role");
            builder.Entity<IdentityUserToken<string>>().ToTable("Tbl_User_Token");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("Tbl_Role_Claims");

            // builder.Entity<TblJobRequestDetail>().HasKey(r => new { r.Id });

            //builder.Entity<TblReview>(entity =>
            //{
            //    entity.HasKey(e => new { e.Review_id, });


            //    entity.HasOne(d => d.TblRequest)
            //         .WithMany(p => p.TblReview)
            //         .HasForeignKey(d => d.FKRequest_id)
            //         .OnDelete(DeleteBehavior.ClientSetNull);

            //});

           
            builder.Entity<VWAllActiveItemList>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VWAllActiveItemList");
            });

            builder.Entity<VWListClient>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VWListClient");
            });

            builder.Entity<VwListSupplier>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwListSupplier");
            });

            builder.Entity<VwListItemCategory>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwListItemCategory");
            });

            builder.Entity<VwListItemUnit>(entity =>
            {

                entity.HasNoKey();
                entity.ToView("VwListItemUnit");
            });


            builder.Entity<VwListItemBrand>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwListItemBrand");
            });


            builder.Entity<VwListGINHead>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwListGINHead");
            });

            builder.Entity<VWAllActiveItemReorderLevelList>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VWAllActiveItemReorderLevelList");
            });

            builder.Entity<VwListGRNHeads>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwListGRNHeads");
            });

            // For the list, View the Quantity balance for each item 

            builder.Entity<VWAllActiveANDAvailableItemList>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VWAllActiveANDAvailableItemList");
            });

            builder.Entity<VWAllActiveANDAvailableItemListForSupplierreturn>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VWAllActiveANDAvailableItemListForSupplierreturn");
            });

            builder.Entity<VwListPOSHeads>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwListPOSHeads");
            });

            builder.Entity<VwBestCustomers>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwBestCustomers");
            });


            builder.Entity<VwListPOSReturnHeads>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwListPOSReturnHeads");
            });




            builder.Entity<VwListSRNHeads>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwListSRNHeads");
            });


            builder.Entity<ViewAllPOsInvoiceItem>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewAllPOsInvoiceItem");
            });

            //dashBoard Report 
            builder.Entity<ViewAllPOsInvoiceItemForReport>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewAllPOsInvoiceItemForReport");
            });


            builder.Entity<ViewAllPurcheseAndRevenue>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewAllPurcheseAndRevenue");

            });


            builder.Entity<ViewAllPOsInvoiceItemForReportPage>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewAllPOsInvoiceItemForReportPage");
            });


            builder.Entity<VwAllPriceBackupHistory>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwAllPriceBackupHistory");
            });



        }



    }
}
