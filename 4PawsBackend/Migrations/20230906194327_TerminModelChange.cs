using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4PawsBackend.Migrations
{
    /// <inheritdoc />
    public partial class TerminModelChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "text",
                table: "Termin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Datum",
                table: "Termin",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Vreme",
                table: "Termin",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vreme",
                table: "Termin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Datum",
                table: "Termin",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "text",
                table: "Termin",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
