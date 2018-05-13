using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Bookshop.Data.Migrations
{
    public partial class AddPublisher_Store : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    city = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    phone = table.Column<string>(nullable: false),
                    postalCode = table.Column<string>(nullable: false),
                    street = table.Column<string>(nullable: false),
                    streetNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookID = table.Column<int>(nullable: false),
                    PublisherID = table.Column<int>(nullable: false),
                    amount = table.Column<int>(nullable: false),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.id);
                    table.ForeignKey(
                        name: "FK_Stores_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stores_Publishers_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stores_BookID",
                table: "Stores",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_PublisherID",
                table: "Stores",
                column: "PublisherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
