using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeExaminationUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Examiner",
                table: "Examinations",
                newName: "ExaminerId");

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateOnly(2024, 2, 23));

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateOnly(2024, 2, 23));

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateOnly(2024, 2, 23));

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_ExaminerId",
                table: "Examinations",
                column: "ExaminerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_AspNetUsers_ExaminerId",
                table: "Examinations",
                column: "ExaminerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_AspNetUsers_ExaminerId",
                table: "Examinations");

            migrationBuilder.DropIndex(
                name: "IX_Examinations_ExaminerId",
                table: "Examinations");

            migrationBuilder.RenameColumn(
                name: "ExaminerId",
                table: "Examinations",
                newName: "Examiner");

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateOnly(2024, 2, 22));

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateOnly(2024, 2, 22));

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateOnly(2024, 2, 22));
        }
    }
}
