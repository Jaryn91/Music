using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Musiction.API.Migrations
{
    public partial class RemoveSongsFromPresentation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Presentations_PresentationId1",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_PresentationId1",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "PresentationId1",
                table: "Songs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PresentationId1",
                table: "Songs",
                nullable: true);

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
    }
}
