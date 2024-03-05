using Microsoft.AspNetCore.Http;

namespace Exam.Dto.Dtos.QuestionDto
{
    public record QuestionDto
    {
        public IFormFile ExcelFile { get; set; }
        public int ExamCategoryId { get; set; }
    }

}
