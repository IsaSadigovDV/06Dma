using Api006.Core.Repositories.Abstractions;
using Api006.Data.Context;
using Api006.Data.Repositories.Concrets;
using Api006.Service.Mappings;
using Api006.Service.Services.Abstractions;
using Api006.Service.Services.Concrets;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api006.App.ServiceRegistrations
{
    public static class ServiceRegister
    {
        public static IServiceCollection RegisterSericesAndRepos(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Default"));
            });

            services.AddAutoMapper(typeof(CategoryMap));
            services.AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining<CategoryMap>());



            //repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            //services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
