using Exam.Entities.Models;

namespace Exam.Dto.Dtos.ExaminationDto
{
    public record ExaminationDto
    {
        public int RequestCount { get; set; }
        public int ExamCategoryId { get; set; }

    }
}
