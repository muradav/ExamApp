using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Exam.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedExamCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExamCategories",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "ModifiedAt", "Name", "QuestionCount" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 2, 16), false, new DateOnly(1, 1, 1), "General Knowledge", 0 },
                    { 2, new DateOnly(2024, 2, 16), false, new DateOnly(1, 1, 1), "Mathematics", 0 },
                    { 3, new DateOnly(2024, 2, 16), false, new DateOnly(1, 1, 1), "History", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
