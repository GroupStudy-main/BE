using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class UpdateInviteAndRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JoinInvite_Accounts_AccountId",
                table: "JoinInvite");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinInvite_Groups_GroupId",
                table: "JoinInvite");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinRequest_Accounts_AccountId",
                table: "JoinRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinRequest_Groups_GroupId",
                table: "JoinRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoinRequest",
                table: "JoinRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoinInvite",
                table: "JoinInvite");

            migrationBuilder.RenameTable(
                name: "JoinRequest",
                newName: "Requests");

            migrationBuilder.RenameTable(
                name: "JoinInvite",
                newName: "Invites");

            migrationBuilder.RenameIndex(
                name: "IX_JoinRequest_GroupId",
                table: "Requests",
                newName: "IX_Requests_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_JoinRequest_AccountId",
                table: "Requests",
                newName: "IX_Requests_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_JoinInvite_GroupId",
                table: "Invites",
                newName: "IX_Invites_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_JoinInvite_AccountId",
                table: "Invites",
                newName: "IX_Invites_AccountId");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Invites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invites",
                table: "Invites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Accounts_AccountId",
                table: "Invites",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Groups_GroupId",
                table: "Invites",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Accounts_AccountId",
                table: "Requests",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Groups_GroupId",
                table: "Requests",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Accounts_AccountId",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Groups_GroupId",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Accounts_AccountId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Groups_GroupId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invites",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Invites");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "JoinRequest");

            migrationBuilder.RenameTable(
                name: "Invites",
                newName: "JoinInvite");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_GroupId",
                table: "JoinRequest",
                newName: "IX_JoinRequest_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_AccountId",
                table: "JoinRequest",
                newName: "IX_JoinRequest_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Invites_GroupId",
                table: "JoinInvite",
                newName: "IX_JoinInvite_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Invites_AccountId",
                table: "JoinInvite",
                newName: "IX_JoinInvite_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoinRequest",
                table: "JoinRequest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoinInvite",
                table: "JoinInvite",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JoinInvite_Accounts_AccountId",
                table: "JoinInvite",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinInvite_Groups_GroupId",
                table: "JoinInvite",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinRequest_Accounts_AccountId",
                table: "JoinRequest",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinRequest_Groups_GroupId",
                table: "JoinRequest",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
