using AutoMapper;
using Exam.Business.Managers;
using Exam.Business.Services;
using Exam.DataAccess.Data;
using Exam.DataAccess.Repository.IRepository;
using Exam.Dto.Dtos.ExaminationDto;
using Exam.Entities.Enums;
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
        private readonly ExaminationManager ExaminationManager;

        public ExaminationController(ApplicationDbContext context,IExaminationRepository examRepo,IQuestionRepository questionRepo, IExaminationDetailRepository detailRepo, IMapper mapper)
        {
            ExaminationManager = new(examRepo,questionRepo, detailRepo, mapper);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ExaminationDto examinationDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            var result = await ExaminationManager.Create(examinationDto,userId);

            return Ok(result);
        }

        [HttpGet("GetExam")]
        public async Task<IActionResult> GetExam(int id)
        {
            var result = await ExaminationManager.GetOne(id);

            return Ok(result);
        }

        [HttpGet("GetAllExams")]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await ExaminationManager.GetAll(userId);

            return Ok(result);
        }

        [HttpPost("CheckExam")]
        public async Task<IActionResult> CheckExam([FromBody]CheckExamRequestDto requestDto)
        {
            var result = await ExaminationManager.CheckExam(requestDto);

            return Ok(result);
        }

        [HttpGet("ExportExam")]
        public async Task<IActionResult> GetExamDetail()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await ExaminationManager.ExportData(userId, this);

            return result;
        }
    }
}
