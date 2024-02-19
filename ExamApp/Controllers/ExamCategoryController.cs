using Exam.DataAccess.Data;
using Exam.Business.Dtos.ExamCategoryDto;
using Exam.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ExamCategoryController> _logger;
        private readonly IMapper _mapper;

        public ExamCategoryController(ApplicationDbContext context, ILogger<ExamCategoryController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ExamCategory> examCategories = await _context.ExamCategories.ToListAsync();

            var categoryDto = _mapper.Map<List<ExamCategoryDto>>(examCategories);

            return Ok(categoryDto);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetOne(int id)
        {
            ExamCategory examCategory= await _context.ExamCategories.FirstOrDefaultAsync(x => x.Id == id);

            if (examCategory != null)
            {
                ExamCategoryDto examCategoryDto = _mapper.Map<ExamCategoryDto>(examCategory);
                return Ok(examCategoryDto);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExamCategoryCreateDto createDto)
        {
            if (createDto != null)
            {
                var existCategory = await _context.ExamCategories.FirstOrDefaultAsync(x => x.Name.ToLower() == createDto.Name.ToLower());

                if (existCategory != null)
                {
                    return BadRequest("Category already exists");
                }

                ExamCategory examCategory = _mapper.Map<ExamCategory>(createDto);

                await _context.AddAsync(examCategory);
                _context.SaveChanges();
                //_logger.LogInformation($"{examCategory.Name} category is created");
                return Ok();
            }

            //_logger.LogError("Error accured");
            return NotFound();
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, [FromBody] ExamCategoryUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest();
            }

            ExamCategory examCategory = _mapper.Map<ExamCategory>(updateDto);

            _context.Update(examCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                var examCategory = await _context.ExamCategories.FirstOrDefaultAsync(x => x.Id == id);

                _context.Remove(examCategory);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }
    }
}
