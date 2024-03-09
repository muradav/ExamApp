using Exam.Dto.AppModel;
using Exam.Dto.Dtos.QuestionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Business.Managers.IManagers
{
    public interface IQuestionManager
    {
        Task<ResultModel<QuestionResponseDto>> GetAllAsync();
        Task<ResultModel<QuestionResponseDto>> GetOneAsync(int id);
        Task<ResultModel<bool>> UpdateAsync(int id, QuestionUpdateDto model);
        Task<ResultModel<bool>> DeleteAsync(int id);
        Task<ResultModel<QuestionResponseDto>> AddAsync(QuestionCreateDto questionCreateDto);
        Task<ResultModel<bool>> UploadExcel(QuestionDto questionDto);
    }
}
