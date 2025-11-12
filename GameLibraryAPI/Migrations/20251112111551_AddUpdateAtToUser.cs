using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdateAtToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Добавляем UpdateAt, nullable
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Убираем колонку при откате
            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Users");
        }
    }
}
