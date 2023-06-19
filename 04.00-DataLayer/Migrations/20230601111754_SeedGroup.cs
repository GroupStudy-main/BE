using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class SeedGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "ClassId", "Name" },
                values: new object[] { 1, 7, "Nhóm 1 của học sinh 1" });

            migrationBuilder.InsertData(
                table: "GroupMembers",
                columns: new[] { "Id", "AccountId", "GroupId", "InviteMessage", "RequestMessage", "State" },
                values: new object[,]
                {
                    { 1, 1, 1, null, null, 0 },
                    { 2, 2, 1, "Nhóm của mình rất hay. Bạn vô nha", null, 1 },
                    { 3, 3, 1, "Nhóm của mình rất hay. Bạn vô nha", null, 2 },
                    { 4, 4, 1, null, "Nhóm của bạn rất hay. Bạn cho mình vô nha", 3 },
                    { 5, 5, 1, null, "Nhóm của bạn rất hay. Bạn cho mình vô nha", 4 }
                });

            migrationBuilder.InsertData(
                table: "GroupSubjects",
                columns: new[] { "Id", "GroupId", "SubjectId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 4 },
                    { 3, 1, 8 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 2);

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
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
