using FluentValidation;
using MembersTeams.API.ActionFilters;
using MembersTeams.Application.Interfaces;
using MembersTeams.Application.Interfaces.Infrastructure;
using MembersTeams.Application.Interfaces.Producers;
using MembersTeams.Application.Producers;
using MembersTeams.Application.Services;
using MembersTeams.Domain.DTOs;
using MembersTeams.Domain.Entities.Validators;
using MembersTeams.Domain.Interfaces;
using MembersTeams.Domain.Interfaces.Financial;
using MembersTeams.Domain.Interfaces.Identity;
using MembersTeams.Domain.Interfaces.Repositories;
using MembersTeams.Domain.Services;
using MembersTeams.Domain.Services.Identity;
using MembersTeams.Infra.Contexts;
using MembersTeams.Infra.Persistence;
using MembersTeams.Infra.Services;
using Microsoft.EntityFrameworkCore;

namespace MembersTeams.Ioc
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

            //MembersTeams
            services.AddScoped<IMembersAppService, MembersAppService>();
            services.AddScoped<IPlayerAppService, PlayerAppService>();
            services.AddScoped<ITeamAppService, TeamAppService>();


            #endregion

            services.AddScoped<IClubMemberProducer, ClubMemberProducer>();
            services.AddScoped<IPlayerProducer, PlayerProducer>();
            services.AddScoped<ITeamProducer, TeamProducer>();

            #region Validators
            // Add Domain Validators
            services.AddScoped<IValidator<CreateClubMemberDTO>, CreateClubMemberValidator>();
            services.AddScoped<IValidator<UpdateClubMemberDTO>, UpdateClubMemberValidator>();
            services.AddScoped<IValidator<CreateMinorClubMemberDTO>, CreateMinorClubMemberValidator>();
            services.AddScoped<IValidator<UpdateMinorClubMemberDTO>, UpdateMinorClubMemberValidator>();
            services.AddScoped<IValidator<CreatePlayerTransferDTO>, CreatePlayerTransferValidator>();
            services.AddScoped<IValidator<UpdatePlayerTransferDTO>, UpdatePlayerTransferValidator>();
            services.AddScoped<IValidator<CreatePlayerContractDTO>, CreatePlayerContractValidator>();
            services.AddScoped<IValidator<UpdatePlayerContractDTO>, UpdatePlayerContractValidator>();
            services.AddScoped<IValidator<CreatePlayerPerformanceHistoryDTO>, CreatePlayerPerformanceHistoryValidator>();
            services.AddScoped<IValidator<UpdatePlayerPerformanceHistoryDTO>, UpdatePlayerPerformanceHistoryValidator>();
            services.AddScoped<IValidator<CreatePlayerDTO>, CreatePlayerValidator>();
            services.AddScoped<IValidator<UpdatePlayerDTO>, UpdatePlayerValidator>();
            services.AddScoped<IValidator<CreateResponsiblePlayerDTO>, CreateResponsiblePlayerValidator>();
            services.AddScoped<IValidator<UpdateResponsiblePlayerDTO>, UpdateResponsiblePlayerValidator>();
            services.AddScoped<IValidator<CreateTeamDTO>, CreateTeamValidator>();
            services.AddScoped<IValidator<UpdateTeamDTO>, UpdateTeamValidator>();
            #endregion

            #region Add Domain Services dependencies
            //MembersTeams
            services.AddScoped<IMembersService, MemberService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IInstitutionService, InstitutionService>();
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
