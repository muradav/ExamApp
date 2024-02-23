using Exam.DataAccess.Data;
using Exam.DataAccess.Repository.IRepository;
using Exam.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Exam.DataAccess.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _db;

        public QuestionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(Question entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task<List<Question>> GetAll(Expression<Func<Question, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Question> query = _db.Questions;

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

        public async Task<Question> GetOne(Expression<Func<Question, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Question> query = _db.Questions;

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

        public bool Remove(Question entity)
        {
            _db.Questions.Remove(entity);
            return true;
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Update(Question entity)
        {
            _db.Update(entity);

            return await Task.FromResult(true);
        }
    }
}