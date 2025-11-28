using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Game_On.Migrations
{
    /// <inheritdoc />
    public partial class AjoutScoreEntrprise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Entreprise",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Entreprise",
                keyColumn: "Id",
                keyValue: 1,
                column: "Score",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Entreprise",
                keyColumn: "Id",
                keyValue: 2,
                column: "Score",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Entreprise");
        }
    }
}
