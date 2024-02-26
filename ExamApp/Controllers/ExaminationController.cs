using AutoMapper;
using Exam.DataAccess.Data;
using Exam.Dto.AppModel;
using Exam.Dto.Dtos.ExaminationDto;
using Exam.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExamApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExaminationController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ExaminationDto examinationDto)
        {
            ResultModel<bool> result = new();
            if (examinationDto!=null)
            {
                Random rnd = new();

                var questions = _context.Questions.Where(q => q.ExamCategoryId == examinationDto.ExamCategoryId);

                var requestedQuestions = questions.Skip(rnd.Next(questions.Count()-examinationDto.RequestCount))
                    .Take(examinationDto.RequestCount).ToList();

                var userId = User.FindFirst(ClaimTypes.NameIdentifier);

                Examination examination = new();
                examination.ExaminerId = userId.ToString();
                examination.ExamCategoryId = examinationDto.ExamCategoryId;
                examination.Questions = requestedQuestions;

                

                await _context.AddAsync(examination);
                await _context.SaveChangesAsync();

                result.Data = true;
                result.IsSuccess = true;
                return Ok(result);

            }

            return NotFound();
        }

        [HttpGet("GetExam")]
        public async Task<IActionResult> GetExam(int id)
        {
            var response = _context.Examinations.Include(x => x.Questions).FirstOrDefault(x => x.Id == id)
                                .Questions.Select(x => new ExamQuestionDto() { Content = x.Content, OptionA = x.OptionA, OptionB = x.OptionB, OptionC = x.OptionC, OptionD = x.OptionD }).ToList();


            return Ok(response);
        }


        [HttpPost("CheckExam")]
        public async Task<IActionResult> CheckExam([FromBody]CheckExamRequestDto requestDto)
        {
            var examination = await _context.Examinations.Include(x => x.Questions).FirstOrDefaultAsync(x => x.Id == requestDto.ExaminationId);
            var response = new CheckExamResponseDto(examination.ExaminerId);

            foreach (var question in examination.Questions)
            {
                var requestPair = requestDto.Results.FirstOrDefault(x => x.QuestionId == question.Id);
                if (requestPair.Answer == question.CorrectOption)
                    response.CorrectAnswersCount++;
                else
                    response.IncorrectAnswersCount++;

                response.Details.Add(new CheckResponsePair(question.CorrectOption, requestPair.Answer, question.Content));
            }

            return Ok(response);
        }
    }
}
