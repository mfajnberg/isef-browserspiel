using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_api.Migrations
{
    public partial class added_avatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemBase_Avatars_CreatureBaseId",
                table: "ItemBase");

            migrationBuilder.RenameColumn(
                name: "CreatureBaseId",
                table: "ItemBase",
                newName: "AvatarId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemBase_CreatureBaseId",
                table: "ItemBase",
                newName: "IX_ItemBase_AvatarId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Avatars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_UserId",
                table: "Avatars",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avatars_Users_UserId",
                table: "Avatars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemBase_Avatars_AvatarId",
                table: "ItemBase",
                column: "AvatarId",
                principalTable: "Avatars",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatars_Users_UserId",
                table: "Avatars");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemBase_Avatars_AvatarId",
                table: "ItemBase");

            migrationBuilder.DropIndex(
                name: "IX_Avatars_UserId",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Avatars");

            migrationBuilder.RenameColumn(
                name: "AvatarId",
                table: "ItemBase",
                newName: "CreatureBaseId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemBase_AvatarId",
                table: "ItemBase",
                newName: "IX_ItemBase_CreatureBaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemBase_Avatars_CreatureBaseId",
                table: "ItemBase",
                column: "CreatureBaseId",
                principalTable: "Avatars",
                principalColumn: "Id");
        }
    }
}
