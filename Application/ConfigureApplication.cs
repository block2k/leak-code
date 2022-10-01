using Application.Services;
using Azure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using System.Text;

namespace Application
{
    public static class ConfigureApplication
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<UnitOfWork>();
            services.AddScoped<StudentService>();
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddAzureClients(x =>
            {
                x.AddBlobServiceClient(configuration.GetValue<string>("Azure:ConnectionStrings"));
            });
            services.AddScoped<AzureServices>();


            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("nextfapdb"));
            }
            else
            {
                services.AddDbContext<AppDbContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("sqlserver"), b => b.MigrationsAssembly("WebApi")));
            }

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        public static void LoadDotenv(this IConfigurationBuilder builder ,string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            foreach(var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split("=", StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2) continue;
            }
        }
    }
}