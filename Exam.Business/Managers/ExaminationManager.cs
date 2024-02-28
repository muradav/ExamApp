using AutoMapper;
using Exam.DataAccess.Repository.IRepository;
using Exam.Dto.AppModel;
using Exam.Dto.Dtos.ExaminationDto;
using Exam.Entities.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Security.Claims;

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
                var examination = await _repo.GetOneWithInclude(filter: x => x.Id == id, 
                                includePredicate: x => x.Include(e => e.Questions).ThenInclude(e => e.Answers));

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
                    ExaminationDetail examDetail = new();
                    var requestPair = requestDto.Answers.FirstOrDefault(x => x.QuestionId == question.Id);

                    foreach (var answer in question.Answers)
                    {
                        if (requestPair.ExaminerAnswer.ToLower() == answer.Content.ToLower() && answer.IsCorrect == true)
                        {
                            examDetail.isCorrect = true;
                            examination.CorrectAnswersCount++;
                        }
                    }
                    examDetail.Answer = requestPair.ExaminerAnswer;
                    examDetail.QuestionId = question.Id;
                    examDetail.ExaminationId = examination.Id;

                    examDetails.Add(examDetail);
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

        public async Task<ResultModel<ExamDetailExportDto>> ExportExamDetail(string userId)
        {
            var result = new ResultModel<ExamDetailExportDto>();

            try
            {
                var examinations = await _repo.GetAll(x => x.ExaminerId == userId, 
                                        includePredicate: x => x.Include(e => e.Questions).Include(e => e.ExamCategory));
                List<ExamDetailExportDto> exportDetailDto = _mapper.Map<List<ExamDetailExportDto>>(examinations);


                

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    ExcelWorksheet ws = package.Workbook.Worksheets.Add("Report");

                    ws.Cells["B3"].Value = "Examination";
                    ws.Cells["C3"].Value = "Examination Date";
                    ws.Cells["D3"].Value = "Correct Answers Count";
                    ws.Cells["E3"].Value = "Incorrect Answers Count";
                    ws.Cells["F3"].Value = "Point";

                    int rowStart = 4;
                    foreach (var examDetail in exportDetailDto)
                    {
                        ws.Cells[string.Format("B{0}", rowStart)].Value = examDetail.ExamCategory;
                        ws.Cells[string.Format("C{0}", rowStart)].Value = examDetail.CreatedAt;
                        ws.Cells[string.Format("D{0}", rowStart)].Value = examDetail.CorrectAnswersCount;
                        ws.Cells[string.Format("E{0}", rowStart)].Value = examDetail.InCorrectAnswersCount;
                        ws.Cells[string.Format("F{0}", rowStart)].Value = examDetail.Point;

                        rowStart++;
                    }


                    using (var stream = new MemoryStream())
                    {
                        package.SaveAs(stream);
                        var content = stream.ToArray();
                        result.Data = true;
                        result.IsSuccess = true;
                        return File(content,
                                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                    "StudenExams.xlsx");
                    }

                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message.ToString();
            }

            return result;
        }

        private ResultModel<ExamDetailExportDto> File(byte[] content, string v1, string v2)
        {
            throw new NotImplementedException();
        }
    }
}
