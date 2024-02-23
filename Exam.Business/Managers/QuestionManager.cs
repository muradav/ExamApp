using AutoMapper;
using Exam.DataAccess.Repository.IRepository;
using Exam.Dto.AppModel;
using Exam.Dto.Dtos.ExamCategoryDto;
using Exam.Dto.Dtos.QuestionDto;
using Exam.Entities.Models;
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
        private readonly IMapper _mapper;

        public QuestionManager(IQuestionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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
    }
}
