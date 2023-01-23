using CloudinaryDotNet;
using DatingApp.Data;
using DatingApp.Helpers;
using DatingApp.Interface;
using DatingApp.Services;
using DatingApp.SignalR;
using dotenv.net;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSingleton<PresenceTracker>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<LogUserActivity>();
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            Cloudinary cloudinary = new(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
            services.AddScoped<IPhotoService, PhotoService>();
            cloudinary.Api.Secure = true;
            services.AddDbContext<DataContext>(opt =>
            {
                string dbUrl = Environment.GetEnvironmentVariable("DB_URL");
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
                opt.UseMySql(dbUrl, ServerVersion.AutoDetect(dbUrl),
                options => options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: System.TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)
                    );
                //opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddSignalR();

            return services;
        }
    }
}
