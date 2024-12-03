using FluentValidation;
using Infrastructures.API.ActionFilters;
using Infrastructures.Application.Interfaces;
using Infrastructures.Application.Services;
using Infrastructures.Application.Services.Infrastructure;
using Infrastructures.Domain.DTOs;
using Infrastructures.Domain.Entities.Validators;
using Infrastructures.Domain.Interfaces;
using Infrastructures.Domain.Interfaces.Financial;
using Infrastructures.Domain.Interfaces.Infrastructures;
using Infrastructures.Domain.Interfaces.Repositories;
using Infrastructures.Domain.Services;
using Infrastructures.Domain.Services.Infrastructures;
using Infrastructures.Infra.Contexts;
using Infrastructures.Infra.Persistence;
using Infrastructures.Infra.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Ioc
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

            #region Add App Services dependencies 
            //Infrastrucutres
            services.AddScoped<IFacilityAppService, FacilityAppService>();
            services.AddScoped<IMaintenanceAppService, MaintenanceAppService>();

            #endregion

            #region Validators
            // Add Domain Validators
            services.AddScoped<IValidator<CreateFacilityDTO>, FacilityValidator>();
            services.AddScoped<IValidator<CreateFacilityReservationDTO>, FacilityReservationValidator>();
            services.AddScoped<IValidator<CreateMaintenanceRequestDTO>, FacilityMaintenanceRequestValidator>();
            #endregion

            #region Add Domain Services dependencies
            //Infrastructures
            services.AddScoped<IFacilityService, FacilityService>();
            services.AddScoped<IMaintenanceService, MaintenanceService>();
            services.AddScoped<IUserService, UserService>();
            #endregion

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
