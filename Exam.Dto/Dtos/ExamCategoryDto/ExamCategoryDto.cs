namespace Exam.Dto.Dtos.ExamCategoryDto
{
    public record ExamCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuestionCount { get; set; }
    }
}
