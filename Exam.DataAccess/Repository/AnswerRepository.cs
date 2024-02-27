using Exam.DataAccess.Data;
using Exam.DataAccess.Repository.IRepository;
using Exam.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Exam.DataAccess.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ApplicationDbContext _db;

        public AnswerRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(Answer entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task<bool> Update(Answer entity)
        {
            _db.Update(entity);

            return await Task.FromResult(true);
        }
        public async Task AddRange(List<Answer> entity)
        {
            await _db.AddRangeAsync(entity);
        }


        public async Task<List<Answer>> GetAll(Expression<Func<Answer, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Answer> query = _db.Answers;

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

        public async Task<Answer> GetOne(Expression<Func<Answer, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Answer> query = _db.Answers;

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

        public bool Remove(Answer entity)
        {
            _db.Answers.Remove(entity);
            return true;
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

    }
}
