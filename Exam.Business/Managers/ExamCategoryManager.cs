using AutoMapper;
using Exam.Business.Managers.IManagers;
using Exam.DataAccess.Repository.IRepository;
using Exam.DataAccess.UnitOfWork;
using Exam.Dto.AppModel;
using Exam.Dto.Dtos.ExamCategoryDto;
using Exam.Entities.Models;

namespace Exam.Business.Managers
{
    public class ExamCategoryManager : IExamCategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExamCategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultModel<ExamCategory>> GetAllAsync()
        {
            var result = new ResultModel<ExamCategory>();
            try
            {
                IEnumerable<ExamCategory> examCategories = await _unitOfWork.ExamCategory.GetAllAsync(tracked: false);

                var categoryDto = _mapper.Map<List<ExamCategoryDto>>(examCategories);

                result.Data = categoryDto;
                result.IsSuccess = true;
                
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message.ToString();
            }
            return result;
        }

        public async Task<ResultModel<ExamCategory>> GetOneAsync(int id)
        {
            var result = new ResultModel<ExamCategory>();
            try
            {
                ExamCategory examCategory = await _unitOfWork.ExamCategory.GetOneAsync(x => x.Id == id);
                ExamCategoryDto examCategoryDto = _mapper.Map<ExamCategoryDto>(examCategory);

                result.Data = examCategoryDto;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {

                result.Message = ex.Message.ToString();
            }

            return result;
        }

        public async Task<ResultModel<bool>> AddAsync(ExamCategoryCreateDto model)
        {
            var result = new ResultModel<bool>();
            try
            {
                var existCategory = await _unitOfWork.ExamCategory.GetOneAsync(x => x.Name.ToLower() == model.Name.ToLower());
                if (existCategory == null)
                {
                    ExamCategory examCategory = _mapper.Map<ExamCategory>(model);
                    await _unitOfWork.ExamCategory.AddAsync(examCategory);
                    await _unitOfWork.SaveAsync();
                    result.Data = true;
                    result.IsSuccess = true;
                }
                else
                {
                    result.Message = "Category already exist";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message.ToString();
            }

            return result;
        }

        public async Task<ResultModel<bool>> UpdateAsync(int id, ExamCategoryUpdateDto model)
        {
            var result = new ResultModel<bool>();
            try
            {
                var existCategory = await _unitOfWork.ExamCategory.GetOneAsync(x => x.Id == id, tracked: false);
                if (existCategory.Id == model.Id)
                {
                    ExamCategory examCategory = _mapper.Map<ExamCategory>(model);
                    _unitOfWork.ExamCategory.Update(examCategory);
                    await _unitOfWork.SaveAsync();
                    
                    result.Data = true;
                    result.IsSuccess = true;
                }
                else
                {
                    result.Message = "Category not found";
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
                ExamCategory examCategory = await _unitOfWork.ExamCategory.GetOneAsync(x => x.Id == id);

                _unitOfWork.ExamCategory.Remove(examCategory);
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
    }
}
