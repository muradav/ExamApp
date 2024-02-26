using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Entities.Models
{
    public class Examination : BaseEntity
    {
        public int RequestCount { get; set; }
        public int CorrectAnswersCount { get; set; }
        public int Point { get; set; }
        public bool IsSuccess { get; set; }

        [ForeignKey("AppUser")]
        public string ExaminerId { get; set; }
        public AppUser AppUser { get; set; }

        public int ExamCategoryId { get; set; }
        public ExamCategory ExamCategory { get; set; }

        public List<Question> Questions { get; set; }

    }
}
