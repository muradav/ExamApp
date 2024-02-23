namespace Exam.Entities.Models
{
    public class ExaminationDetail : BaseEntity
    {
        public string Answer { get; set; }
        public bool isCorrect { get; set; }

        public int ExaminationId { get; set; }
        public Examination Examination { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }    
}
