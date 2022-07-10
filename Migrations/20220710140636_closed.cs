using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tracerapi.Migrations
{
    public partial class closed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "closed",
                table: "Taches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "closed",
                table: "Incidents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "closed",
                table: "Taches");

            migrationBuilder.DropColumn(
                name: "closed",
                table: "Incidents");
        }
    }
}
