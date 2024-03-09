using Exam.Business.Managers.IManagers;
using Exam.Dto.Dtos.QuestionDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Controllers
{
    [Authorize(Roles = "Admin,Examiner")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionManager QuestionManager;

        public QuestionController(IQuestionManager QuestionManager)
        {
            this.QuestionManager =  QuestionManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] QuestionCreateDto questionCreateDto)
        {
            //var answers = Request.Form.FirstOrDefault(x => x.Key == nameof(QuestionCreateDto.Answers)).ToString();
            //var jsonAnswers = answers.Remove(1, answers.IndexOf(','));
            //List<AnswerCreateDto> resultAnswer = JsonConvert.DeserializeObject<List<AnswerCreateDto>>(jsonAnswers);
            //questionCreateDto.Answers = resultAnswer;

            var result = await QuestionManager.AddAsync(questionCreateDto);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await QuestionManager.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var result = await QuestionManager.GetOneAsync(id);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] QuestionUpdateDto updateDto)
        {
            var result = await QuestionManager.UpdateAsync(id, updateDto);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await QuestionManager.DeleteAsync(id);

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
