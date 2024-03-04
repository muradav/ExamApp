using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changePropertyNameExaminationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_AspNetUsers_ExaminerId",
                table: "Examinations");

            migrationBuilder.RenameColumn(
                name: "ExaminerId",
                table: "Examinations",
                newName: "ExamineeId");

            migrationBuilder.RenameIndex(
                name: "IX_Examinations_ExaminerId",
                table: "Examinations",
                newName: "IX_Examinations_ExamineeId");

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateOnly(2024, 3, 4));

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateOnly(2024, 3, 4));

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateOnly(2024, 3, 4));

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_AspNetUsers_ExamineeId",
                table: "Examinations",
                column: "ExamineeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_AspNetUsers_ExamineeId",
                table: "Examinations");

            migrationBuilder.RenameColumn(
                name: "ExamineeId",
                table: "Examinations",
                newName: "ExaminerId");

            migrationBuilder.RenameIndex(
                name: "IX_Examinations_ExamineeId",
                table: "Examinations",
                newName: "IX_Examinations_ExaminerId");

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateOnly(2024, 2, 27));

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateOnly(2024, 2, 27));

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateOnly(2024, 2, 27));

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_AspNetUsers_ExaminerId",
                table: "Examinations",
                column: "ExaminerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
