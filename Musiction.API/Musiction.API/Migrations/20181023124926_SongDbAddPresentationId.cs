using Microsoft.EntityFrameworkCore.Migrations;

namespace Musiction.API.Migrations
{
    public partial class SongDbAddPresentationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "YouTubeUrl",
                table: "Songs",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentationId",
                table: "Songs",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PresentationId",
                table: "Songs");

            migrationBuilder.AlterColumn<string>(
                name: "YouTubeUrl",
                table: "Songs",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);
        }
    }
}
