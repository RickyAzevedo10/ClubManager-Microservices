using Financial.Domain.Entities;
using Financial.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Financial.Infra.Contexts
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        //Financial
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<RevenueCategory> RevenueCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<ClubMember> ClubMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Financial
            builder.ApplyConfiguration(new RevenueCategoryConfiguration());
            builder.ApplyConfiguration(new ExpenseCategoryConfiguration());
            builder.ApplyConfiguration(new ExpenseConfiguration());
            builder.ApplyConfiguration(new RevenueConfiguration());
        }
    }
}
