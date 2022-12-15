using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_api.Migrations
{
    public partial class minor_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.CreateTable(
                name: "Party",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Party", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PossessorId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Intellect = table.Column<int>(type: "int", nullable: false),
                    Discipline = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<int>(type: "int", nullable: false),
                    Agility = table.Column<int>(type: "int", nullable: false),
                    Lucidity = table.Column<int>(type: "int", nullable: false),
                    Charisma = table.Column<int>(type: "int", nullable: false),
                    Alignment = table.Column<int>(type: "int", nullable: false),
                    Temperament = table.Column<int>(type: "int", nullable: false),
                    Morale = table.Column<int>(type: "int", nullable: false),
                    FellowshipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avatars_Party_FellowshipId",
                        column: x => x.FellowshipId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Avatars_Users_PossessorId",
                        column: x => x.PossessorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ItemBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatureBaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemBase_Avatars_CreatureBaseId",
                        column: x => x.CreatureBaseId,
                        principalTable: "Avatars",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_FellowshipId",
                table: "Avatars",
                column: "FellowshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_PossessorId",
                table: "Avatars",
                column: "PossessorId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemBase_CreatureBaseId",
                table: "ItemBase",
                column: "CreatureBaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemBase");

            migrationBuilder.DropTable(
                name: "Avatars");

            migrationBuilder.DropTable(
                name: "Party");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");
        }
    }
}
