using Exam.Dto.AppModel;
using Exam.Dto.Dtos.ExamCategoryDto;
using Exam.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Business.Managers.IManagers
{
    public interface IExamCategoryManager
    {
        Task<ResultModel<ExamCategory>> GetAllAsync();
        Task<ResultModel<ExamCategory>> GetOneAsync(int id);
        Task<ResultModel<bool>> AddAsync(ExamCategoryCreateDto model);
        Task<ResultModel<bool>> UpdateAsync(int id, ExamCategoryUpdateDto model);
        Task<ResultModel<bool>> DeleteAsync(int id);
    }
}
