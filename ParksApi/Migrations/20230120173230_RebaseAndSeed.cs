using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parks.Migrations
{
    public partial class RebaseAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ParkType",
                columns: table => new
                {
                    ParkTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkType", x => x.ParkTypeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Parks",
                columns: table => new
                {
                    ParkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    State = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParkTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parks", x => x.ParkId);
                    table.ForeignKey(
                        name: "FK_Parks_ParkType_ParkTypeId",
                        column: x => x.ParkTypeId,
                        principalTable: "ParkType",
                        principalColumn: "ParkTypeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_Parks_ParkTypeId",
                table: "Parks",
                column: "ParkTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parks");

            migrationBuilder.DropTable(
                name: "ParkType");
        }
    }
}
