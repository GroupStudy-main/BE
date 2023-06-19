using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class RemoveMeetingRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_MeetingRooms_MeetingRoomId",
                table: "Meetings");

            migrationBuilder.DropTable(
                name: "MeetingRooms");

            migrationBuilder.RenameColumn(
                name: "MeetingRoomId",
                table: "Meetings",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Meetings_MeetingRoomId",
                table: "Meetings",
                newName: "IX_Meetings_GroupId");

            migrationBuilder.AddColumn<int>(
                name: "CountMember",
                table: "Meetings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Groups_GroupId",
                table: "Meetings",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Groups_GroupId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CountMember",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Meetings");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Meetings",
                newName: "MeetingRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Meetings_GroupId",
                table: "Meetings",
                newName: "IX_Meetings_MeetingRoomId");

            migrationBuilder.CreateTable(
                name: "MeetingRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    CountMember = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingRooms_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRooms_GroupId",
                table: "MeetingRooms",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_MeetingRooms_MeetingRoomId",
                table: "Meetings",
                column: "MeetingRoomId",
                principalTable: "MeetingRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
