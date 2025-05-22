using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KalendarzApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEntryCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_EntryCategory_CategoryId",
                table: "Entries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntryCategory",
                table: "EntryCategory");

            migrationBuilder.RenameTable(
                name: "EntryCategory",
                newName: "EntryCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntryCategories",
                table: "EntryCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_EntryCategories_CategoryId",
                table: "Entries",
                column: "CategoryId",
                principalTable: "EntryCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_EntryCategories_CategoryId",
                table: "Entries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntryCategories",
                table: "EntryCategories");

            migrationBuilder.RenameTable(
                name: "EntryCategories",
                newName: "EntryCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntryCategory",
                table: "EntryCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_EntryCategory_CategoryId",
                table: "Entries",
                column: "CategoryId",
                principalTable: "EntryCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
