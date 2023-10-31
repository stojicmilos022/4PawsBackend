using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4PawsBackend.Migrations
{
    /// <inheritdoc />
    public partial class FileNameAddedToSalon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "SalonSlike",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "SalonSlike");
        }
    }
}
