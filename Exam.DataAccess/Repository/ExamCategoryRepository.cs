using Exam.DataAccess.Data;
using Exam.DataAccess.Repository.IRepository;
using Exam.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Exam.DataAccess.Repository
{
    public class ExamCategoryRepository : IExamCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public ExamCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(ExamCategory entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task<bool> Update(ExamCategory entity)
        {
            _db.Update(entity);

            return await Task.FromResult(true);
        }

        public async Task<List<ExamCategory>> GetAll(Expression<Func<ExamCategory, bool>> filter = null, bool tracked = true)
        {
            IQueryable<ExamCategory> query = _db.ExamCategories;

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

        public async Task<ExamCategory> GetOne(Expression<Func<ExamCategory, bool>> filter = null, bool tracked = true)
        {
            IQueryable<ExamCategory> query = _db.ExamCategories;

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

        public bool Remove(ExamCategory entity)
        {
            _db.ExamCategories.Remove(entity);
            return true;
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

    }
}
