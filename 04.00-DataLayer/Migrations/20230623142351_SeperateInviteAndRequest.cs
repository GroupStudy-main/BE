using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class SeperateInviteAndRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DropColumn(
                name: "InviteMessage",
                table: "GroupMembers");

            migrationBuilder.DropColumn(
                name: "RequestMessage",
                table: "GroupMembers");

            migrationBuilder.CreateTable(
                name: "JoinInvite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinInvite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JoinInvite_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinInvite_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JoinRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JoinRequest_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinRequest_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 5,
                column: "State",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_JoinInvite_AccountId",
                table: "JoinInvite",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinInvite_GroupId",
                table: "JoinInvite",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinRequest_AccountId",
                table: "JoinRequest",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinRequest_GroupId",
                table: "JoinRequest",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JoinInvite");

            migrationBuilder.DropTable(
                name: "JoinRequest");

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

            migrationBuilder.UpdateData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 2,
                column: "InviteMessage",
                value: "Nhóm của mình rất hay. Bạn vô nha");

            migrationBuilder.UpdateData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "RequestMessage", "State" },
                values: new object[] { "Nhóm của bạn rất hay. Bạn cho mình vô nha", 4 });

            migrationBuilder.UpdateData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 7,
                column: "InviteMessage",
                value: "Nhóm của mình rất hay. Bạn vô nha");

            migrationBuilder.UpdateData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 11,
                column: "InviteMessage",
                value: "Nhóm của mình rất hay. Bạn vô nha");

            migrationBuilder.InsertData(
                table: "GroupMembers",
                columns: new[] { "Id", "AccountId", "GroupId", "InviteMessage", "RequestMessage", "State" },
                values: new object[,]
                {
                    { 3, 3, 1, "Nhóm của mình rất hay. Bạn vô nha", null, 2 },
                    { 4, 4, 1, null, "Nhóm của bạn rất hay. Bạn cho mình vô nha", 3 },
                    { 8, 3, 2, "Nhóm của mình rất hay. Bạn vô nha", null, 2 },
                    { 9, 4, 2, null, "Nhóm của bạn rất hay. Bạn cho mình vô nha", 3 },
                    { 12, 3, 3, "Nhóm của mình rất hay. Bạn vô nha", null, 2 },
                    { 13, 4, 3, null, "Nhóm của bạn rất hay. Bạn cho mình vô nha", 3 }
                });
        }
    }
}
