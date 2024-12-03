using Microsoft.EntityFrameworkCore;
using TrainingCompetition.Domain.Entities;

namespace TrainingCompetition.Infra.Contexts
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {

        //TrainingCompetition
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchStatistic> MatchStatistics { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }
        public DbSet<TrainingAttendance> TrainingAttendances { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
