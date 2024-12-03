using FluentValidation;
using MembersTeams.Domain.Services.Identity;
using Microsoft.EntityFrameworkCore;
using TrainingCompetition.API.ActionFilters;
using TrainingCompetition.Application.Interfaces;
using TrainingCompetition.Application.Interfaces.Infrastructure;
using TrainingCompetition.Application.Services;
using TrainingCompetition.Domain.DTOs;
using TrainingCompetition.Domain.Entities.Validators;
using TrainingCompetition.Domain.Interfaces;
using TrainingCompetition.Domain.Interfaces.Identity;
using TrainingCompetition.Domain.Interfaces.Repositories;
using TrainingCompetition.Domain.Services;
using TrainingCompetition.Domain.Services.Identity;
using TrainingCompetition.Infra.Contexts;
using TrainingCompetition.Infra.Persistence;
using TrainingCompetition.Infra.Services;

namespace TrainingCompetition.Ioc
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

            //TrainingCompetition
            services.AddScoped<IMatchAppService, MatchAppService>();
            services.AddScoped<ITrainingAppService, TrainingAppService>();

            #endregion

            #region Validators
            // Add Domain Validators

            services.AddScoped<IValidator<CreateMatchDTO>, MatchValidator>();
            services.AddScoped<IValidator<CreateMatchStatisticDTO>, MatchStatisticValidator>();
            services.AddScoped<IValidator<CreateTrainingSessionDTO>, TrainingSessionValidator>();
            services.AddScoped<IValidator<CreateTrainingAttendanceDTO>, TrainingAttendanceValidator>();

            #endregion

            #region Add Domain Services dependencies
            //TrainingCompetition
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<ITrainingService, TrainingService>();
            services.AddScoped<ITeamService, TeamService>();
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
