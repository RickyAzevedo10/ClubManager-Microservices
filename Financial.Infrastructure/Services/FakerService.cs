using Bogus;
using Financial.Domain.Entities;

namespace Financial.Infrastructure.Services
{
    public class FakerService
    {
        public List<User> GenerateUsers(int userCount)
        {
            int _userId = 1;

            var users = new Faker<User>()
                .RuleFor(u => u.Email, f => $"user-{Guid.NewGuid()}@example.com")
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .Generate(userCount);

            return users;
        }

        public List<Player> GenerateFakePlayers(int count)
        {
            var faker = new Faker<Player>()
                .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                .RuleFor(p => p.LastName, f => f.Name.LastName());

            return faker.Generate(count);
        }

        public List<ClubMember> GenerateFakeClubMembers(int count)
        {
            var faker = new Faker<ClubMember>()
                .RuleFor(cm => cm.FirstName, f => f.Name.FirstName())
                .RuleFor(cm => cm.LastName, f => f.Name.LastName())
                .RuleFor(cm => cm.Partner, f => f.Random.Bool())
                .RuleFor(cm => cm.Email, f => f.Internet.Email())
                .RuleFor(cm => cm.PhoneNumber, f => f.Phone.PhoneNumber());

            return faker.Generate(count);
        }

        public List<Entity> GenerateFakeEntities(List<ClubMember> clubMembers, List<Player> players, int count)
        {
            var availablePlayers = new List<Player>(players);

            var entity = new Faker<Entity>()
                    .RuleFor(e => e.Internal, f => f.Random.Bool())
                    .RuleFor(e => e.External, f => f.Random.Bool())
                    .RuleFor(e => e.EntityType, f => f.PickRandom("Club", "Player", "Manager", "Sponsor"))
                    .RuleFor(e => e.EntityName, f => f.Company.CompanyName())
                    .RuleFor(e => e.ClubMemberId, f => f.PickRandom(clubMembers).Id)
                    .RuleFor(e => e.PlayerId, f =>
                    {
                        if (availablePlayers.Count == 0) return null; // Caso não haja mais jogadores disponíveis.
                        var player = f.PickRandom(availablePlayers);
                        availablePlayers.Remove(player); // Remove o jogador da lista para evitar duplicação.
                        return player.Id;
                    })
                    .Generate(count);

            return entity;
        }

        public List<Expense> GenerateFakeExpenses(List<Entity> entities, List<User> users, int count)
        {
            var expenseId = 1;
            var availableUser = new List<User>(users);

            var expense = new Faker<Expense>()
                    .RuleFor(e => e.ExpenseDate, f => f.Date.Past(1))
                    .RuleFor(e => e.Amount, f => f.Finance.Amount(100, 1000))
                    .RuleFor(e => e.Destination, f => f.Company.CompanyName())
                    .RuleFor(e => e.CategoryId, f => f.PickRandom(1,15))
                    .RuleFor(e => e.Description, f => f.Lorem.Sentence())
                    .RuleFor(e => e.PaymentReference, f => f.Finance.CreditCardNumber())
                    .RuleFor(e => e.EntityId, f => f.PickRandom(entities).Id)
                    .RuleFor(e => e.ResponsibleUserId, f => {
                        if (availableUser.Count == 0) return 0;
                        var user = f.PickRandom(availableUser);
                        availableUser.Remove(user); 
                        return user.Id;
                    })
                    .Generate(count);

            return expense;
        }

        public List<Revenue> GenerateFakeRevenues(List<Entity> entities, List<User> users, int count)
        {
            var revenueId = 1;
            var availableUser = new List<User>(users);

            var revenue = new Faker<Revenue>()
                //.RuleFor(r => r.Id, _ => revenueId++)
                .RuleFor(r => r.RevenueDate, f => f.Date.Past(1))
                .RuleFor(r => r.Amount, f => f.Finance.Amount(500, 5000))
                .RuleFor(r => r.Source, f => f.Company.CompanyName())
                .RuleFor(r => r.CategoryId, f => f.PickRandom(1,15))
                .RuleFor(r => r.Description, f => f.Lorem.Sentence())
                .RuleFor(r => r.PaymentReference, f => f.Finance.CreditCardNumber())
                .RuleFor(r => r.EntityId, f => f.PickRandom(entities).Id)
                .RuleFor(e => e.ResponsibleUserId, f => {
                    if (availableUser.Count == 0) return 0;
                    var user = f.PickRandom(availableUser);
                    availableUser.Remove(user);
                    return user.Id;
                })
                .Generate(count);

            return revenue;
        }
    }
}
