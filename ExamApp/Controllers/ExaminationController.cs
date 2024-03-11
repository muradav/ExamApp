using Exam.Business.Managers.IManagers;
using Exam.Dto.Dtos.ExaminationDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExamApp.Controllers
{
    [Authorize(Roles = "Examinee")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private readonly IExaminationManager ExaminationManager;

        public ExaminationController(IExaminationManager ExaminationManager)
        {
            this.ExaminationManager = ExaminationManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ExaminationDto examinationDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            var result = await ExaminationManager.AddAsync(examinationDto,userId);

            return Ok(result);
        }

        [HttpGet("examination")]
        public async Task<IActionResult> GetExam(int id)
        {
            var result = await ExaminationManager.GetOneAsync(id);

            return Ok(result);
        }

        [HttpGet("allExaminations")]
        public async Task<IActionResult> GetAll()
        {
            throw new Exception("Test exception");
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await ExaminationManager.GetAllAsync(userId);

            return Ok(result);
        }

        [HttpPost("checkinExam")]
        public async Task<IActionResult> CheckExam([FromBody]CheckExamRequestDto requestDto)
        {
            var result = await ExaminationManager.CheckExam(requestDto);

            return Ok(result);
        }

        [HttpGet("exportingExam")]
        public async Task<IActionResult> GetExamDetail()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await ExaminationManager.ExportData(userId, this);

            return result;
        }
    }
}
