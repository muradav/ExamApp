using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Dto.Dtos.QuestionDto
{
    public class QuestionResponseDto
    {
        public int ExamCategoryId { get; set; }
        public string Content { get; set; }
        public string ImagerUrl { get; set; }
        public List<AnswerResponseDto> Answers { get; set; }

    }

    public class AnswerResponseDto
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; } = false;
        public string ImageUrl { get; set; }
    }
}
