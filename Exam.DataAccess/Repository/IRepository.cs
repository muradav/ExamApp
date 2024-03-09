using Exam.DataAccess.GenericRepository;
using Exam.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccess.Repository
{
    public interface IAnswerRepository : IGenericRepository<Answer> { }
    public interface IExamCategoryRepository : IGenericRepository<ExamCategory> { }
    public interface IExaminationDetailRepository : IGenericRepository<ExaminationDetail> { }
    public interface IExaminationRepository : IGenericRepository<Examination> { }
    public interface IQuestionRepository : IGenericRepository<Question> { }
}
