using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Dto.Dtos.ExaminationDto
{
    public class CheckExamResponseDto
    {
        public int CorrectAnswersCount { get; set; }
        public int IncorrectAnswersCount { get; set; }
        public bool IsSuccess { get; set; }
        public string ExaminerId { get; set; }
        public List<CheckResponsePair> Details { get; set; }

        public CheckExamResponseDto(string examinerId)
        {
            Details = new();
            IsSuccess = CorrectAnswersCount - IncorrectAnswersCount >=1 ;
            ExaminerId = examinerId;
        }
    }

    public class CheckResponsePair
    {
        public string Question { get; set; }
        public string ExaminerAnswer { get; set; }
        public string CorrectAnswer { get; set; }
        public bool IsSuccess { get; set; }

        public CheckResponsePair(string question, string examinerAnswer, string correctAnswer)
        {
            ExaminerAnswer = examinerAnswer;
            CorrectAnswer = correctAnswer;
            Question = question;
            IsSuccess = examinerAnswer == correctAnswer;
        }
    }
}
