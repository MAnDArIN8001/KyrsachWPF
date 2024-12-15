using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecepiesEverywhere.Migrations
{
    /// <inheritdoc />
    public partial class OneToMoreMarks2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Users_UsersId",
                table: "Marks");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Marks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Marks_UsersId",
                table: "Marks",
                newName: "IX_Marks_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Users_UserId",
                table: "Marks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Users_UserId",
                table: "Marks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Marks",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Marks_UserId",
                table: "Marks",
                newName: "IX_Marks_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Users_UsersId",
                table: "Marks",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
