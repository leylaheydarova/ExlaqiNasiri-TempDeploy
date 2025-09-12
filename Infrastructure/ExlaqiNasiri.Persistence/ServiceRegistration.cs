using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Repositories.Articles;
using ExlaqiNasiri.Application.Repositories.HadithCategories;
using ExlaqiNasiri.Application.Repositories.Hadiths;
using ExlaqiNasiri.Application.Repositories.LessonFields;
using ExlaqiNasiri.Application.Repositories.Lessons;
using ExlaqiNasiri.Application.Repositories.News;
using ExlaqiNasiri.Domain.Entities.BaseEntities;
using ExlaqiNasiri.Persistence.Context;
using ExlaqiNasiri.Persistence.Repositories.Articles;
using ExlaqiNasiri.Persistence.Repositories.HadithCategories;
using ExlaqiNasiri.Persistence.Repositories.Hadiths;
using ExlaqiNasiri.Persistence.Repositories.LessonFields;
using ExlaqiNasiri.Persistence.Repositories.Lessons;
using ExlaqiNasiri.Persistence.Repositories.News;
using ExlaqiNasiri.Persistence.Services;
using ExlaqiNasiri.Persistence.Services.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExlaqiNasiri.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExlaqiNasiriDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default"))); // PostgreSQL

            services.AddIdentity<BaseUser, IdentityRole>(options =>
            {
                //Password ucun mumkun duzgunlukler
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 2;

                //Lockout
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

                //Signin
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                //User
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ExlaqiNasiriDbContext>()
                .AddDefaultTokenProviders();

            //Repositories
            services.AddScoped<IHadithCategoryReadRepository, HadithCategoryReadRepository>();
            services.AddScoped<IHadithCategoryWriteRepository, HadithCategoryWriteRepository>();
            services.AddScoped<IHadithReadRepository, HadithReadRepository>();
            services.AddScoped<IHadithWriteRepository, HadithWriteRepository>();
            services.AddScoped<ILessonFieldReadRepository, LessonFieldReadRepository>();
            services.AddScoped<ILessonFieldWriteRepository, LessonFieldWriteRepository>();
            services.AddScoped<ILessonWriteRepository, LessonWriteRepository>();
            services.AddScoped<ILessonReadRepository, LessonReadRepository>();
            services.AddScoped<IWebNewsReadRepository, WebNewsReadRepository>();
            services.AddScoped<IWebNewsWriteRepository, WebNewsWriteRepository>();
            services.AddScoped<IArticleReadRepository, ArticleReadRepository>();
            services.AddScoped<IArticleWriteRepository, ArticleWriteRepository>();

            //Services
            services.AddScoped<IHadithService, HadithService>();
            services.AddScoped<IHadithCategoryService, HadithCategoryService>();
            services.AddScoped<ILessonFieldService, LessonFieldService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IWebUserService, WebUserService>();
            services.AddScoped<IWebNewsService, WebNewsService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService,  UserService>();


            //Helper Services
            services.AddScoped<IGetEntityService, GetEntityService>();
            services.AddScoped<IFileUpload, FileUpload>();
            services.AddScoped<ISendMailService, SendMailService>();
            services.AddScoped<IGenerateJWTTokenService, GenerateJWTTokenService>();

            //Utils
            services.AddMemoryCache();
            return services;
        }
    }
}
