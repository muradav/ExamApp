using Exam.DataAccess.Data;
using Exam.Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Business.Extensions
{
    public static class SeedExtension
    {
        public static ModelBuilder Seed(this ModelBuilder builder)
        {
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

            return builder;
        }
    }
}
