using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Dto.Dtos.QuestionDto
{
    public record QuestionResponseDto
    {
        public int ExamCategoryId { get; set; }
        public string Content { get; set; }
        public string ImagerUrl { get; set; }
        public List<AnswerResponseDto> Answers { get; set; }

    }

    public record AnswerResponseDto
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; } = false;
        public string ImageUrl { get; set; }
    }
}
