using System.Reflection;
using Application.Common.Behaviours;
using Application.Photos.Commands.Create;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestAuthenticUserBehaviour<,>));

            services.AddTransient(typeof(CreatePhotoCommand));


            return services;
        }
    }
}
