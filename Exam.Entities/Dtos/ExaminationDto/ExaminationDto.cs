using Exam.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Entities.Dtos.ExaminationDto
{
    public class ExaminationDto
    {
        public string Examiner { get; set; }
        public int RequestCount { get; set; }
        public int ExamCategoryId { get; set; }

    }
}
