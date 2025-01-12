using Identity.Infrastructure.Services;
using Identity.Domain.Entities;
using Identity.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infra.Contexts
{
    //public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    public class DataContext : DbContext
    {
        private readonly FakerService _fakerService;

        public DataContext(DbContextOptions<DataContext> options, FakerService fakerService) : base(options)
        {
            _fakerService = fakerService;
        }

        //Identity
        public DbSet<Institution> Institution { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<UserPermissions> UserPermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Identity
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRolesConfiguration());

            // Gerar 1 instituição
            var institutions = _fakerService.GenerateInstitution(1);
            builder.Entity<Institution>().HasData(institutions.ToArray());

        }
    }
}
