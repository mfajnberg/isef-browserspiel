using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_api.Migrations
{
    public partial class modified_avatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatars_Party_FellowshipId",
                table: "Avatars");

            migrationBuilder.DropForeignKey(
                name: "FK_Avatars_Users_PossessorId",
                table: "Avatars");

            migrationBuilder.DropForeignKey(
                name: "FK_Avatars_Users_UserId",
                table: "Avatars");

            migrationBuilder.DropTable(
                name: "ItemBase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Party",
                table: "Party");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avatars",
                table: "Avatars");

            migrationBuilder.DropIndex(
                name: "IX_Avatars_PossessorId",
                table: "Avatars");

            migrationBuilder.DropIndex(
                name: "IX_Avatars_UserId",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "PossessorId",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Avatars");

            migrationBuilder.RenameTable(
                name: "Party",
                newName: "Parties");

            migrationBuilder.RenameTable(
                name: "Avatars",
                newName: "Creatures");

            migrationBuilder.RenameIndex(
                name: "IX_Avatars_FellowshipId",
                table: "Creatures",
                newName: "IX_Creatures_FellowshipId");

            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DestinationAxialQ",
                table: "Parties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DestinationAxialR",
                table: "Parties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeaderId",
                table: "Parties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationAxialQ",
                table: "Parties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationAxialR",
                table: "Parties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Creatures",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parties",
                table: "Parties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Creatures",
                table: "Creatures",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatureId = table.Column<int>(type: "int", nullable: true),
                    PartyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AvatarId",
                table: "Users",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_DestinationAxialQ_DestinationAxialR",
                table: "Parties",
                columns: new[] { "DestinationAxialQ", "DestinationAxialR" });

            migrationBuilder.CreateIndex(
                name: "IX_Parties_LeaderId",
                table: "Parties",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_LocationAxialQ_LocationAxialR",
                table: "Parties",
                columns: new[] { "LocationAxialQ", "LocationAxialR" });

            migrationBuilder.CreateIndex(
                name: "IX_Item_CreatureId",
                table: "Item",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_PartyId",
                table: "Item",
                column: "PartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Creatures_Parties_FellowshipId",
                table: "Creatures",
                column: "FellowshipId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Creatures_LeaderId",
                table: "Parties",
                column: "LeaderId",
                principalTable: "Creatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_HexTiles_DestinationAxialQ_DestinationAxialR",
                table: "Parties",
                columns: new[] { "DestinationAxialQ", "DestinationAxialR" },
                principalTable: "HexTiles",
                principalColumns: new[] { "Q", "R" });

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_HexTiles_LocationAxialQ_LocationAxialR",
                table: "Parties",
                columns: new[] { "LocationAxialQ", "LocationAxialR" },
                principalTable: "HexTiles",
                principalColumns: new[] { "Q", "R" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Creatures_AvatarId",
                table: "Users",
                column: "AvatarId",
                principalTable: "Creatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creatures_Parties_FellowshipId",
                table: "Creatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Creatures_LeaderId",
                table: "Parties");

            migrationBuilder.DropForeignKey(
                name: "FK_Parties_HexTiles_DestinationAxialQ_DestinationAxialR",
                table: "Parties");

            migrationBuilder.DropForeignKey(
                name: "FK_Parties_HexTiles_LocationAxialQ_LocationAxialR",
                table: "Parties");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Creatures_AvatarId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Users_AvatarId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parties",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_DestinationAxialQ_DestinationAxialR",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_LeaderId",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_LocationAxialQ_LocationAxialR",
                table: "Parties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Creatures",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DestinationAxialQ",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "DestinationAxialR",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "LeaderId",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "LocationAxialQ",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "LocationAxialR",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Creatures");

            migrationBuilder.RenameTable(
                name: "Parties",
                newName: "Party");

            migrationBuilder.RenameTable(
                name: "Creatures",
                newName: "Avatars");

            migrationBuilder.RenameIndex(
                name: "IX_Creatures_FellowshipId",
                table: "Avatars",
                newName: "IX_Avatars_FellowshipId");

            migrationBuilder.AddColumn<int>(
                name: "PossessorId",
                table: "Avatars",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Avatars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Party",
                table: "Party",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avatars",
                table: "Avatars",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ItemBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AvatarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemBase_Avatars_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatars",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_PossessorId",
                table: "Avatars",
                column: "PossessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_UserId",
                table: "Avatars",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemBase_AvatarId",
                table: "ItemBase",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avatars_Party_FellowshipId",
                table: "Avatars",
                column: "FellowshipId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avatars_Users_PossessorId",
                table: "Avatars",
                column: "PossessorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Avatars_Users_UserId",
                table: "Avatars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
