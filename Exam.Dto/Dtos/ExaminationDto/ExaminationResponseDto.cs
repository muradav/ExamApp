using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Dto.Dtos.ExaminationDto
{
    public record ExaminationResponseDto
    {
        public int ExamCategoryId { get; set; }
        public List<ExamQuestionDto> Questions { get; set; }
    }

    public record ExamQuestionDto
    {
        public string Content { get; set; }
        public List<ExamAnswerDto> Answers { get; set; }
    }

    public record ExamAnswerDto 
    {
        public string Content { get; set; }
        public string ImageUrl { get; set; }
    }
}
