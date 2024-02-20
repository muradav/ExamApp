using AutoMapper;
using Exam.Business.AppModel;
using Exam.Business.Dtos.ExamCategoryDto;
using Exam.DataAccess.Data;
using Exam.DataAccess.Repository.IRepository;
using Exam.Entities.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Business.Managers
{
    public class ExamCategoryManager
    {
        private readonly IExamCategoryRepository _repo;
        private readonly IMapper _mapper;

        public ExamCategoryManager(IExamCategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ResultModel<ExamCategory>> GetAll()
        {
            var result = new ResultModel<ExamCategory>();
            try
            {
                IEnumerable<ExamCategory> examCategories = await _repo.GetAll(tracked: false);

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

        public async Task<ResultModel<ExamCategory>> GetOne(int id)
        {
            var result = new ResultModel<ExamCategory>();
            try
            {
                ExamCategory examCategory = await _repo.GetOne(x => x.Id == id);
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

        public async Task<ResultModel<bool>> Create(ExamCategoryCreateDto model)
        {
            var result = new ResultModel<bool>();
            try
            {
                var existCategory = await _repo.GetOne(x => x.Name.ToLower() == model.Name.ToLower());
                if (existCategory == null)
                {
                    ExamCategory examCategory = _mapper.Map<ExamCategory>(model);
                    await _repo.Add(examCategory);
                    await _repo.SaveAsync();
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

        public async Task<ResultModel<bool>> Update(int id, ExamCategoryUpdateDto model)
        {
            var result = new ResultModel<bool>();
            try
            {
                var existCategory = await _repo.GetOne(x => x.Id == id, false);
                if (existCategory.Id == model.Id)
                {
                    ExamCategory examCategory = _mapper.Map<ExamCategory>(model);
                    await _repo.Update(examCategory);
                    await _repo.SaveAsync();
                    
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

        public async Task<ResultModel<bool>> Delete(int id)
        {
            var result = new ResultModel<bool>();
            try
            {
                ExamCategory examCategory = await _repo.GetOne(x => x.Id == id);

                _repo.Remove(examCategory);
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
