using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class modifyExaminationDetailEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Examinations_ExaminationId",
                table: "Quizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Questions_QuestionId",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quizzes",
                table: "Quizzes");

            migrationBuilder.RenameTable(
                name: "Quizzes",
                newName: "ExaminationDetails");

            migrationBuilder.RenameIndex(
                name: "IX_Quizzes_QuestionId",
                table: "ExaminationDetails",
                newName: "IX_ExaminationDetails_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Quizzes_ExaminationId",
                table: "ExaminationDetails",
                newName: "IX_ExaminationDetails_ExaminationId");

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswersCount",
                table: "ExaminationDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExaminationDetails",
                table: "ExaminationDetails",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateOnly(2024, 2, 26));

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateOnly(2024, 2, 26));

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateOnly(2024, 2, 26));

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationDetails_Examinations_ExaminationId",
                table: "ExaminationDetails",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationDetails_Questions_QuestionId",
                table: "ExaminationDetails",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationDetails_Examinations_ExaminationId",
                table: "ExaminationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationDetails_Questions_QuestionId",
                table: "ExaminationDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExaminationDetails",
                table: "ExaminationDetails");

            migrationBuilder.DropColumn(
                name: "CorrectAnswersCount",
                table: "ExaminationDetails");

            migrationBuilder.RenameTable(
                name: "ExaminationDetails",
                newName: "Quizzes");

            migrationBuilder.RenameIndex(
                name: "IX_ExaminationDetails_QuestionId",
                table: "Quizzes",
                newName: "IX_Quizzes_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_ExaminationDetails_ExaminationId",
                table: "Quizzes",
                newName: "IX_Quizzes_ExaminationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quizzes",
                table: "Quizzes",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Examinations_ExaminationId",
                table: "Quizzes",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Questions_QuestionId",
                table: "Quizzes",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
