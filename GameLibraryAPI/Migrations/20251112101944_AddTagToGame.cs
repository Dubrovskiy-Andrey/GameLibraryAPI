using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTagToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Games");
        }
    }
}
