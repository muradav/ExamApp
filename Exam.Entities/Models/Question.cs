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
        public string ImageUrl { get; set; }
        public string ExcelUrl { get; set; }

        public int ExamCategoryId { get; set; }
        public ExamCategory ExamCategory { get; set; }

        public List<Answer> Answers { get; set; }

        public List<Examination> Examinations { get; set; }

        public List<ExaminationDetail> ExaminationDetails { get; set; }
    }
}
