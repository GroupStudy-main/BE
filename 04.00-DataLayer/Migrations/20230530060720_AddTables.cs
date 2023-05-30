using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connection_Accounts_AccountId",
                table: "Connection");

            migrationBuilder.DropForeignKey(
                name: "FK_Connection_Meeting_MeetingId",
                table: "Connection");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMember_Accounts_AccountId",
                table: "GroupMember");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMember_Group_GroupId",
                table: "GroupMember");

            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_MeetingRoom_MeetingRoomId",
                table: "Meeting");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingRoom_Group_GroupId",
                table: "MeetingRoom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetingRoom",
                table: "MeetingRoom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meeting",
                table: "Meeting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupMember",
                table: "GroupMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Group",
                table: "Group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Connection",
                table: "Connection");

            migrationBuilder.RenameTable(
                name: "MeetingRoom",
                newName: "MeetingRooms");

            migrationBuilder.RenameTable(
                name: "Meeting",
                newName: "Meetings");

            migrationBuilder.RenameTable(
                name: "GroupMember",
                newName: "GroupMembers");

            migrationBuilder.RenameTable(
                name: "Group",
                newName: "Groups");

            migrationBuilder.RenameTable(
                name: "Connection",
                newName: "Connections");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingRoom_GroupId",
                table: "MeetingRooms",
                newName: "IX_MeetingRooms_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Meeting_MeetingRoomId",
                table: "Meetings",
                newName: "IX_Meetings_MeetingRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMember_GroupId",
                table: "GroupMembers",
                newName: "IX_GroupMembers_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMember_AccountId",
                table: "GroupMembers",
                newName: "IX_GroupMembers_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Connection_MeetingId",
                table: "Connections",
                newName: "IX_Connections_MeetingId");

            migrationBuilder.RenameIndex(
                name: "IX_Connection_AccountId",
                table: "Connections",
                newName: "IX_Connections_AccountId");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CountMember",
                table: "MeetingRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsLeader",
                table: "GroupMembers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Connections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingRooms",
                table: "MeetingRooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meetings",
                table: "Meetings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupMembers",
                table: "GroupMembers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Connections",
                table: "Connections",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "FullName",
                value: "Nguyen Van A");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "FullName" },
                values: new object[] { "student2@gmail.com", "Dao Thi B" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "FullName", "Username" },
                values: new object[] { "student3@gmail.com", "Tran Van C", "student3" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "FullName", "RoleId", "Username" },
                values: new object[] { "student4@gmail.com", "Li Thi D", 2, "student4" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "FullName", "Password", "Phone", "RoleId", "Username" },
                values: new object[,]
                {
                    { 5, "student5@gmail.com", "Tran Van E", "123456789", "0123456789", 2, "student5" },
                    { 6, "student6@gmail.com", "Li Chinh F", "123456789", "0123456789", 2, "student6" },
                    { 7, "student7@gmail.com", "Ngo Van G", "123456789", "0123456789", 2, "student7" },
                    { 8, "student3@gmail.com", "Tran Van H", "123456789", "0123456789", 2, "student8" },
                    { 9, "student10@gmail.com", "Tran Van I", "123456789", "0123456789", 2, "student9" },
                    { 10, "student10@gmail.com", "Tran Van J", "123456789", "0123456789", 2, "student10" },
                    { 11, "trankhaiminhkhoi@gmail.com", "Tran Khoi", "123456789", "0123456789", 1, "parent1" }
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Accounts_AccountId",
                table: "GroupMembers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Groups_GroupId",
                table: "GroupMembers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingRooms_Groups_GroupId",
                table: "MeetingRooms",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_MeetingRooms_MeetingRoomId",
                table: "Meetings",
                column: "MeetingRoomId",
                principalTable: "MeetingRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Accounts_AccountId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Meetings_MeetingId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Accounts_AccountId",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Groups_GroupId",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingRooms_Groups_GroupId",
                table: "MeetingRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_MeetingRooms_MeetingRoomId",
                table: "Meetings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meetings",
                table: "Meetings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetingRooms",
                table: "MeetingRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupMembers",
                table: "GroupMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Connections",
                table: "Connections");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CountMember",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "IsLeader",
                table: "GroupMembers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Connections");

            migrationBuilder.RenameTable(
                name: "Meetings",
                newName: "Meeting");

            migrationBuilder.RenameTable(
                name: "MeetingRooms",
                newName: "MeetingRoom");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "Group");

            migrationBuilder.RenameTable(
                name: "GroupMembers",
                newName: "GroupMember");

            migrationBuilder.RenameTable(
                name: "Connections",
                newName: "Connection");

            migrationBuilder.RenameIndex(
                name: "IX_Meetings_MeetingRoomId",
                table: "Meeting",
                newName: "IX_Meeting_MeetingRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingRooms_GroupId",
                table: "MeetingRoom",
                newName: "IX_MeetingRoom_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMembers_GroupId",
                table: "GroupMember",
                newName: "IX_GroupMember_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMembers_AccountId",
                table: "GroupMember",
                newName: "IX_GroupMember_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_MeetingId",
                table: "Connection",
                newName: "IX_Connection_MeetingId");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_AccountId",
                table: "Connection",
                newName: "IX_Connection_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meeting",
                table: "Meeting",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingRoom",
                table: "MeetingRoom",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Group",
                table: "Group",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupMember",
                table: "GroupMember",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Connection",
                table: "Connection",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Email",
                value: "@gmail.com");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Username" },
                values: new object[] { "@gmail.com", "student4" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "RoleId", "Username" },
                values: new object[] { "trankhaiminhkhoi@gmail.com", 1, "parent1" });

            migrationBuilder.AddForeignKey(
                name: "FK_Connection_Accounts_AccountId",
                table: "Connection",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Connection_Meeting_MeetingId",
                table: "Connection",
                column: "MeetingId",
                principalTable: "Meeting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMember_Accounts_AccountId",
                table: "GroupMember",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMember_Group_GroupId",
                table: "GroupMember",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_MeetingRoom_MeetingRoomId",
                table: "Meeting",
                column: "MeetingRoomId",
                principalTable: "MeetingRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingRoom_Group_GroupId",
                table: "MeetingRoom",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
