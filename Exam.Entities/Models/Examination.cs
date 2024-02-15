using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Entities.Models
{
    public class Examination : BaseEntity
    {
        public string Examiner { get; set; }
        public int RequestCount { get; set; }
        public int Point { get; set; }

        public int ExamCategoryId { get; set; }
        public ExamCategory ExamCategory { get; set; }

        public List<Quiz> Quizzes { get; set; }


    }
}
