using Exam.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccess.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IAnswerRepository Answer { get; }
        IExamCategoryRepository ExamCategory { get; }
        IExaminationDetailRepository ExaminationDetail { get; }
        IExaminationRepository Examination { get; }
        IQuestionRepository Question { get; }

        Task<int> SaveAsync();
    }
}
