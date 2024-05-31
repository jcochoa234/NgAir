using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NgAir.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class TablesXeros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "XeroContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XeroContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "XeroContainerHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContainerNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateArrive = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XeroContainerHeaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "XeroInvoiceHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AmountDue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountCredited = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    XeroContactId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XeroInvoiceHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XeroInvoiceHeaders_XeroContacts_XeroContactId",
                        column: x => x.XeroContactId,
                        principalTable: "XeroContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "XeroContainerLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContainerNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    XeroContainerHeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XeroContainerLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XeroContainerLines_XeroContainerHeaders_XeroContainerHeaderId",
                        column: x => x.XeroContainerHeaderId,
                        principalTable: "XeroContainerHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "XeroInvoiceLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LineItemID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LineAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    UnitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    XeroInvoiceHeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XeroInvoiceLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XeroInvoiceLines_XeroInvoiceHeaders_XeroInvoiceHeaderId",
                        column: x => x.XeroInvoiceHeaderId,
                        principalTable: "XeroInvoiceHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "XeroContainerLineSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContainerNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    XeroContainerLineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XeroContainerLineSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XeroContainerLineSeries_XeroContainerLines_XeroContainerLineId",
                        column: x => x.XeroContainerLineId,
                        principalTable: "XeroContainerLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "XeroInvoiceLineSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    XeroInvoiceLineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XeroInvoiceLineSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XeroInvoiceLineSeries_XeroInvoiceLines_XeroInvoiceLineId",
                        column: x => x.XeroInvoiceLineId,
                        principalTable: "XeroInvoiceLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_XeroContainerLines_XeroContainerHeaderId",
                table: "XeroContainerLines",
                column: "XeroContainerHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_XeroContainerLineSeries_XeroContainerLineId",
                table: "XeroContainerLineSeries",
                column: "XeroContainerLineId");

            migrationBuilder.CreateIndex(
                name: "IX_XeroInvoiceHeaders_XeroContactId",
                table: "XeroInvoiceHeaders",
                column: "XeroContactId");

            migrationBuilder.CreateIndex(
                name: "IX_XeroInvoiceLines_XeroInvoiceHeaderId",
                table: "XeroInvoiceLines",
                column: "XeroInvoiceHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_XeroInvoiceLineSeries_XeroInvoiceLineId",
                table: "XeroInvoiceLineSeries",
                column: "XeroInvoiceLineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "XeroContainerLineSeries");

            migrationBuilder.DropTable(
                name: "XeroInvoiceLineSeries");

            migrationBuilder.DropTable(
                name: "XeroContainerLines");

            migrationBuilder.DropTable(
                name: "XeroInvoiceLines");

            migrationBuilder.DropTable(
                name: "XeroContainerHeaders");

            migrationBuilder.DropTable(
                name: "XeroInvoiceHeaders");

            migrationBuilder.DropTable(
                name: "XeroContacts");
        }
    }
}
