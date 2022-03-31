using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace deliveryFITPackages.API.Infra.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    createdBy = table.Column<string>(type: "TEXT", nullable: false),
                    userId = table.Column<Guid>(type: "TEXT", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    recipient = table.Column<string>(type: "TEXT", nullable: false),
                    zipcode = table.Column<string>(type: "TEXT", nullable: false),
                    houseNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    postedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    withdrawnAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    deliveredAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    packageId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.id);
                    table.ForeignKey(
                        name: "FK_Details_Packages_packageId",
                        column: x => x.packageId,
                        principalTable: "Packages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Details_packageId",
                table: "Details",
                column: "packageId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "Packages");
        }
    }
}
