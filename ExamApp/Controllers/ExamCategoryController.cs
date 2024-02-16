using Exam.DataAccess.Data;
using Exam.DataAccess.Dtos.ExamCategoryDto;
using Exam.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ExamCategoryController> _logger;

        public ExamCategoryController(ApplicationDbContext context, ILogger<ExamCategoryController> logger = null)
        {
            _context = context;
            _logger = logger;
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
                //_logger.LogInformation($"{examCategory.Name} category is created");
                return Ok();
            }

            //_logger.LogError("Error accured");
            return NotFound();
        }
    }
}
