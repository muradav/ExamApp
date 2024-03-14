using AutoMapper;
using Exam.Business.Managers.IManagers;
using Exam.DataAccess.UnitOfWorks;
using Exam.Dto.AppModel;
using Exam.Dto.Dtos.ExamCategoryDto;
using Exam.Entities.Models;
using log4net;

namespace Exam.Business.Managers
{
    public class ExamCategoryManager : IExamCategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public ExamCategoryManager(IUnitOfWork unitOfWork, IMapper mapper, ILog logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultModel<ExamCategory>> GetAllAsync()
        {
            var result = new ResultModel<ExamCategory>();
            
            IEnumerable<ExamCategory> examCategories = await _unitOfWork.ExamCategory.GetAllAsync(tracked: false);

            var categoryDto = _mapper.Map<List<ExamCategoryDto>>(examCategories);

            result.Data = categoryDto;
            result.IsSuccess = true;
            _logger.Info("All categories pulled");

            return result;
        }

        public async Task<ResultModel<ExamCategory>> GetOneAsync(int id)
        {
            var result = new ResultModel<ExamCategory>();
            
            ExamCategory examCategory = await _unitOfWork.ExamCategory.GetOneAsync(x => x.Id == id);
            ExamCategoryDto examCategoryDto = _mapper.Map<ExamCategoryDto>(examCategory);

            result.Data = examCategoryDto;
            result.IsSuccess = true;
            _logger.Info($"{examCategoryDto.Name} pulled");

            return result;
        }

        public async Task<ResultModel<bool>> AddAsync(ExamCategoryCreateDto model)
        {
            var result = new ResultModel<bool>();
            
            var existCategory = await _unitOfWork.ExamCategory.GetOneAsync(x => x.Name.ToLower() == model.Name.ToLower());
            if (existCategory == null)
            {
                ExamCategory examCategory = _mapper.Map<ExamCategory>(model);
                await _unitOfWork.ExamCategory.AddAsync(examCategory);
                await _unitOfWork.SaveAsync();
                result.Data = true;
                result.IsSuccess = true;
                _logger.Info($"{examCategory.Name} created");
            }
            else
            {
                result.Message = "Category already exist";
                _logger.Info(result.Message);
            }

            return result;
        }

        public async Task<ResultModel<bool>> UpdateAsync(int id, ExamCategoryUpdateDto model)
        {
            var result = new ResultModel<bool>();
            
            var existCategory = await _unitOfWork.ExamCategory.GetOneAsync(x => x.Id == id, tracked: false);
            if (existCategory.Id == model.Id)
            {
                ExamCategory examCategory = _mapper.Map<ExamCategory>(model);
                _unitOfWork.ExamCategory.Update(examCategory);
                await _unitOfWork.SaveAsync();
                
                result.Data = true;
                result.IsSuccess = true;
                _logger.Info($"{existCategory.Name} updated to {examCategory.Name}");
            }
            else
            {
                result.Message = "Category not found";
                _logger.Info(result.Message);
            }

            return result;
        }

        public async Task<ResultModel<bool>> DeleteAsync(int id)
        {
            var result = new ResultModel<bool>();
            
            ExamCategory examCategory = await _unitOfWork.ExamCategory.GetOneAsync(x => x.Id == id);

            _unitOfWork.ExamCategory.Remove(examCategory);
            await _unitOfWork.SaveAsync();

            result.Data = true;
            result.IsSuccess = true;
            _logger.Info($"{examCategory.Name} deleted");

            return result;
        }
    }
}
