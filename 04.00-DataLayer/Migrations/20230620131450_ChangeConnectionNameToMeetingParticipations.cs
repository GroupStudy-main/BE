using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class ChangeConnectionNameToMeetingParticipations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Accounts_AccountId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Meetings_MeetingId",
                table: "Connections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Connections",
                table: "Connections");

            migrationBuilder.RenameTable(
                name: "Connections",
                newName: "MeetingParticipations");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_MeetingId",
                table: "MeetingParticipations",
                newName: "IX_MeetingParticipations_MeetingId");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_AccountId",
                table: "MeetingParticipations",
                newName: "IX_MeetingParticipations_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingParticipations",
                table: "MeetingParticipations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingParticipations_Accounts_AccountId",
                table: "MeetingParticipations",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingParticipations_Meetings_MeetingId",
                table: "MeetingParticipations",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingParticipations_Accounts_AccountId",
                table: "MeetingParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingParticipations_Meetings_MeetingId",
                table: "MeetingParticipations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetingParticipations",
                table: "MeetingParticipations");

            migrationBuilder.RenameTable(
                name: "MeetingParticipations",
                newName: "Connections");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingParticipations_MeetingId",
                table: "Connections",
                newName: "IX_Connections_MeetingId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingParticipations_AccountId",
                table: "Connections",
                newName: "IX_Connections_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Connections",
                table: "Connections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Accounts_AccountId",
                table: "Connections",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Meetings_MeetingId",
                table: "Connections",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
