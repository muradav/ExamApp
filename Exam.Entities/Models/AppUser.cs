using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Entities.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        [NotMapped]
        public string FullName { get { return $"{Name} {Surname}"; } }
    }
    public enum Roles
    {
        Admin,
        Examiner
    }
}
