using Exam.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Exam.Business.Services;
using Exam.Entities.Dtos.QuestionDto;
using Exam.DataAccess.Data;
using ExcelDataReader;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {

        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;

        public QuestionController(IWebHostEnvironment env, ApplicationDbContext context)
        {
            _env = env;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> UploadExcel([FromForm] QuestionDto questionDto)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (questionDto.ExcelFile != null && questionDto.ExcelFile.Length >0)
            {

                string filename = "exam" + Guid.NewGuid().ToString() + Path.GetExtension(questionDto.ExcelFile.FileName);

                var filePath = Path.Combine(_env.WebRootPath, "excel", filename);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    questionDto.ExcelFile.CopyTo(stream);
                }

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            bool isHeaderSkipped = false;
                            while (reader.Read())
                            {
                                if (!isHeaderSkipped)
                                {
                                    isHeaderSkipped = true;
                                    continue;
                                }

                                Question question = new();
                                var category = _context.ExamCategories.FirstOrDefault(c => c.Id == questionDto.ExamCategoryId);

                                question.Content = reader.GetValue(1).ToString();
                                question.OptionA = reader.GetValue(2).ToString();
                                question.OptionB = reader.GetValue(3).ToString();
                                question.OptionC = reader.GetValue(4).ToString();
                                question.OptionD = reader.GetValue(5).ToString();
                                question.CorrectOption = reader.GetValue(6).ToString();

                                question.ExamCategoryId = questionDto.ExamCategoryId;
                                category.QuestionCount++;

                                question.ExcelUrl = filename; 

                                await _context.AddAsync(question);
                                _context.SaveChanges();
                            }
                        } while (reader.NextResult());
                    }
                }

                return Ok();
            }

            return NotFound();
        }
    }
}
