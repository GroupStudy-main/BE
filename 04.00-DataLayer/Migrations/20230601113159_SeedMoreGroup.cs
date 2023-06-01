using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class SeedMoreGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "ClassId", "Name" },
                values: new object[] { 2, 7, "Nhóm 2 của học sinh 1" });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "ClassId", "Name" },
                values: new object[] { 3, 8, "Nhóm 1 của học sinh 2" });

            migrationBuilder.InsertData(
                table: "GroupMembers",
                columns: new[] { "Id", "AccountId", "GroupId", "InviteMessage", "RequestMessage", "State" },
                values: new object[,]
                {
                    { 6, 1, 2, null, null, 0 },
                    { 7, 2, 2, "Nhóm của mình rất hay. Bạn vô nha", null, 1 },
                    { 8, 3, 2, "Nhóm của mình rất hay. Bạn vô nha", null, 2 },
                    { 9, 4, 2, null, "Nhóm của bạn rất hay. Bạn cho mình vô nha", 3 },
                    { 10, 1, 3, null, null, 0 },
                    { 11, 2, 3, "Nhóm của mình rất hay. Bạn vô nha", null, 1 },
                    { 12, 3, 3, "Nhóm của mình rất hay. Bạn vô nha", null, 2 },
                    { 13, 4, 3, null, "Nhóm của bạn rất hay. Bạn cho mình vô nha", 3 }
                });

            migrationBuilder.InsertData(
                table: "GroupSubjects",
                columns: new[] { "Id", "GroupId", "SubjectId" },
                values: new object[,]
                {
                    { 4, 2, 1 },
                    { 5, 2, 2 },
                    { 6, 2, 3 },
                    { 7, 3, 5 },
                    { 8, 3, 6 },
                    { 9, 3, 9 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 7);

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
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
