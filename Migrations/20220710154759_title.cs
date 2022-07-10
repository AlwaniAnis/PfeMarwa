using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tracerapi.Migrations
{
    public partial class title : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Titre",
                table: "Taches",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titre",
                table: "Taches");
        }
    }
}
