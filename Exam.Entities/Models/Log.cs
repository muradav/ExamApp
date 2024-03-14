using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Entities.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime LogDate { get; set; }
        public string Thread { get; set; }
        public string Logger { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
