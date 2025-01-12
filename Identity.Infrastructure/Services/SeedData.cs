using Identity.Infra.Contexts;

namespace Identity.Infrastructure.Services
{
    public class SeedData
    {
        public static async Task SeedAsync(DataContext context, FakerService fakeService)
        {
            // Verificar se os dados já existem
            if (context.User.Any() || context.UserPermissions.Any())
            {
                return; 
            }

            var institutions = context.Institution;

            var userPermissions = fakeService.GenerateUserPermissions(15);
            await context.UserPermissions.AddRangeAsync(userPermissions);
            await context.SaveChangesAsync();

            var users = fakeService.GenerateUsers(15, institutions.First(), userPermissions); 
            await context.User.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
}
