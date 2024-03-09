using Exam.Dto.AppModel;
using Exam.Dto.Dtos.ExaminationDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Business.Managers.IManagers
{
    public interface IExaminationManager
    {
        Task<ResultModel<bool>> AddAsync(ExaminationDto model, Claim userId);
        Task<ResultModel<ExaminationResponseDto>> GetOneAsync(int id);
        Task<ResultModel<List<ExaminationResponseDto>>> GetAllAsync(string userId);
        Task<ResultModel<bool>> CheckExam(CheckExamRequestDto requestDto);
        Task<FileContentResult> ExportData(string userId, ControllerBase controller);

    }
}
