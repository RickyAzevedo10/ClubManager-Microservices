using Infrastructures.Infra.Contexts;

namespace Infrastructures.Infrastructure.Services
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

            // Gerar Facilities
            var facilities = fakeService.GenerateFacilities(5);
            await context.Facilities.AddRangeAsync(facilities);
            await context.SaveChangesAsync();

            // Gerar Facility Reservations
            var reservations = fakeService.GenerateFacilityReservations(10, facilities, users);
            await context.FacilityReservations.AddRangeAsync(reservations);
            await context.SaveChangesAsync();

            // Gerar Maintenance Histories
            var histories = fakeService.GenerateMaintenanceHistories(8, facilities, users);
            await context.MaintenanceHistories.AddRangeAsync(histories);
            await context.SaveChangesAsync();

            // Gerar Maintenance Requests
            var requests = fakeService.GenerateMaintenanceRequests(12, facilities, users);
            await context.MaintenanceRequests.AddRangeAsync(requests);
            await context.SaveChangesAsync();
        }
    }
}
