using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_api.Migrations
{
    public partial class added_chest_amount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ElectrumAmount",
                table: "SiteBase",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Electrum",
                table: "Parties",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElectrumAmount",
                table: "SiteBase");

            migrationBuilder.DropColumn(
                name: "Electrum",
                table: "Parties");
        }
    }
}
