using Exam.Entities.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Exam.Business.Services
{
    public static class ExcelService
    {
        public static  string UploadExcel(this IFormFile file, IWebHostEnvironment env, string folder)
        {
            string filename = "exam"+ Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(env.WebRootPath, folder, filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                 file.CopyTo(stream);
            }

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
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
                            //var category = _context.ExamCategories.FirstOrDefault(c => c.Id == questionDto.ExamCategoryId);

                            question.Content = reader.GetValue(1).ToString();
                            question.OptionA = reader.GetValue(2).ToString();
                            question.OptionB = reader.GetValue(3).ToString();
                            question.OptionC = reader.GetValue(4).ToString();
                            question.OptionD = reader.GetValue(5).ToString();
                            question.CorrectOption = reader.GetValue(6).ToString();

                            //question.ExamCategoryId = questionDto.ExamCategoryId;
                            //category.QuestionCount++;
                            question.ExcelUrl = filename;

                            //await _context.AddAsync(question);
                            //_context.SaveChanges();
                        }
                    } while (reader.NextResult());
                }
            }

            return filename;
        }
    }
}
