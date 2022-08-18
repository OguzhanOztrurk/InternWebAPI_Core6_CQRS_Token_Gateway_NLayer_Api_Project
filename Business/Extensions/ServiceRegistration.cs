



using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;

using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

using DataAccess.Abstract;
using DataAccess.Concrete.Repository;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using Configuration = Castle.Windsor.Installer.Configuration;

namespace Business.Extensions
{
    public static class ServiceRegistration
    {
        
        public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("connectionStr"),
                    sqlOptions =>
                    {
                        sqlOptions
                            .EnableRetryOnFailure(
                                maxRetryCount: 1,
                                maxRetryDelay: TimeSpan.FromSeconds(10),
                                errorNumbersToAdd: null);
                    });
                // options.EnableSensitiveDataLogging();
                // options.EnableDetailedErrors();
            }, ServiceLifetime.Transient, ServiceLifetime.Singleton);
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services

                .AddTransient<ICurrentRepository, CurrentRepository>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IAdminRepository, AdminRepository>()
                .AddTransient<IWorkplaceRepository,WorkplaceRepository>()
                .AddTransient<IAdvertRepository, AdvertRepository>()
                .AddTransient<IAdvertDetailRepository,AdvertDetailRepository>()
                .AddTransient<IInternRepository,InternRepository>()
                .AddTransient<IAppealRepository,AppealRepository>()
                .AddTransient<IEducationRepository,EducationRepository>()
                .AddTransient<ITalentRepository,TalentRepository>()
                ;
            
        }
        public static void AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly())
                 .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection CustomErrorException(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddProblemDetails(opt =>
            {

                opt.IncludeExceptionDetails = (con, action) => false;

                opt.Map<InvalidOperationException>(exp => new ProblemDetails

                {

                    
                    
                    Title = "Message",

                    Status = StatusCodes.Status500InternalServerError,

                    Detail = exp.Message,


                });
            });
        }
        
        
    }
}
