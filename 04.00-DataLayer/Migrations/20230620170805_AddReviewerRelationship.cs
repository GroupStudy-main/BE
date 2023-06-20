using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class AddReviewerRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewerId",
                table: "ReviewDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewDetails_ReviewerId",
                table: "ReviewDetails",
                column: "ReviewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewDetails_Accounts_ReviewerId",
                table: "ReviewDetails",
                column: "ReviewerId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewDetails_Accounts_ReviewerId",
                table: "ReviewDetails");

            migrationBuilder.DropIndex(
                name: "IX_ReviewDetails_ReviewerId",
                table: "ReviewDetails");

            migrationBuilder.DropColumn(
                name: "ReviewerId",
                table: "ReviewDetails");
        }
    }
}
