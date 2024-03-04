using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Dto.Dtos.QuestionDto
{
    public class QuestionUpdateDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int ExamCategoryId { get; set; }
        public IFormFile Image { get; set; }
        public List<AnswerCreateDto> Answers { get; set; }

    }

    public class AnswerUpdateDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; } = false;
        public IFormFile Image { get; set; }
    }
}
