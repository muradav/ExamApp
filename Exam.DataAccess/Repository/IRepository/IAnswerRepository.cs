using Exam.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccess.Repository.IRepository
{
    public interface IAnswerRepository
    {
        Task<List<Answer>> GetAll(Expression<Func<Answer, bool>> filter = null, bool tracked = true);
        Task<Answer> GetOne(Expression<Func<Answer, bool>> filter = null, bool tracked = true);
        Task Add(Answer entity);
        Task AddRange(List<Answer> entity);
        Task<bool> Update(Answer entity);
        bool Remove(Answer entity);
        Task SaveAsync();
    }
}
