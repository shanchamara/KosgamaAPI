using CommonStockManagementDatabase.SQLQueries;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonStockManagementDatabase.Migrations
{
    /// <inheritdoc />
    public partial class View1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SQlViewsClass sQlViewsClass = new();
            sQlViewsClass.DeleteViewQuery(migrationBuilder);
            sQlViewsClass.InsertViewQuery(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
