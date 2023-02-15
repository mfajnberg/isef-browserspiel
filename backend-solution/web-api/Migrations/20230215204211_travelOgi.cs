using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_api.Migrations
{
    public partial class travelOgi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_OGIs_ActionId",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_ActionId",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "Parties");
            */

            migrationBuilder.AddColumn<int>(
                name: "AxialQ",
                table: "OGIs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AxialR",
                table: "OGIs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "OGIs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TravelTime",
                table: "HexTiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OGIs_PartyId",
                table: "OGIs",
                column: "PartyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OGIs_Parties_PartyId",
                table: "OGIs",
                column: "PartyId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OGIs_Parties_PartyId",
                table: "OGIs");

            migrationBuilder.DropIndex(
                name: "IX_OGIs_PartyId",
                table: "OGIs");

            migrationBuilder.DropColumn(
                name: "AxialQ",
                table: "OGIs");

            migrationBuilder.DropColumn(
                name: "AxialR",
                table: "OGIs");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "OGIs");

            migrationBuilder.DropColumn(
                name: "TravelTime",
                table: "HexTiles");

            /*
            migrationBuilder.AddColumn<int>(
                name: "ActionId",
                table: "Parties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parties_ActionId",
                table: "Parties",
                column: "ActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_OGIs_ActionId",
                table: "Parties",
                column: "ActionId",
                principalTable: "OGIs",
                principalColumn: "Id");

            */
        }
    }
}
