using Exam.DataAccess.Data;
using Exam.Business.Dtos.ExamCategoryDto;
using Exam.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Exam.Business.Managers;
using Exam.DataAccess.Repository.IRepository;

namespace ExamApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamCategoryController : ControllerBase
    {
        private readonly ExamCategoryManager ExamCategoryManager;

        public ExamCategoryController(IExamCategoryRepository repository, IMapper mapper)
        {
            ExamCategoryManager = new ExamCategoryManager(repository, mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await ExamCategoryManager.GetAll();

            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetOne(int id)
        {
            var result = await ExamCategoryManager.GetOne(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExamCategoryCreateDto createDto)
        {
            var result = await ExamCategoryManager.Create(createDto);

            return Ok(result);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, [FromBody] ExamCategoryUpdateDto updateDto)
        {
            var result = await ExamCategoryManager.Update(id, updateDto);

            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await ExamCategoryManager.Delete(id);

            return Ok(result);
        }
    }
}
