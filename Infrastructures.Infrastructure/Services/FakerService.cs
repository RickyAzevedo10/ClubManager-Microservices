using Bogus;
using Infrastructures.Domain.Entities;

namespace Infrastructures.Infrastructure.Services
{
    public class FakerService
    {
        public List<User> GenerateUsers(int userCount)
        {
            var users = new Faker<User>()
                .RuleFor(u => u.Email, f => $"user-{Guid.NewGuid()}@example.com")
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .Generate(userCount);
            
            return users;
        }

        public List<Facility> GenerateFacilities(int facilityCount, long? categoryId = null)
        {
            var facilities = new Faker<Facility>()
                .RuleFor(f => f.Name, f => f.Commerce.ProductName())
                .RuleFor(f => f.FacilityCategoryId, f => categoryId ?? f.Random.Int(1, 6))
                .RuleFor(f => f.Location, f => f.Address.FullAddress())
                .RuleFor(f => f.Capacity, f => f.Random.Int(50, 5000))
                .RuleFor(f => f.Description, f => f.Lorem.Paragraph())
                .Generate(facilityCount);

            return facilities;
        }

        public List<FacilityReservation> GenerateFacilityReservations(int reservationCount, List<Facility> facilities, List<User> users)
        {
            var reservations = new Faker<FacilityReservation>()
                .RuleFor(r => r.FacilityId, f => f.PickRandom(facilities).Id)
                .RuleFor(r => r.StartTime, f => f.Date.Future())
                .RuleFor(r => r.EndTime, (f, r) => r.StartTime.AddHours(f.Random.Int(1, 4)))
                .RuleFor(r => r.EventType, f => f.Lorem.Word())
                .RuleFor(r => r.EventDescription, f => f.Lorem.Sentence())
                .RuleFor(r => r.ReservedUserId, f => f.PickRandom(users).Id)
                .Generate(reservationCount);

            return reservations;
        }

        public List<MaintenanceHistory> GenerateMaintenanceHistories(int historyCount, List<Facility> facilities, List<User> users)
        {
            var histories = new Faker<MaintenanceHistory>()
                .RuleFor(h => h.FacilityId, f => f.PickRandom(facilities).Id)
                .RuleFor(h => h.MaintenanceType, f => f.PickRandom("Preventiva", "Corretiva", "Urgente"))
                .RuleFor(h => h.Description, f => f.Lorem.Sentence())
                .RuleFor(h => h.MaintenanceDate, f => f.Date.Past(1))
                .RuleFor(h => h.RequestUserId, f => f.PickRandom(users).Id)
                .Generate(historyCount);

            return histories;
        }

        public List<MaintenanceRequest> GenerateMaintenanceRequests(int requestCount, List<Facility> facilities, List<User> users)
        {
            var requests = new Faker<MaintenanceRequest>()
                .RuleFor(r => r.FacilityId, f => f.PickRandom(facilities).Id)
                .RuleFor(r => r.MaintenanceType, f => f.PickRandom("Elétrica", "Hidráulica", "Estrutural"))
                .RuleFor(r => r.ProblemDescription, f => f.Lorem.Paragraph())
                .RuleFor(r => r.Priority, f => f.PickRandom("Baixa", "Média", "Alta"))
                .RuleFor(r => r.RequestDate, f => f.Date.Past(2))
                .RuleFor(r => r.RequestedUserId, f => f.PickRandom(users).Id)
                .RuleFor(r => r.Status, f => f.PickRandom("Aberto", "Em Andamento", "Concluído", "Cancelado"))
                .Generate(requestCount);

            return requests;
        }
    }
}
