using Financial.Infra.Contexts;

namespace Financial.Infrastructure.Services
{
    public class SeedData
    {
        public static async Task SeedAsync(DataContext context, FakerService fakeService)
        {
            // Verificar se os dados já existem
            if (context.User.Any())
            {
                return; 
            }

            var users = fakeService.GenerateUsers(15); 
            await context.User.AddRangeAsync(users);
            await context.SaveChangesAsync();

            var guardians = fakeService.GenerateFakeClubMembers(6);
            await context.ClubMembers.AddRangeAsync(guardians);
            await context.SaveChangesAsync();

            var players = fakeService.GenerateFakePlayers(10);
            await context.Player.AddRangeAsync(players);
            await context.SaveChangesAsync();

            var entities = fakeService.GenerateFakeEntities(guardians, players, 10);
            await context.Entities.AddRangeAsync(entities);
            await context.SaveChangesAsync();

            var expenses = fakeService.GenerateFakeExpenses(entities, users, 10);
            await context.Expenses.AddRangeAsync(expenses);
            await context.SaveChangesAsync();

            var revenues = fakeService.GenerateFakeRevenues(entities, users, 10);
            await context.Revenues.AddRangeAsync(revenues);
            await context.SaveChangesAsync();
        }
    }
}
