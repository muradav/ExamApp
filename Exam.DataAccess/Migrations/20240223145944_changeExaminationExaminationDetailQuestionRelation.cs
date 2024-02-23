using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeExaminationExaminationDetailQuestionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionQuiz");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Quizzes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Quizzes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccess",
                table: "Examinations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ExaminationQuestion",
                columns: table => new
                {
                    ExaminationsId = table.Column<int>(type: "integer", nullable: false),
                    QuestionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationQuestion", x => new { x.ExaminationsId, x.QuestionsId });
                    table.ForeignKey(
                        name: "FK_ExaminationQuestion_Examinations_ExaminationsId",
                        column: x => x.ExaminationsId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationQuestion_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_QuestionId",
                table: "Quizzes",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationQuestion_QuestionsId",
                table: "ExaminationQuestion",
                column: "QuestionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Questions_QuestionId",
                table: "Quizzes",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Questions_QuestionId",
                table: "Quizzes");

            migrationBuilder.DropTable(
                name: "ExaminationQuestion");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_QuestionId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "IsSuccess",
                table: "Examinations");

            migrationBuilder.CreateTable(
                name: "QuestionQuiz",
                columns: table => new
                {
                    QuestionsId = table.Column<int>(type: "integer", nullable: false),
                    QuizzesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionQuiz", x => new { x.QuestionsId, x.QuizzesId });
                    table.ForeignKey(
                        name: "FK_QuestionQuiz_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionQuiz_Quizzes_QuizzesId",
                        column: x => x.QuizzesId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionQuiz_QuizzesId",
                table: "QuestionQuiz",
                column: "QuizzesId");
        }
    }
}
