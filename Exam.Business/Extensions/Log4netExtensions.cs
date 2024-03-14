﻿using log4net;
using log4net.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Exam.Business.Extensions
{
    public static class Log4netExtensions
    {
        public static void AddLog4net<T>(this IServiceCollection services)
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            services.AddSingleton(LogManager.GetLogger(typeof(T)));
        }
    }
}
