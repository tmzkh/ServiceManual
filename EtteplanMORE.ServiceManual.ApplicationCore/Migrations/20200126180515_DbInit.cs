using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ServiceManual");

            migrationBuilder.CreateTable(
                name: "FactoryDevices",
                schema: "ServiceManual",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactoryDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceTasks",
                schema: "ServiceManual",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FactoryDeviceId = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Criticality = table.Column<int>(nullable: false),
                    Done = table.Column<sbyte>(type: "TINYINT(4)", nullable: false, defaultValue: (sbyte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceTasks_FactoryDevices_FactoryDeviceId",
                        column: x => x.FactoryDeviceId,
                        principalSchema: "ServiceManual",
                        principalTable: "FactoryDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceTasks_FactoryDeviceId",
                schema: "ServiceManual",
                table: "MaintenanceTasks",
                column: "FactoryDeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceTasks",
                schema: "ServiceManual");

            migrationBuilder.DropTable(
                name: "FactoryDevices",
                schema: "ServiceManual");
        }
    }
}
