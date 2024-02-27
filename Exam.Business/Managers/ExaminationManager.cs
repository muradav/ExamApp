using AutoMapper;
using Exam.DataAccess.Data;
using Exam.DataAccess.Repository.IRepository;
using Exam.Dto.AppModel;
using Exam.Dto.Dtos.ExamCategoryDto;
using Exam.Dto.Dtos.ExaminationDto;
using Exam.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Business.Managers
{
    
    public class ExaminationManager
    {
        private readonly IExaminationRepository _repo;
        private readonly IQuestionRepository _questionRepo;
        private readonly IExaminationDetailRepository _detailRepo;
        private readonly IMapper _mapper;

        public ExaminationManager(IExaminationRepository repo, IQuestionRepository questionRepo, IExaminationDetailRepository detailRepo, IMapper mapper)
        {
            _repo = repo;
            _questionRepo = questionRepo;
            _detailRepo = detailRepo;
            _mapper = mapper;

        }

        public async Task<ResultModel<bool>> Create(ExaminationDto model, Claim userId)
        {
            ResultModel<bool> result = new();
            try
            {
                Random rnd = new();

                var questions = await _questionRepo.GetRandom(model.RequestCount,q => q.ExamCategoryId == model.ExamCategoryId);

                Examination examination = new();
                examination.ExaminerId = userId.Value;
                examination.ExamCategoryId = model.ExamCategoryId;
                examination.Questions = questions;

                await _repo.Add(examination);
                await _repo.SaveAsync();

                result.Data = true;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ResultModel<Examination>> GetOne(int id)
        {
            var result = new ResultModel<Examination>();
            try
            {
                var examination = await _repo.GetOneWithInclude(filter: x => x.Id == id, includePredicate: x => x.Include(e => e.Questions));

                ExaminationResponseDto response = _mapper.Map<ExaminationResponseDto>(examination);

                result.Data = response;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {

                result.Message = ex.Message.ToString();
            }

            return result;
        }

        public async Task<ResultModel<bool>> CheckExam(CheckExamRequestDto requestDto)
        {
            var result = new ResultModel<bool>();
            try
            {
                var examination = await _repo.GetOneWithInclude(filter: x => x.Id == requestDto.ExaminationId,
                                                                includePredicate: x => x.Include(x => x.Questions).ThenInclude(x => x.Answers));

                List<ExaminationDetail> examDetails = new();

                foreach (var question in examination.Questions)
                {
                    foreach (var answer in question.Answers)
                    {
                        ExaminationDetail examDetail = new();

                        var requestPair = requestDto.Answers.FirstOrDefault(x => x.QuestionId == question.Id);
                        if (requestPair.ExaminerAnswer.ToLower() == answer.Content.ToLower() && answer.IsCorrect == true)
                        {
                            examDetail.isCorrect = true;
                            examination.CorrectAnswersCount++;
                        }
                        examDetail.Answer = requestPair.ExaminerAnswer;
                        examDetail.QuestionId = question.Id;
                        examDetail.ExaminationId = examination.Id;

                        examDetails.Add(examDetail);
                    }
                }

                await _detailRepo.AddRange(examDetails);
                await _detailRepo.SaveAsync();

                examination.Point = (examination.CorrectAnswersCount * 100) / examination.Questions.Count();
                if (examination.Point>65)
                {
                    examination.IsSuccess = true;
                    result.Data = "You passed exam";
                }
                else
                {
                    result.Data = "You failed";
                }

                await _repo.Update(examination);
                await _repo.SaveAsync();

                
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {

                result.Message = ex.Message.ToString();
            }
            return result;
        }
    }
}
