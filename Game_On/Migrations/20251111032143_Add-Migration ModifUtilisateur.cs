using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Game_On.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationModifUtilisateur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "TempsDeJeuCumule",
                table: "Utilisateur",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 1,
                column: "TempsDeJeuCumule",
                value: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 2,
                column: "TempsDeJeuCumule",
                value: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Utilisateur",
                keyColumn: "Id",
                keyValue: 3,
                column: "TempsDeJeuCumule",
                value: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempsDeJeuCumule",
                table: "Utilisateur");
        }
    }
}
