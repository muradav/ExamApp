using AutoMapper;
using Exam.Business.Managers;
using Exam.DataAccess.Data;
using Exam.DataAccess.Repository.IRepository;
using Exam.Dto.Dtos.ExamCategoryDto;
using Exam.Dto.Dtos.QuestionDto;
using Exam.Entities.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ExamApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {

        private readonly IWebHostEnvironment _env;
        private readonly QuestionManager QuestionManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public QuestionController(IQuestionRepository repository, IMapper mapper, IWebHostEnvironment env, ApplicationDbContext context)
        {
            QuestionManager = new QuestionManager(repository, mapper);
            _env = env;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestionCreateDto questionCreateDto)
        {
            var existQuestion = await _context.Questions.FirstOrDefaultAsync(x=> x.Content.ToLower() == questionCreateDto.Content.ToLower());
            if (existQuestion != null)
            {
                return BadRequest();
            }

            Question question = _mapper.Map<Question>(existQuestion);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await QuestionManager.GetAll();

            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetOne(int id)
        {
            var result = await QuestionManager.GetOne(id);

            return Ok(result);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, [FromBody] QuestionUpdateDto updateDto)
        {
            var result = await QuestionManager.Update(id, updateDto);

            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await QuestionManager.Delete(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UploadExcel([FromForm] QuestionDto questionDto)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (questionDto.ExcelFile != null && questionDto.ExcelFile.Length > 0)
            {

                string filename = "exam" + Guid.NewGuid().ToString() + Path.GetExtension(questionDto.ExcelFile.FileName);

                var filePath = Path.Combine(_env.WebRootPath, "excel", filename);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    questionDto.ExcelFile.CopyTo(stream);
                }

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            bool isHeaderSkipped = false;
                            while (reader.Read())
                            {
                                if (!isHeaderSkipped)
                                {
                                    isHeaderSkipped = true;
                                    continue;
                                }

                                Question question = new();
                                var category = _context.ExamCategories.FirstOrDefault(c => c.Id == questionDto.ExamCategoryId);

                                question.Content = reader.GetValue(1).ToString();
                                //question.OptionA = reader.GetValue(2).ToString();
                                //question.OptionB = reader.GetValue(3).ToString();
                                //question.OptionC = reader.GetValue(4).ToString();
                                //question.OptionD = reader.GetValue(5).ToString();
                                //question.CorrectOption = reader.GetValue(6).ToString();

                                question.ExamCategoryId = questionDto.ExamCategoryId;
                                if (category != null)
                                {
                                    category.QuestionCount++;
                                }

                                question.ExcelUrl = filename;

                                await _context.AddAsync(question);
                                _context.SaveChanges();
                            }
                        } while (reader.NextResult());
                    }
                }

                return Ok();
            }

            return NotFound();
        }
    }
}
