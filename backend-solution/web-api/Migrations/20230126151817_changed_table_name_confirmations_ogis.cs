using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_api.Migrations
{
    public partial class changed_table_name_confirmations_ogis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Confirmation",
                table: "Confirmation");

            migrationBuilder.RenameTable(
                name: "Confirmation",
                newName: "Confirmations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Confirmations",
                table: "Confirmations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OGIs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ScheduledAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ScheduledFor = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OGIs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OGIs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Confirmations",
                table: "Confirmations");

            migrationBuilder.RenameTable(
                name: "Confirmations",
                newName: "Confirmation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Confirmation",
                table: "Confirmation",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Interactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ScheduledAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ScheduledFor = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
