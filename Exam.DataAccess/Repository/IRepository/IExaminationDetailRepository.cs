using Exam.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccess.Repository.IRepository
{
    public interface IExaminationDetailRepository
    {
        Task<List<ExaminationDetail>> GetAll(Expression<Func<ExaminationDetail, bool>> filter = null, bool tracked = true);
        Task<ExaminationDetail> GetOne(Expression<Func<ExaminationDetail, bool>> filter = null, bool tracked = true);
        Task Add(ExaminationDetail entity);
        Task AddRange(List<ExaminationDetail> entity);
        Task<bool> Update(ExaminationDetail entity);
        bool Remove(ExaminationDetail entity);
        Task SaveAsync();
    }
}
