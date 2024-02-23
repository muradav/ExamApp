using Exam.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccess.Repository.IRepository
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetAll(Expression<Func<Question, bool>> filter = null, bool tracked = true);
        Task<Question> GetOne(Expression<Func<Question, bool>> filter = null, bool tracked = true);
        Task Add(Question entity);
        Task<bool> Update(Question entity);
        bool Remove(Question entity);
        Task SaveAsync();
    }
}
