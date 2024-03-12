using Exam.DataAccess.DbInitializers;
using Exam.DataAccess.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace Exam.DataAccess.Extensions
{
    public static class DbExtensions
    {
        public static IServiceCollection DbServices(this IServiceCollection services)
        {
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
