using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Dto.Dtos.ExaminationDto
{
    public class ExaminationResponseDto
    {
        public int ExamCategoryId { get; set; }
        public List<ExamQuestionDto> Questions { get; set; }
    }

    public  class ExamQuestionDto
    {
        public string Content { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
    }
}
