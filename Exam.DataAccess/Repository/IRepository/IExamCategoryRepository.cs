using Exam.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccess.Repository.IRepository
{
    public interface IExamCategoryRepository
    {
        Task<List<ExamCategory>> GetAll(Expression<Func<ExamCategory, bool>> filter = null, bool tracked = true);
        Task<ExamCategory> GetOne(Expression<Func<ExamCategory, bool>> filter = null, bool tracked = true);
        Task Add(ExamCategory entity);
        Task<bool> Update(ExamCategory entity);
        bool Remove(ExamCategory entity);
        Task SaveAsync();
    }
}
