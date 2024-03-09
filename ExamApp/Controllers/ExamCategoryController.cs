using Exam.Business.Managers.IManagers;
using Exam.Dto.Dtos.ExamCategoryDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Controllers
{
    [Authorize(Roles = "Admin,Examiner")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExamCategoryController : ControllerBase
    {
        private readonly IExamCategoryManager ExamCategoryManager;

        public ExamCategoryController(IExamCategoryManager ExamCategoryManager)
        {
            this.ExamCategoryManager = ExamCategoryManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await ExamCategoryManager.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var result = await ExamCategoryManager.GetOneAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExamCategoryCreateDto createDto)
        {
            var result = await ExamCategoryManager.AddAsync(createDto);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExamCategoryUpdateDto updateDto)
        {
            var result = await ExamCategoryManager.UpdateAsync(id, updateDto);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await ExamCategoryManager.DeleteAsync(id);

            return Ok(result);
        }
    }
}
