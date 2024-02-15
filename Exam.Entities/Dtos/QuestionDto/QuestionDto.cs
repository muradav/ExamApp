using Exam.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Entities.Dtos.QuestionDto
{
    public class QuestionDto
    {
        public IFormFile ExcelFile { get; set; }
        public int ExamCategoryId { get; set; }
    }

}
