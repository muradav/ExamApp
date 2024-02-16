using Microsoft.AspNetCore.Http;

namespace Exam.DataAccess.Dtos.QuestionDto
{
    public class QuestionDto
    {
        public IFormFile ExcelFile { get; set; }
        public int ExamCategoryId { get; set; }
    }

}
