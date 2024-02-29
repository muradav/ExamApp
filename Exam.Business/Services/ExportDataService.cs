using ClosedXML.Excel;
using Exam.Dto.Dtos.ExaminationDto;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Business.Services
{
    public static class ExportDataService
    {
        public static FileContentResult ExportData(this List<ExamDetailExportDto> model, ControllerBase controller)
        {
			try
			{
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Examination Report");

                    worksheet.Range("A1:E1").Merge();
                    worksheet.Cell(1, 1).Value = "Report";
                    worksheet.Cell(1, 1).Style.Font.Bold = true;
                    worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(1, 1).Style.Font.FontSize = 30;

                    worksheet.Cell(2, 1).Value = "Examination";
                    worksheet.Cell(2, 2).Value = "Examination Date";
                    worksheet.Cell(2, 3).Value = "Correct Answers Count";
                    worksheet.Cell(2, 4).Value = "Incorrect Answers Count";
                    worksheet.Cell(2, 5).Value = "Point";
                    worksheet.Range("A2:E2").Style.Fill.BackgroundColor = XLColor.Alizarin;


                    int rowStart = 3;
                    foreach (var detail in model)
                    {
                        worksheet.Cell(rowStart, 1).Value = detail.ExamCategory;
                        worksheet.Cell(rowStart, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(rowStart, 2).Value = detail.CreatedAt.ToString();
                        worksheet.Cell(rowStart, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(rowStart, 3).Value = detail.CorrectAnswersCount;
                        worksheet.Cell(rowStart, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(rowStart, 4).Value = detail.InCorrectAnswersCount;
                        worksheet.Cell(rowStart, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(rowStart, 5).Value = detail.Point;
                        worksheet.Cell(rowStart, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        rowStart++;
                    }
                    rowStart--;

                    worksheet.Cells("A2:E" + rowStart).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cells("A2:E" + rowStart).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cells("A2:E" + rowStart).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cells("A2:E" + rowStart).Style.Border.RightBorder = XLBorderStyleValues.Thin;

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
