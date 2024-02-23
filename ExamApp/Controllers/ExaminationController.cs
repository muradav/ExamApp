﻿using Exam.DataAccess.Data;
using Exam.Dto.Dtos.ExaminationDto;
using Exam.Entities.Models;
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
                examination.RequestCount = examinationDto.RequestCount;
                examination.ExamCategoryId = examinationDto.ExamCategoryId;

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
