using Exam.DataAccess.Data;
using Exam.Entities.Dtos.ExamCategoryDto;
using Exam.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExamCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExamCategoryDto category)
        {
            if (category != null)
            {
                ExamCategory examCategory = new();

                examCategory.Name = category.Name;
                await _context.AddAsync(examCategory);
                _context.SaveChanges();
                return Ok();
            }

            return NotFound();
        }
    }
}
