using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4PawsBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedDatumStringColumnToTermin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DatumString",
                table: "Termin",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatumString",
                table: "Termin");
        }
    }
}
