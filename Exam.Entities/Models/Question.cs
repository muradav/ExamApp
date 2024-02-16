using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Entities.Models
{
    public class Question : BaseEntity
    {
        public string Content { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectOption { get; set; }

        public string ExcelUrl { get; set; }

        [NotMapped]
        public IFormFile ExcelFile { get; set; }

        public int ExamCategoryId { get; set; }
        public ExamCategory ExamCategory { get; set; }

        public List<Quiz> Quizzes { get; set; }
    }
}
