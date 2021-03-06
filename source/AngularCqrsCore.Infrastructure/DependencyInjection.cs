﻿using Application.Common.Interfaces;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;


namespace Persistence
{
    public static class DependencyInjection 
    {

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.AddDbContext<AngularCoreContext>(options => options.UseSqlServer(configuration["ConnectionString"]));

            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IApplicationDbContext, AngularCoreContext>();

            return services;
        }
    }
}
