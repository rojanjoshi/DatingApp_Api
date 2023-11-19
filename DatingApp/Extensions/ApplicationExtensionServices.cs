using DatingApp.Db.Interface;
using DatingApp.Db.Services;
using DatingApp.Db;
using Microsoft.EntityFrameworkCore;
using DatingApp.Db.Repository.IRepository;
using DatingApp.Db.Repository;
using DatingApp.Helpers;
using DatingApp.Interface;
using DatingApp.Service;

namespace DatingApp.Extensions
{
    public static class ApplicationExtensionServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddCors();
            return services;
        }
    }
}
