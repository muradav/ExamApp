﻿using AutoMapper;
using ClosedXML.Excel;
using Exam.Business.Services;
using Exam.DataAccess.Repository.IRepository;
using Exam.Dto.AppModel;
using Exam.Dto.Dtos.ExaminationDto;
using Exam.Entities.Models;
using Microsoft.AspNetCore.Mvc;
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
                examination.ExamineeId = userId.Value;
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

        public async Task<ResultModel<ExaminationResponseDto>> GetOne(int id)
        {
            var result = new ResultModel<ExaminationResponseDto>();
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

        public async Task<ResultModel<List<ExaminationResponseDto>>> GetAll(string userId)
        {
            var result = new ResultModel<List<ExaminationResponseDto>>();
            try
            {
                var examination = await _repo.GetAll(filter: x => x.ExamineeId == userId,
                                includePredicate: x => x.Include(e => e.Questions).ThenInclude(e => e.Answers));

                List<ExaminationResponseDto> response = _mapper.Map<List<ExaminationResponseDto>>(examination);

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

        public async Task<FileContentResult> ExportData(string userId, ControllerBase controller)
        {

            try
            {
                var examinations = await _repo.GetAll(x => x.ExamineeId == userId, 
                                        includePredicate: x => x.Include(e => e.Questions).Include(e => e.ExamCategory));

                List<ExamDetailExportDto>  exportDetailDto = _mapper.Map<List<ExamDetailExportDto>>(examinations);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Examination Report");

                    worksheet.Range("A1:F1").Merge();
                    worksheet.Cell(1, 1).Value = "Report";
                    worksheet.Cell(1, 1).Style.Font.Bold = true;
                    worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(1, 1).Style.Font.FontSize = 30;

                    worksheet.Cell(2, 1).Value = "№";
                    worksheet.Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(2, 2).Value = "Examination";
                    worksheet.Cell(2, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(2, 3).Value = "Examination Date";
                    worksheet.Cell(2, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(2, 4).Value = "Correct Answers Count";
                    worksheet.Cell(2, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(2, 5).Value = "Incorrect Answers Count";
                    worksheet.Cell(2, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(2, 6).Value = "Point";
                    worksheet.Cell(2, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("A2:F2").Style.Fill.BackgroundColor = XLColor.Alizarin;

                    int rowNumber = 1;
                    int rowStart = 3;
                    foreach (var detail in exportDetailDto)
                    {
                        worksheet.Cell(rowStart, 1).Value = rowNumber;
                        worksheet.Cell(rowStart, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(rowStart, 2).Value = detail.ExamCategory;
                        worksheet.Cell(rowStart, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(rowStart, 3).Value = detail.CreatedAt.ToString();
                        worksheet.Cell(rowStart, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(rowStart, 4).Value = detail.CorrectAnswersCount;
                        worksheet.Cell(rowStart, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(rowStart, 5).Value = detail.InCorrectAnswersCount;
                        worksheet.Cell(rowStart, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(rowStart, 6).Value = detail.Point;
                        worksheet.Cell(rowStart, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        rowNumber++;
                        rowStart++;
                    }
                    rowStart--;

                    worksheet.Cells("A2:F" + rowStart).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cells("A2:F" + rowStart).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cells("A2:F" + rowStart).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cells("A2:F" + rowStart).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        string filename = "Examination Report.xlsx";
                        return controller.File(content, mimeType, filename);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
