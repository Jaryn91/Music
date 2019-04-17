using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Musiction.API.Migrations
{
    public partial class AddPresentation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PresentationId1",
                table: "Songs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Presentations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Path = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presentations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Songs_PresentationId1",
                table: "Songs",
                column: "PresentationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Presentations_PresentationId1",
                table: "Songs",
                column: "PresentationId1",
                principalTable: "Presentations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Presentations_PresentationId1",
                table: "Songs");

            migrationBuilder.DropTable(
                name: "Presentations");

            migrationBuilder.DropIndex(
                name: "IX_Songs_PresentationId1",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "PresentationId1",
                table: "Songs");
        }
    }
}
