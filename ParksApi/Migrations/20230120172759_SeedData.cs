using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParksApi.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParksApi");

            migrationBuilder.InsertData(
                table: "ParkType",
                columns: new[] { "ParkTypeId", "Type" },
                values: new object[] { 1, "National" });

            migrationBuilder.InsertData(
                table: "ParkType",
                columns: new[] { "ParkTypeId", "Type" },
                values: new object[] { 2, "State" });

            migrationBuilder.InsertData(
                table: "Parks",
                columns: new[] { "ParkId", "City", "Name", "ParkTypeId", "State" },
                values: new object[,]
                {
                    { 1, "Grand Canyon National Park", "Grand Canyon", 1, "AZ" },
                    { 2, "Yosemite National Park", "Yosemite", 1, "CA" },
                    { 3, "Acadia National Park", "Acadia", 1, "ME" },
                    { 4, "Big Sur", "Pfieffer Big Sur", 2, "CA" },
                    { 5, "Big Sur", "Julia Pfieffer Burns", 2, "CA" },
                    { 6, "Big Sur", "Andrew Molera", 2, "CA" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Parks_ParkType_ParkTypeId",
                table: "Parks",
                column: "ParkTypeId",
                principalTable: "ParkType",
                principalColumn: "ParkTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parks_ParkType_ParkTypeId",
                table: "Parks");

            migrationBuilder.DeleteData(
                table: "Parks",
                keyColumn: "ParkId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Parks",
                keyColumn: "ParkId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Parks",
                keyColumn: "ParkId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Parks",
                keyColumn: "ParkId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Parks",
                keyColumn: "ParkId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Parks",
                keyColumn: "ParkId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ParkType",
                keyColumn: "ParkTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkType",
                keyColumn: "ParkTypeId",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "Park",
                columns: table => new
                {
                    ParkTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Park_ParkType_ParkTypeId",
                        column: x => x.ParkTypeId,
                        principalTable: "ParkType",
                        principalColumn: "ParkTypeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
