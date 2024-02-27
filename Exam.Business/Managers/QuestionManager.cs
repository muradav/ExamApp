using AutoMapper;
using Exam.Business.Services;
using Exam.DataAccess.Repository.IRepository;
using Exam.Dto.AppModel;
using Exam.Dto.Dtos.ExamCategoryDto;
using Exam.Dto.Dtos.QuestionDto;
using Exam.Entities.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Business.Managers
{
    public class QuestionManager
    {
        private readonly IQuestionRepository _repo;
        private readonly IExamCategoryRepository _categoryRepo;
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public QuestionManager(IQuestionRepository repo, IMapper mapper, IWebHostEnvironment env, IExamCategoryRepository categoryRepo, IAnswerRepository answerRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _env = env;
            _categoryRepo = categoryRepo;
            _answerRepository = answerRepository;
        }

        public async Task<ResultModel<QuestionResponseDto>> GetAll()
        {
            var result = new ResultModel<QuestionResponseDto>();
            try
            {
                IEnumerable<Question> questions = await _repo.GetAll(tracked: false);

                var response = _mapper.Map<List<QuestionResponseDto>>(questions);

                result.Data = response;
                result.IsSuccess = true;

                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message.ToString();
            }
            return result;
        }

        public async Task<ResultModel<QuestionResponseDto>> GetOne(int id)
        {
            var result = new ResultModel<QuestionResponseDto>();
            try
            {
                Question question = await _repo.GetOne(x => x.Id == id);
                QuestionResponseDto response = _mapper.Map<QuestionResponseDto>(question);

                result.Data = response;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {

                result.Message = ex.Message.ToString();
            }

            return result;
        }

        public async Task<ResultModel<bool>> Update(int id, QuestionUpdateDto model)
        {
            var result = new ResultModel<bool>();
            try
            {
                var existQuestion = await _repo.GetOne(x => x.Id == id, false);
                if (existQuestion.Id == model.Id)
                {
                    Question question = _mapper.Map<Question>(model);
                    await _repo.Update(question);
                    await _repo.SaveAsync();

                    result.Data = true;
                    result.IsSuccess = true;
                }
                else
                {
                    result.Message = "Question not found";
                }
            }
            catch (Exception ex)
            {

                result.Message = ex.Message.ToString();
            }


            return result;
        }

        public async Task<ResultModel<bool>> Delete(int id)
        {
            var result = new ResultModel<bool>();
            try
            {
                Question question = await _repo.GetOne(x => x.Id == id);

                _repo.Remove(question);
                await _repo.SaveAsync();

                result.Data = true;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {

                result.Message = ex.Message.ToString();
            }

            return result;
        }

        public async Task<ResultModel<bool>> Create(QuestionCreateDto questionCreateDto)
        {
            var result = new ResultModel<bool>();
            try
            {
                var existQuestion = await _repo.GetOne(x => x.Content.ToLower() == questionCreateDto.Content.ToLower(), false);
                if (existQuestion != null)
                {
                    result.Message = "Question is exist";
                    result.IsSuccess = false;
                    return result;
                }

                Question question = _mapper.Map<Question>(questionCreateDto);

                if (questionCreateDto.Image != null)
                {
                    if (!questionCreateDto.Image.IsImage())
                    {
                        result.Message = "Image format is not valid";
                        result.IsSuccess = false;
                        return result;
                    }
                    if (questionCreateDto.Image.ValidSize(20))
                    {
                        result.Message = "Image is not valid";
                        result.IsSuccess = false;
                        return result;
                    }
                    question.ImageUrl = question.Image.SaveImage(_env, "images/questionImages");

                    foreach (var answerDto in questionCreateDto.Answers)
                    {
                        if (answerDto.Image != null)
                        {
                            if (!answerDto.Image.IsImage())
                            {
                                result.Message = "Image format is not valid";
                                result.IsSuccess = false;
                                return result;
                            }
                            if (answerDto.Image.ValidSize(20))
                            {
                                result.Message = "Image is not valid";
                                result.IsSuccess = false;
                                return result;                    
                            }

                            foreach (var answer in question.Answers)
                            {
                                answer.ImageUrl = answerDto.Image.SaveImage(_env, "images/answerImages");
                            }
                        }
                    }
                }

                await _repo.Add(question);
                await _repo.SaveAsync();

                result.Data = question;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Message =  ex.Message.ToString();
            }

            return result;
        }

        public async Task<ResultModel<bool>> UploadExcel(QuestionDto questionDto)
        {
            var result = new ResultModel<bool>();

            try
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
                                    var category = await _categoryRepo.GetOne(c => c.Id == questionDto.ExamCategoryId);

                                    question.Content = reader.GetValue(1).ToString();
                                    question.ExamCategoryId = questionDto.ExamCategoryId;
                                    if (category != null)
                                    {
                                        category.QuestionCount++;
                                    }
                                    question.ExcelUrl = filename;

                                    await _repo.Add(question);
                                    await _repo.SaveAsync();


                                    List<Answer> answers = new();
                                    for (int i = 2; i <= 5; i++)
                                    {
                                        Answer answer = new();
                                        answer.QuestionId = question.Id;
                                        answer.Content = reader.GetValue(i).ToString();
                                        if (answer.Content.ToLower() == reader.GetValue(6).ToString().ToLower())
                                        {
                                            answer.IsCorrect = true;
                                        }
                                        answers.Add(answer);
                                    }

                                    await _answerRepository.AddRange(answers);


                                    await _answerRepository.SaveAsync();
                                }
                            } while (reader.NextResult());
                        }
                    }

                    result.Data = true;
                    result.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message.ToString();
            }

            return result;
        }
    }
}
