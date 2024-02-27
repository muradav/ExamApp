using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Entities.Models
{
    public class Answer : BaseEntity
    {
        public string AnswerKey { get; set; }
        public string AnswerContent { get; set; }
        public string IsCorrect { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
