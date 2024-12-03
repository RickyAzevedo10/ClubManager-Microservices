using Financial.API.ActionFilters;
using Financial.App.Services.Infrastructures;
using Financial.Application.Interfaces.Infrastructure;
using Financial.Application.Services;
using Financial.Domain.DTOs;
using Financial.Domain.Entities.Validators;
using Financial.Domain.Interfaces;
using Financial.Domain.Interfaces.Financial;
using Financial.Domain.Interfaces.Identity;
using Financial.Domain.Interfaces.Repositories;
using Financial.Domain.Services;
using Financial.Domain.Services.Identity;
using Financial.Infra.Contexts;
using Financial.Infra.Persistence;
using Financial.Infra.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Financial.Ioc
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
            //Financial
            services.AddScoped<IEntityAppService, EntityAppService>();
            services.AddScoped<IExpenseAppService, ExpenseAppService>();
            services.AddScoped<IRevenueAppService, RevenueAppService>();

            #endregion

            #region Validators
            // Add Domain Validators
            services.AddScoped<IValidator<ExpenseDTO>, ExpenseValidator>();
            services.AddScoped<IValidator<RevenueDTO>, RevenueValidator>();
            services.AddScoped<IValidator<CreateEntityDTO>, EntityValidator>();
            #endregion

            #region Add Domain Services dependencies
            //Financial
            services.AddScoped<IEntityService, EntityService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IRevenueService, RevenueService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMembersService, MemberService>();
            services.AddScoped<IPlayerService, PlayerService>();
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
