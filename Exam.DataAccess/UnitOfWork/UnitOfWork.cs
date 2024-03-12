using Exam.DataAccess.Data;
using Exam.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext _db { get; set; }

        public IAnswerRepository Answer { get; private set; }
        public IExamCategoryRepository ExamCategory { get; private set; }
        public IExaminationDetailRepository ExaminationDetail { get; private set; }
        public IExaminationRepository Examination { get; private set; }
        public IQuestionRepository Question { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Answer = new AnswerRepository(db);
            ExamCategory = new ExamCategoryRepository(db);
            ExaminationDetail = new ExaminationDetailRepository(db);
            Examination = new ExaminationRepository(db);
            Question = new QuestionRepository(db);
        }

        public async Task<int> SaveAsync()
            => await _db.SaveChangesAsync();
    }
}

