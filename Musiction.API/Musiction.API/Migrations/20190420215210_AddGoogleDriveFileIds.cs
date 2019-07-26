using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Musiction.API.Migrations
{
    public partial class AddGoogleDriveFileIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FinalFileName",
                table: "Presentations",
                newName: "GoogleDrivePptxFileId");

            migrationBuilder.AddColumn<string>(
                name: "GoogleDriveZipFileId",
                table: "Presentations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleDriveZipFileId",
                table: "Presentations");

            migrationBuilder.RenameColumn(
                name: "GoogleDrivePptxFileId",
                table: "Presentations",
                newName: "FinalFileName");
        }
    }
}
