using FluentValidation;
using Identity.API.ActionFilters;
using Identity.App.Interfaces.Identity;
using Identity.App.Interfaces.Infrastructure;
using Identity.Application.Interfaces.Producers;
using Identity.Application.Producers;
using Identity.Application.Services;
using Identity.Domain.DTOs;
using Identity.Domain.Entities.Validators;
using Identity.Domain.Interfaces;
using Identity.Domain.Interfaces.Identity;
using Identity.Domain.Interfaces.Repositories;
using Identity.Domain.Services;
using Identity.Domain.Services.Identity;
using Identity.Infra.Contexts;
using Identity.Infra.Persistence;
using Identity.Infra.Services;
using Microsoft.EntityFrameworkCore;

namespace Identity.Ioc
{
    public static class DIConfiguration
    {
        public static void AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            // Add UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add Model Errors
            services.AddScoped<IModelErrorsContext, ModelErrorsContext>();
            services.AddScoped<IEntityValidationService, EntityValidationService>();
            //Add notification context
            services.AddScoped<INotificationContext, NotificationContext>();

            //Add Infra Services dependencies
            services.AddScoped<IUserClaimsService, UserClaimsService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            #region Add App Services dependencies 
            //Identity
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IInstitutionAppService, InstitutionAppService>();

            #endregion

            #region Validators
            // Add Domain Validators
            services.AddScoped<IValidator<CreateInstitutionDTO>, InstitutionValidator>();
            services.AddScoped<IValidator<CreateUserDTO>, CreateUserValidator>();
            services.AddScoped<IValidator<UpdateUserDTO>, UpdateUserValidator>();
            services.AddScoped<IValidator<CreateUserPermissionsDTO>, CreateUserPermissionsValidator>();
            services.AddScoped<IValidator<UpdateUserPermissionsDTO>, UpdateUserPermissionsValidator>();
            services.AddScoped<IValidator<ResetPasswordDTO>, PasswordValidator>();
            #endregion

            #region Add Domain Services dependencies

            //Identity
            services.AddScoped<IInstitutionService, InstitutionService>();
            services.AddScoped<IUserService, UserService>();

            #endregion

            // Producers Services
            services.AddScoped<IUserProducer, UserProducer>();
            services.AddScoped<IInstitutionProducer, InstitutionProducer>();

            // Add data access dependencies
            var sql_conn = configuration.GetConnectionString("DefaultDockerConnection");
            services.AddDbContext<DataContext>(options => options.UseSqlServer(sql_conn));

            // Add services to the container.
            services.AddControllers(options => 
            {
                options.Filters.Add<NotificationFilter>();
                options.Filters.Add<ModelErrorFilter>();
            });
        }
    }
}
