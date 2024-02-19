using Microsoft.AspNetCore.Http;

namespace Exam.Business.Dtos.QuestionDto
{
    public class QuestionDto
    {
        public IFormFile ExcelFile { get; set; }
        public int ExamCategoryId { get; set; }
    }

}
