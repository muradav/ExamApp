using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Entities.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        [MaxLength(255)]
        public string Thread { get; set; }
        [MaxLength(255)]
        public string Logger { get; set; }
        [MaxLength(50)]
        public string Level { get; set; }
        [MaxLength(4000)]
        public string Message { get; set; }
        [MaxLength(2000)]
        public string Exception { get; set; }
    }
}
