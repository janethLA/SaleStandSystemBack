using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySalesStandSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    username = table.Column<string>(type: "varchar(50)", nullable: false),
                    password = table.Column<string>(type: "varchar(50)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    registrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SalesStand",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    salesStandName = table.Column<string>(type: "varchar(100)", nullable: false),
                    address = table.Column<string>(type: "varchar(100)", nullable: false),
                    longitude = table.Column<string>(type: "varchar(50)", nullable: false),
                    latitude = table.Column<string>(type: "varchar(50)", nullable: false),
                    description = table.Column<string>(type: "varchar(200)", nullable: false),
                    image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesStand", x => x.id);
                    table.ForeignKey(
                        name: "FK_SalesStand_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "varchar(50)", nullable: false),
                    description = table.Column<string>(type: "varchar(100)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    measurement = table.Column<string>(type: "varchar(50)", nullable: false),
                    quantity = table.Column<double>(type: "float", nullable: false),
                    image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SalesStandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                    table.ForeignKey(
                        name: "FK_Product_SalesStand_SalesStandId",
                        column: x => x.SalesStandId,
                        principalTable: "SalesStand",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_SalesStandId",
                table: "Product",
                column: "SalesStandId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesStand_UserId",
                table: "SalesStand",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "SalesStand");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
