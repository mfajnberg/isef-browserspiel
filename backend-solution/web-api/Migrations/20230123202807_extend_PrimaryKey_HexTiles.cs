﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_api.Migrations
{
    public partial class extend_PrimaryKey_HexTiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HexTiles",
                table: "HexTiles");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "HexTiles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "AxialQ",
                table: "HexTiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AxialR",
                table: "HexTiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HexTiles",
                table: "HexTiles",
                columns: new[] { "AxialQ", "AxialR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HexTiles",
                table: "HexTiles");

            migrationBuilder.DropColumn(
                name: "AxialQ",
                table: "HexTiles");

            migrationBuilder.DropColumn(
                name: "AxialR",
                table: "HexTiles");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "HexTiles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HexTiles",
                table: "HexTiles",
                column: "Id");
        }
    }
}