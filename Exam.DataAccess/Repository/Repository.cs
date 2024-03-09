using Exam.DataAccess.Data;
using Exam.DataAccess.GenericRepository;
using Exam.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccess.Repository
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository { public AnswerRepository(ApplicationDbContext context) : base(context) { } }
    public class ExamCategoryRepository : GenericRepository<ExamCategory>, IExamCategoryRepository { public ExamCategoryRepository(ApplicationDbContext context) : base(context) { } }
    public class ExaminationDetailRepository : GenericRepository<ExaminationDetail>, IExaminationDetailRepository { public ExaminationDetailRepository(ApplicationDbContext context) : base(context) { } }
    public class ExaminationRepository : GenericRepository<Examination>, IExaminationRepository { public ExaminationRepository(ApplicationDbContext context) : base(context) { } }
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository { public QuestionRepository(ApplicationDbContext context) : base(context) { } }
}
