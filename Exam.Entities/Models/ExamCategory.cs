using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Entities.Models
{
    public class ExamCategory :BaseEntity
    {
        public string Name { get; set; }

        public List<Question> Questions { get; set; }

        public int QuestionCount { get; set; } = 0;
    }
}
