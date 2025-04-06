using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonStockManagementDatabase.Migrations
{
    /// <inheritdoc />
    public partial class InitialViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SQLQueries.SQlViewsClass  sQl= new SQLQueries.SQlViewsClass();  
            sQl.DeleteViewQuery(migrationBuilder);
            sQl.InsertViewQuery(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
