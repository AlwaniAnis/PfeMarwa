using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tracerapi.Migrations
{
    public partial class inter2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Titre",
                table: "Interventions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titre",
                table: "Interventions");
        }
    }
}
