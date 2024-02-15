namespace Exam.Entities.Models
{
    public class Quiz : BaseEntity
    {
        public string? Content { get; set; }
        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }
        public string? CorrectOption { get; set; }
        public bool isCorrect { get; set; }

        public int ExaminationId { get; set; }
        public Examination Examination { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }    
}
