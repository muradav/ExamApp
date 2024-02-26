using Exam.DataAccess.Data;
using Exam.DataAccess.Repository.IRepository;
using Exam.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccess.Repository
{
    public class ExaminationRepository : IExaminationRepository
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<Examination> dbSet;

        public ExaminationRepository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Examinations;
        }

        public async Task Add(Examination entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<List<Examination>> GetAll(Expression<Func<Examination, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Examination> query = dbSet;

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

        public async Task<Examination> GetOne(Expression<Func<Examination, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Examination> query = dbSet;

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

        public async Task<Examination> GetOneWithInclude(Expression<Func<Examination, bool>> filter = null, Func<IQueryable<Examination>, IQueryable<Examination>> includePredicate = null, bool tracked = true)
        {
            IQueryable<Examination> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (includePredicate != null)
            {
                query = includePredicate(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public bool Remove(Examination entity)
        {
            _db.Examinations.Remove(entity);
            return true;
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Update(Examination entity)
        {
            _db.Update(entity);

            return await Task.FromResult(true);
        }
    }
}
