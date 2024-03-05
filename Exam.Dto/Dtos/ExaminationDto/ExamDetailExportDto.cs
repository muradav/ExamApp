using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Dto.Dtos.ExaminationDto
{
    public record ExamDetailExportDto
    {
        public string ExamCategory { get; set; }
        public DateOnly CreatedAt { get; set; }
        public int CorrectAnswersCount { get; set; }
        public int InCorrectAnswersCount { get; set; }
        public int Point { get; set; }
    }
}
