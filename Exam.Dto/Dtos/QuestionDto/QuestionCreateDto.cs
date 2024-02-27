using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public string Content { get; set; }
        public bool IsCorrect { get; set; } = false;
        public IFormFile Image { get; set; }
    }
}
