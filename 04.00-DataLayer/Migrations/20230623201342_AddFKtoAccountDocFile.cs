using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class AddFKtoAccountDocFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DocumentFiles");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "DocumentFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFiles_AccountId",
                table: "DocumentFiles",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentFiles_Accounts_AccountId",
                table: "DocumentFiles",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentFiles_Accounts_AccountId",
                table: "DocumentFiles");

            migrationBuilder.DropIndex(
                name: "IX_DocumentFiles_AccountId",
                table: "DocumentFiles");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "DocumentFiles");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DocumentFiles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
