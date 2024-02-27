using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removeColumnAnswerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerContent",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "AnswerKey",
                table: "Answers",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Answers",
                newName: "AnswerKey");

            migrationBuilder.AddColumn<string>(
                name: "AnswerContent",
                table: "Answers",
                type: "text",
                nullable: true);
        }
    }
}
