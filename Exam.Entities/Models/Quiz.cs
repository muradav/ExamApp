namespace Exam.Entities.Models
{
    public class Quiz : BaseEntity
    {
        public bool isCorrect { get; set; }

        public int ExaminationId { get; set; }
        public Examination Examination { get; set; }

        public List<Question> Questions { get; set; }
    }    
}
