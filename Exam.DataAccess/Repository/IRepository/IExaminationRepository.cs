using Exam.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccess.Repository.IRepository
{
    public interface IExaminationRepository
    {
        Task<List<Examination>> GetAll(Expression<Func<Examination, bool>> filter = null, Func<IQueryable<Examination>, IQueryable<Examination>> includePredicate = null, bool tracked = true);
        Task<Examination> GetOne(Expression<Func<Examination, bool>> filter = null, bool tracked = true); 
        Task<Examination> GetOneWithInclude(Expression<Func<Examination, bool>> filter = null,
                    Func<IQueryable<Examination>, IQueryable<Examination>> includePredicate = null, bool tracked = true);
        Task Add(Examination entity);
        Task<bool> Update(Examination entity);
        bool Remove(Examination entity);
        Task SaveAsync();
    }
}
