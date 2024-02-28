using AutoMapper;
using Exam.Business.Managers;
using Exam.Business.Services;
using Exam.DataAccess.Data;
using Exam.DataAccess.Repository.IRepository;
using Exam.Dto.AppModel;
using Exam.Dto.Dtos.ExamCategoryDto;
using Exam.Dto.Dtos.QuestionDto;
using Exam.Entities.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace ExamApp.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionManager QuestionManager;

        public QuestionController(IQuestionRepository repository, IMapper mapper, IWebHostEnvironment env, IExamCategoryRepository categoryRepository, IAnswerRepository answerRepository)
        {
            QuestionManager = new QuestionManager(repository, mapper, env, categoryRepository, answerRepository);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] QuestionCreateDto questionCreateDto)
        {
            //var answers = Request.Form.FirstOrDefault(x => x.Key == nameof(QuestionCreateDto.Answers)).ToString();
            //var jsonAnswers = answers.Remove(1, answers.IndexOf(','));
            //List<AnswerCreateDto> resultAnswer = JsonConvert.DeserializeObject<List<AnswerCreateDto>>(jsonAnswers);
            //questionCreateDto.Answers = resultAnswer;

            var result = await QuestionManager.Create(questionCreateDto);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await QuestionManager.GetAll();

            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetOne(int id)
        {
            var result = await QuestionManager.GetOne(id);

            return Ok(result);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, [FromBody] QuestionUpdateDto updateDto)
        {
            var result = await QuestionManager.Update(id, updateDto);

            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await QuestionManager.Delete(id);

            return Ok(result);
        }

        [HttpPost("uploadExcel")]
        public async Task<IActionResult> UploadExcel([FromForm] QuestionDto questionDto)
        {
            var result = await QuestionManager.UploadExcel(questionDto);

            return Ok(result);
        }
    }
}
