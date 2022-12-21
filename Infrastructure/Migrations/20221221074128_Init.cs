using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acounts",
                columns: table => new
                {
                    AcountId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Authenticated = table.Column<bool>(type: "boolean", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acounts", x => x.AcountId);
                });

            migrationBuilder.CreateTable(
                name: "Transacitions",
                columns: table => new
                {
                    TransacitionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Recipient = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    Sender = table.Column<string>(type: "text", nullable: false),
                    PaymentType = table.Column<int>(type: "integer", nullable: false),
                    AcountId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacitions", x => x.TransacitionId);
                    table.ForeignKey(
                        name: "FK_Transacitions_Acounts_AcountId",
                        column: x => x.AcountId,
                        principalTable: "Acounts",
                        principalColumn: "AcountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transacitions_AcountId",
                table: "Transacitions",
                column: "AcountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacitions");

            migrationBuilder.DropTable(
                name: "Acounts");
        }
    }
}
