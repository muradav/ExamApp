using Exam.DataAccess.Data;
using Exam.DataAccess.Repository.IRepository;
using Exam.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Exam.DataAccess.Repository
{
    public class ExaminationDetailRepository : IExaminationDetailRepository
    {
        private readonly ApplicationDbContext _db;

        public ExaminationDetailRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(ExaminationDetail entity)
        {
            await _db.AddAsync(entity);
        }
        public async Task AddRange(List<ExaminationDetail> entity)
        {
            await _db.AddRangeAsync(entity);
        }

        public async Task<bool> Update(ExaminationDetail entity)
        {
            _db.Update(entity);

            return await Task.FromResult(true);
        }

        public async Task<List<ExaminationDetail>> GetAll(Expression<Func<ExaminationDetail, bool>> filter = null, bool tracked = true)
        {
            IQueryable<ExaminationDetail> query = _db.ExaminationDetails;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<ExaminationDetail> GetOne(Expression<Func<ExaminationDetail, bool>> filter = null, bool tracked = true)
        {
            IQueryable<ExaminationDetail> query = _db.ExaminationDetails;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public bool Remove(ExaminationDetail entity)
        {
            _db.ExaminationDetails.Remove(entity);
            return true;
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
