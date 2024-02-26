using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Dto.Dtos.ExaminationDto
{
    public class CheckExamRequestDto
    {
        public int ExaminationId { get; set; }
        public List<QuestionAnswerPair> Results { get; set; }
    }

    public class QuestionAnswerPair
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
