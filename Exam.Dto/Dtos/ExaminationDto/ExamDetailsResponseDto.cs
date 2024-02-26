using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Dto.Dtos.ExaminationDto
{
    public class ExamDetailsResponseDto
    {
        public string Answer { get; set; }
        public bool isCorrect { get; set; }
        public int ExaminationId { get; set; }
        public int QuestionId { get; set; }
    }
}
