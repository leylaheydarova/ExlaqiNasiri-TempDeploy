using ExlaqiNasiri.Application.Validation.HadithCategory;
using ExlaqiNasiri.Application.Validation.LessonField;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ExlaqiNasiri.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(ServiceRegistration));
            services.AddMediatR(s => s.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssemblyContaining<HadithCategoryCommandDTOValidation>();
            services.AddValidatorsFromAssemblyContaining<LessonFieldCreateDTOValidation>();
            return services;
        }
    }
}
