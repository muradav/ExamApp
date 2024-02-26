using Exam.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<ExamCategory> ExamCategories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<ExaminationDetail> ExaminationDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ExamCategory>().HasData(
                new ExamCategory
                {
                    Id = 1,
                    Name = "General Knowledge"
                },
                new ExamCategory
                {
                    Id = 2,
                    Name = "Mathematics"
                },
                new ExamCategory
                {
                    Id = 3,
                    Name = "History"
                }
                );
        }
    }
}
