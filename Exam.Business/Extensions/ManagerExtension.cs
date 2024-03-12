using Exam.Business.Managers;
using Exam.Business.Managers.IManagers;
using Microsoft.Extensions.DependencyInjection;

namespace Exam.Business.Extensions
{
    public static class ManagerExtension
    {
        public static IServiceCollection AddManagerService(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticateManager, AuthenticateManager>();
            services.AddScoped<IExamCategoryManager, ExamCategoryManager>();
            services.AddScoped<IExaminationManager, ExaminationManager>();
            services.AddScoped<IQuestionManager, QuestionManager>();

            return services;
        }
    }
}
