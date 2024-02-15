using Exam.DataAccess.Data;
using Exam.Entities.Dtos.ExaminationDto;
using Exam.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExaminationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ExaminationDto examinationDto)
        {
            if (examinationDto!=null)
            {
                Random rnd = new();

                var questions = _context.Questions.Where(q => q.ExamCategoryId == examinationDto.ExamCategoryId);

                var requestedQuestions = questions.Skip(rnd.Next(questions.Count()-examinationDto.RequestCount))
                    .Take(examinationDto.RequestCount).ToList();

                Examination examination = new();
                examination.Examiner = examinationDto.Examiner;
                examination.RequestCount = examinationDto.RequestCount;
                examination.ExamCategoryId = examinationDto.ExamCategoryId;
                examination.Quizzes = requestedQuestions.Select(question => 
                {
                    var quiz = new Quiz();
                    quiz.Content = question.Content;
                    quiz.OptionA = question.OptionA;
                    quiz.OptionB = question.OptionB;
                    quiz.OptionC = question.OptionC;
                    quiz.OptionD = question.OptionD;
                    quiz.CorrectOption = question.CorrectOption;
                    quiz.QuestionId = question.Id;
                    return quiz;
                }).ToList();

                await _context.AddAsync(examination);
                await _context.SaveChangesAsync();

                return Ok(examination);
            }

            return NotFound();
        }

        //public async Task<IActionResult> CheckResult()
        //{

        //    return Ok();
        //}
    }
}
