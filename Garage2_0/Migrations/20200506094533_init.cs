using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage2_0.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkedVehicle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VType = table.Column<int>(nullable: false),
                    Wheels = table.Column<int>(nullable: false),
                    RegistrationNumber = table.Column<string>(maxLength: 6, nullable: false),
                    Manufacturer = table.Column<string>(maxLength: 15, nullable: false),
                    Arrival = table.Column<DateTime>(nullable: false),
                    Color = table.Column<string>(maxLength: 15, nullable: false),
                    VehicleModel = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkedVehicle", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkedVehicle");
        }
    }
}
