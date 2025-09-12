using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExlaqiNasiri.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IOtpService, OtpService>();
            return services;
        }
    }
}
