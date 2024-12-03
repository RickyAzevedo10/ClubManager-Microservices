using Infrastructures.Domain.Entities;
using Infrastructures.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Infra.Contexts
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        

        //Infrastructures
        public DbSet<FacilityCategory> FacilityCategories { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityReservation> FacilityReservations { get; set; }
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
        public DbSet<MaintenanceHistory> MaintenanceHistories { get; set; }
        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Infrastructures
            builder.ApplyConfiguration(new FacilityCategoryConfiguration());

        }
    }
}
