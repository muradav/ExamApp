using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Dto.Dtos.QuestionDto
{
    public class QuestionCreateDto
    {
        public string Content { get; set; }
        public int ExamCategoryId { get; set; }

        public IFormFile Image { get; set; }
        public List<AnswerCreateDto> Answers { get; set; }
    }

    public class AnswerCreateDto
    {
        public string AnswerKey { get; set; }
        public string AnswerContent { get; set; }
        public string IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public IFormFile Image { get; set; }
    }
}
