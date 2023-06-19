using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class ChangeTblGroupMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLeader",
                table: "GroupMembers");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "GroupSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "InviteMessage",
                table: "GroupMembers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestMessage",
                table: "GroupMembers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupSubjects_SubjectId",
                table: "GroupSubjects",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSubjects_Subjects_SubjectId",
                table: "GroupSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupSubjects_Subjects_SubjectId",
                table: "GroupSubjects");

            migrationBuilder.DropIndex(
                name: "IX_GroupSubjects_SubjectId",
                table: "GroupSubjects");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "GroupSubjects");

            migrationBuilder.DropColumn(
                name: "InviteMessage",
                table: "GroupMembers");

            migrationBuilder.DropColumn(
                name: "RequestMessage",
                table: "GroupMembers");

            migrationBuilder.AddColumn<bool>(
                name: "IsLeader",
                table: "GroupMembers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
