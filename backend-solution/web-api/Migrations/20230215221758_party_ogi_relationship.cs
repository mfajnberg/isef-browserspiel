using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_api.Migrations
{
    public partial class party_ogi_relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_OGIs_Parties_PartyId1",
            //    table: "OGIs");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Parties_OGIs_OngoingGameplayInteractionId",
            //    table: "Parties");
                
            //migrationBuilder.DropIndex(
            //    name: "IX_Parties_OngoingGameplayInteractionId",
            //    table: "Parties");

            //migrationBuilder.DropIndex(
            //    name: "IX_OGIs_PartyId1",
            //    table: "OGIs");

            //migrationBuilder.DropColumn(
            //    name: "PartyId1",
            //    table: "OGIs");

            //migrationBuilder.RenameColumn(
            //    name: "OngoingGameplayInteractionId",
            //    table: "Parties",
            //    newName: "Action");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "Action",
            //    table: "Parties",
            //    newName: "OngoingGameplayInteractionId");

            //migrationBuilder.AddColumn<int>(
            //    name: "PartyId1",
            //    table: "OGIs",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Parties_OngoingGameplayInteractionId",
            //    table: "Parties",
            //    column: "OngoingGameplayInteractionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OGIs_PartyId1",
            //    table: "OGIs",
            //    column: "PartyId1",
            //    unique: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OGIs_Parties_PartyId1",
            //    table: "OGIs",
            //    column: "PartyId1",
            //    principalTable: "Parties",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Parties_OGIs_OngoingGameplayInteractionId",
            //    table: "Parties",
            //    column: "OngoingGameplayInteractionId",
            //    principalTable: "OGIs",
            //    principalColumn: "Id");
        }
    }
}
