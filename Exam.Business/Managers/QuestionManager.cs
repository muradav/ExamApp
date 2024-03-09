using AutoMapper;
using Exam.Business.Managers.IManagers;
using Exam.Business.Services;
using Exam.DataAccess.UnitOfWork;
using Exam.Dto.AppModel;
using Exam.Dto.Dtos.QuestionDto;
using Exam.Entities.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Exam.Business.Managers
{
    public class QuestionManager : IQuestionManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public QuestionManager(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public async Task<ResultModel<QuestionResponseDto>> GetAllAsync()
        {
            var result = new ResultModel<QuestionResponseDto>();
            try
            {
                IEnumerable<Question> questions = await _unitOfWork.Question.GetAllAsync(include: a => a.Include(x => x.Answers), tracked: false);

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

        public async Task<ResultModel<QuestionResponseDto>> GetOneAsync(int id)
        {
            var result = new ResultModel<QuestionResponseDto>();
            try
            {
                Question question = await _unitOfWork.Question.GetOneAsync(filter: x => x.Id == id,
                                        include: x => x.Include(a => a.Answers));
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

        public async Task<ResultModel<bool>> UpdateAsync(int id, QuestionUpdateDto model)
        {
            var result = new ResultModel<bool>();
            try
            {
                var existQuestion = await _unitOfWork.Question.GetOneAsync(filter: x => x.Id == id, 
                    include: x => x.Include(a => a.Answers), tracked: false);
                if (existQuestion.Id == model.Id)
                {
                    Question question = _mapper.Map<Question>(model);

                    if (model.Image != null)
                    {
                        if (!model.Image.IsImage())
                        {
                            result.Message = "Image format is not valid";
                            result.IsSuccess = false;
                            return result;
                        }
                        if (model.Image.ValidSize(20))
                        {
                            result.Message = "Image is not valid";
                            result.IsSuccess = false;
                            return result;
                        }
                        if (existQuestion.ImageUrl !=null)
                        {
                            string questionImage = Path.Combine(_env.WebRootPath, "images/questionImages", existQuestion.ImageUrl);
                            ImageService.DeleteImage(questionImage);
                        }
                        question.ImageUrl = model.Image.SaveImage(_env, "images/questionImages");
                    }
                    for (int i = 0; i < model.Answers.Count; i++)
                    {
                        if (model.Answers[i].Image != null)
                        {
                            if (!model.Answers[i].Image.IsImage())
                            {
                                result.Message = "Image format is not valid";
                                result.IsSuccess = false;
                                return result;
                            }
                            if (model.Answers[i].Image.ValidSize(20))
                            {
                                result.Message = "Image is not valid";
                                result.IsSuccess = false;
                                return result;
                            }
                            if (existQuestion.Answers[i].ImageUrl != null)
                            {
                                string asnwerImage = Path.Combine(_env.WebRootPath, "images/answerImages", existQuestion.Answers[i].ImageUrl);
                            }
                            question.Answers[i].ImageUrl = model.Answers[i].Image.SaveImage(_env, "images/answerImages");
                        }
                    }

                    _unitOfWork.Question.Update(question);
                    await _unitOfWork.SaveAsync();

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

        public async Task<ResultModel<bool>> DeleteAsync(int id)
        {
            var result = new ResultModel<bool>();
            try
            {
                Question question = await _unitOfWork.Question.GetOneAsync(x => x.Id == id);

                _unitOfWork.Question.Remove(question);
                await _unitOfWork.SaveAsync();

                result.Data = true;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {

                result.Message = ex.Message.ToString();
            }

            return result;
        }

        public async Task<ResultModel<QuestionResponseDto>> AddAsync(QuestionCreateDto questionCreateDto)
        {
            var result = new ResultModel<QuestionResponseDto>();
            try
            {
                var existQuestion = await _unitOfWork.Question.GetOneAsync(x => x.Content.ToLower() == questionCreateDto.Content.ToLower(), tracked: false);
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
                    question.ImageUrl = questionCreateDto.Image.SaveImage(_env, "images/questionImages");
                }
                for (int i = 0; i < questionCreateDto.Answers.Count; i++)
                {
                    if (questionCreateDto.Answers[i].Image != null)
                    {
                        if (!questionCreateDto.Answers[i].Image.IsImage())
                        {
                            result.Message = "Image format is not valid";
                            result.IsSuccess = false;
                            return result;
                        }
                        if (questionCreateDto.Answers[i].Image.ValidSize(20))
                        {
                            result.Message = "Image is not valid";
                            result.IsSuccess = false;
                            return result;
                        }

                        question.Answers[i].ImageUrl = questionCreateDto.Answers[i].Image.SaveImage(_env, "images/answerImages");
                    }
                }

                await _unitOfWork.Question.AddAsync(question);
                await _unitOfWork.SaveAsync();

                QuestionResponseDto questionResponse = _mapper.Map<QuestionResponseDto>(question);

                result.Data = questionResponse;
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
                                    var category = await _unitOfWork.ExamCategory.GetOneAsync(c => c.Id == questionDto.ExamCategoryId);

                                    question.Content = reader.GetValue(1).ToString();
                                    question.ExamCategoryId = questionDto.ExamCategoryId;
                                    if (category != null)
                                    {
                                        category.QuestionCount++;
                                    }
                                    question.ExcelUrl = filename;

                                    await _unitOfWork.Question.AddAsync(question);
                                    await _unitOfWork.SaveAsync();


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

                                    await _unitOfWork.Answer.AddRangeAsync(answers);


                                    await _unitOfWork.SaveAsync();
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
