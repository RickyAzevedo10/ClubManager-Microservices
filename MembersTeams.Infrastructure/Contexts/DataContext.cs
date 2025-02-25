﻿using MembersTeams.Domain.Entities;
using MembersTeams.Infrastructure.Configuration;
using MembersTeams.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace MembersTeams.Infra.Contexts
{
    //public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    public class DataContext : DbContext
    {
        private readonly FakerService _fakerService;

        public DataContext(DbContextOptions<DataContext> options, FakerService fakerService) : base(options)
        {
            _fakerService = fakerService;
        }

        //MembersTeams
        public DbSet<Player> Player { get; set; }
        public DbSet<PlayerCategory> PlayerCategory { get; set; }
        public DbSet<PlayerPerformanceHistory> PlayerPerformanceHistory { get; set; }
        public DbSet<ClubMember> ClubMember { get; set; }
        public DbSet<MinorClubMember> MinorClubMember { get; set; }
        public DbSet<PlayerResponsible> PlayerResponsible { get; set; }
        public DbSet<PlayerContract> PlayerContract { get; set; }
        public DbSet<PlayerTransfer> PlayerTransfer { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamCategory> TeamCategory { get; set; }
        public DbSet<TeamPlayer> TeamPlayer { get; set; }
        public DbSet<TeamCoach> TeamCoach { get; set; }
        public DbSet<UserClubMember> UserClubMembers { get; set; }
        public DbSet<Institution> Institution { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Gerar 1 instituição
            var institutions = _fakerService.GenerateInstitution(1);
            builder.Entity<Institution>().HasData(institutions.ToArray());

            //MembersTeams
            builder.ApplyConfiguration(new TeamCategoryConfiguration());
            builder.ApplyConfiguration(new TeamCoachConfiguration());
            builder.ApplyConfiguration(new ClubMemberConfiguration());
            builder.ApplyConfiguration(new UserClubMemberConfiguration());
            builder.ApplyConfiguration(new MinorClubMemberConfiguration());
            builder.ApplyConfiguration(new PlayerCategoryConfiguration());
            builder.ApplyConfiguration(new TeamPlayerConfiguration());
            builder.ApplyConfiguration(new PlayerContractConfiguration());
            builder.ApplyConfiguration(new PlayerConfiguration());
            builder.ApplyConfiguration(new PlayerTransferConfiguration());
        }
    }
}
