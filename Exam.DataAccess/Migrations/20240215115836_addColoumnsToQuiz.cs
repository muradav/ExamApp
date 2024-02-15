using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addColoumnsToQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Quizzes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorrectOption",
                table: "Quizzes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OptionA",
                table: "Quizzes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OptionB",
                table: "Quizzes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OptionC",
                table: "Quizzes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OptionD",
                table: "Quizzes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "CorrectOption",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "OptionA",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "OptionB",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "OptionC",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "OptionD",
                table: "Quizzes");
        }
    }
}
