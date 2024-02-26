using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeTableDesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswersCount",
                table: "ExaminationDetails");

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswersCount",
                table: "Examinations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswersCount",
                table: "Examinations");

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswersCount",
                table: "ExaminationDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
