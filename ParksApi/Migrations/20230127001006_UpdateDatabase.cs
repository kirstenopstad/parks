using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parks.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parks_ParkType_ParkTypeId",
                table: "Parks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkType",
                table: "ParkType");

            migrationBuilder.RenameTable(
                name: "ParkType",
                newName: "ParkTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkTypes",
                table: "ParkTypes",
                column: "ParkTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parks_ParkTypes_ParkTypeId",
                table: "Parks",
                column: "ParkTypeId",
                principalTable: "ParkTypes",
                principalColumn: "ParkTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parks_ParkTypes_ParkTypeId",
                table: "Parks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkTypes",
                table: "ParkTypes");

            migrationBuilder.RenameTable(
                name: "ParkTypes",
                newName: "ParkType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkType",
                table: "ParkType",
                column: "ParkTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parks_ParkType_ParkTypeId",
                table: "Parks",
                column: "ParkTypeId",
                principalTable: "ParkType",
                principalColumn: "ParkTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
