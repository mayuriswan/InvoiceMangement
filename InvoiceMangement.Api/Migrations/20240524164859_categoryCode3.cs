using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceMangement.Api.Migrations
{
    /// <inheritdoc />
    public partial class categoryCode3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Categories_CategoryID",
                table: "Invoices");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Categories_CategoryID",
                table: "Invoices",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Categories_CategoryID",
                table: "Invoices");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Categories_CategoryID",
                table: "Invoices",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
