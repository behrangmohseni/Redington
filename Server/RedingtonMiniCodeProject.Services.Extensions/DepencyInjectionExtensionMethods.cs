using Microsoft.Extensions.DependencyInjection;
using RedingtonMiniCodeProject.Services.Abstraction;
using RedingtonMiniCodeProject.Services.Models;

namespace RedingtonMiniCodeProject.Services.Extensions
{
    public static class DepencyInjectionExtensionMethods
    {
        public static void AddCalculationServices(this IServiceCollection services)
        {
            services.AddSingleton<ICalculationServiceFactory, CalculationServiceFactory>();
        }

        public static void AddCalculationLookupService(this IServiceCollection services)
        {
            services.AddSingleton<ICalculationLookupService, CalculationLookupService>();
        }

        public static void AddFileLoggingService(this IServiceCollection services, FileLoggerProperties properties)
        {
            services.AddSingleton<ILoggingFormatter<LoggingInformation>, LoggingFormatter>();
            services.AddSingleton(properties);
            services.AddSingleton<ILoggingWriter<LoggingInformation, ILoggingFormatter<LoggingInformation>>, FileLoggingWriter<LoggingInformation>>();
            services.AddSingleton<ILoggingService<LoggingInformation>, FileLoggingService>();
        }

    }
}
