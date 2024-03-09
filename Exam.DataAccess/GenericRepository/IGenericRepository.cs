using Exam.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccess.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, 
                    Func<IQueryable<T>, IQueryable<T>> include = null, bool tracked = true);
        Task<T> GetOneAsync(Expression<Func<T, bool>> filter = null,
                    Func<IQueryable<T>, IQueryable<T>> include = null, bool tracked = true);
        Task<List<T>> GetRandomAsync(int take = 0, Expression<Func<T, bool>> filter = null, bool tracked = true);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entity);
        bool Update(T entity);
        bool Remove(T entity);
    }
}
