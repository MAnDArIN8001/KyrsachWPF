﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecepiesEverywhere.Migrations
{
    /// <inheritdoc />
    public partial class OneToMoreMarks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarkUser");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Marks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Marks_UsersId",
                table: "Marks",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Users_UsersId",
                table: "Marks",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Users_UsersId",
                table: "Marks");

            migrationBuilder.DropIndex(
                name: "IX_Marks_UsersId",
                table: "Marks");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Marks");

            migrationBuilder.CreateTable(
                name: "MarkUser",
                columns: table => new
                {
                    MarksId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkUser", x => new { x.MarksId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_MarkUser_Marks_MarksId",
                        column: x => x.MarksId,
                        principalTable: "Marks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarkUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarkUser_UsersId",
                table: "MarkUser",
                column: "UsersId");
        }
    }
}
