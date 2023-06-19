using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class GroupMemberAddUniqeKeyPair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GroupMembers_AccountId",
                table: "GroupMembers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Meetings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_AccountId_GroupId",
                table: "GroupMembers",
                columns: new[] { "AccountId", "GroupId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GroupMembers_AccountId_GroupId",
                table: "GroupMembers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_AccountId",
                table: "GroupMembers",
                column: "AccountId");
        }
    }
}
