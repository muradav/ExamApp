using AutoMapper;
using Exam.Business.Managers;
using Exam.DataAccess.Data;
using Exam.DataAccess.Repository.IRepository;
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
        private readonly ExaminationManager ExaminationManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExaminationController(ApplicationDbContext context,IExaminationRepository examRepo,IQuestionRepository questionRepo, IExaminationDetailRepository detailRepo, IMapper mapper)
        {
            ExaminationManager = new(examRepo,questionRepo, detailRepo, mapper);
            _context = context;
            _mapper = mapper;
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


        [HttpPost("CheckExam")]
        public async Task<IActionResult> CheckExam([FromBody]CheckExamRequestDto requestDto)
        {
            var result = await ExaminationManager.CheckExam(requestDto);

            return Ok(result);
        }
    }
}
