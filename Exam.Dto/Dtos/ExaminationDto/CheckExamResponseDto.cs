using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Dto.Dtos.ExaminationDto
{
    public record CheckExamResponseDto
    {
        public int CorrectAnswersCount { get; set; }
        public int IncorrectAnswersCount { get; set; }
        public bool IsSuccess { get; set; }
    }
}
