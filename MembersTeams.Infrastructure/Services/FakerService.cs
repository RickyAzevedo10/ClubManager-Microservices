using Bogus;
using MembersTeams.Domain.Entities;

namespace MembersTeams.Infrastructure.Services
{
    public class FakerService
    {
        public List<Institution> GenerateInstitution(int institutionCount)
        {
            int _institutionId = 1;

            var institutions = new Faker<Institution>()
                .RuleFor(i => i.Id, f => _institutionId++)
                .RuleFor(i => i.Name, f => f.Company.CompanyName())
                .Generate(institutionCount);

            return institutions;
        }

        public List<User> GenerateUsers(int userCount)
        {
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
                //.RuleFor(p => p.Id, f => f.IndexFaker + 1) // IDs sequenciais
                .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                .RuleFor(p => p.LastName, f => f.Name.LastName())
                .RuleFor(p => p.DateOfBirth, f => f.Date.Past(20, DateTime.Now.AddYears(-16)))
                .RuleFor(p => p.Nationality, f => f.Address.Country())
                .RuleFor(p => p.Position, f => f.PickRandom("Goalkeeper", "Defender", "Midfielder", "Forward"))
                .RuleFor(p => p.Height, f => f.Random.Int(160, 200))
                .RuleFor(p => p.Weight, f => f.Random.Int(60, 100))
                .RuleFor(p => p.PreferredFoot, f => f.PickRandom("Left", "Right", "Both"))
                .RuleFor(p => p.PlayerCategoryId, f => f.Random.Long(1, 5));

            return faker.Generate(count);
        }

        public List<PlayerContract> GenerateFakePlayerContracts(List<Player> players, int count)
        {
            var faker = new Faker<PlayerContract>()
                //.RuleFor(pc => pc.Id, f => f.IndexFaker + 1)
                .RuleFor(pc => pc.PlayerId, f => f.PickRandom(players).Id) // Escolhe IDs dos jogadores
                .RuleFor(pc => pc.StartDate, f => f.Date.Past(2))
                .RuleFor(pc => pc.EndDate, (f, pc) => pc.StartDate.AddYears(f.Random.Int(1, 5)))
                .RuleFor(pc => pc.Salary, f => f.Finance.Amount(20000, 500000))
                .RuleFor(pc => pc.ContractType, f => f.PickRandom("Professional", "Youth", "Amateur"));

            return faker.Generate(count);
        }

        public List<PlayerPerformanceHistory> GenerateFakePlayerPerformanceHistories(List<Player> players, int count)
        {
            var faker = new Faker<PlayerPerformanceHistory>()
                //.RuleFor(pph => pph.Id, f => f.IndexFaker + 1)
                .RuleFor(pph => pph.PlayerId, f => f.PickRandom(players).Id) // Relaciona com PlayerId
                .RuleFor(pph => pph.Season, f => $"{f.Date.Past(1).Year}/{f.Date.Past(1).Year + 1}")
                .RuleFor(pph => pph.ClubOpponent, f => f.Company.CompanyName())
                .RuleFor(pph => pph.Goals, f => f.Random.Int(0, 20))
                .RuleFor(pph => pph.Assists, f => f.Random.Int(0, 15))
                .RuleFor(pph => pph.MinutesPlayed, f => f.Random.Int(0, 90))
                .RuleFor(pph => pph.YellowCards, f => f.Random.Int(0, 5))
                .RuleFor(pph => pph.RedCards, f => f.Random.Int(0, 1));

            return faker.Generate(count);
        }

        public List<PlayerTransfer> GenerateFakePlayerTransfers(List<Player> players, int count)
        {
            var faker = new Faker<PlayerTransfer>()
                //.RuleFor(pt => pt.Id, f => f.IndexFaker + 1)
                .RuleFor(pt => pt.PlayerId, f => f.PickRandom(players).Id) // Relaciona com PlayerId
                .RuleFor(pt => pt.FromClub, f => f.Company.CompanyName())
                .RuleFor(pt => pt.ToClub, f => f.Company.CompanyName())
                .RuleFor(pt => pt.TransferDate, f => f.Date.Past(2))
                .RuleFor(pt => pt.TransferFee, f => f.Finance.Amount(10000, 10000000));

            return faker.Generate(count);
        }

        public List<ClubMember> GenerateFakeClubMembers(int count)
        {
            var faker = new Faker<ClubMember>()
                //.RuleFor(cm => cm.Id, f => f.IndexFaker + 1)
                .RuleFor(cm => cm.FirstName, f => f.Name.FirstName())
                .RuleFor(cm => cm.LastName, f => f.Name.LastName())
                .RuleFor(cm => cm.Partner, f => f.Random.Bool())
                .RuleFor(cm => cm.EducationOfficer, f => f.Random.Bool())
                .RuleFor(cm => cm.DateOfJoining, f => f.Date.Past(5))
                .RuleFor(cm => cm.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
                .RuleFor(cm => cm.Email, f => f.Internet.Email())
                .RuleFor(cm => cm.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(cm => cm.Address, f => f.Address.FullAddress());

            return faker.Generate(count);
        }

        public List<MinorClubMember> GenerateFakeMinorClubMembers(List<ClubMember> guardians, int count)
        {
            var faker = new Faker<MinorClubMember>()
                //.RuleFor(mcm => mcm.Id, f => f.IndexFaker + 1)
                .RuleFor(mcm => mcm.FirstName, f => f.Name.FirstName())
                .RuleFor(mcm => mcm.LastName, f => f.Name.LastName())
                .RuleFor(mcm => mcm.DateOfBirth, f => f.Date.Past(12, DateTime.Now.AddYears(-5)))
                .RuleFor(mcm => mcm.DateOfJoining, f => f.Date.Past(2))
                .RuleFor(mcm => mcm.Partner, f => f.Random.Bool())
                .RuleFor(mcm => mcm.Address, f => f.Address.FullAddress())
                .RuleFor(mcm => mcm.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(mcm => mcm.Email, f => f.Internet.Email())
                .RuleFor(mcm => mcm.GuardianId, f => f.PickRandom(guardians).Id); // Relaciona com GuardianId

            return faker.Generate(count);
        }

        public List<PlayerResponsible> GenerateFakePlayerResponsibles(List<Player> players, List<ClubMember> members, int responsiblesPerPlayer)
        {
            var responsibles = new List<PlayerResponsible>();
            var responsibleId = 1;

            foreach (var player in players)
            {
                // Selecionar membros aleatórios para cada jogador
                var selectedMembers = members.OrderBy(_ => Guid.NewGuid()).Take(responsiblesPerPlayer).ToList();

                foreach (var member in selectedMembers)
                {
                    var responsible = new Faker<PlayerResponsible>()
                        //.RuleFor(r => r.Id, _ => responsibleId++)
                        .RuleFor(r => r.PlayerId, _ => player.Id)
                        .RuleFor(r => r.MemberId, _ => member.Id)
                        .RuleFor(r => r.Relationship, f => f.PickRandom("Parent", "Guardian", "Coach", "Relative"))
                        .RuleFor(r => r.IsPrimaryContact, f => f.Random.Bool())
                        .Generate();

                    responsibles.Add(responsible);
                }
            }

            return responsibles;
        }

        public List<UserClubMember> GenerateFakeUserClubMembers(List<User> users, List<ClubMember> clubMembers)
        {
            var userClubMembers = new List<UserClubMember>();
            var userClubMemberId = 1;

            // Combinar cada utilizador com um membro do clube
            var userClubPairs = users.Zip(clubMembers, (user, clubMember) => new { user, clubMember });

            foreach (var pair in userClubPairs)
            {
                var userClubMember = new Faker<UserClubMember>()
                    //.RuleFor(ucm => ucm.Id, _ => userClubMemberId++)
                    .RuleFor(ucm => ucm.UserId, _ => pair.user.Id)
                    .RuleFor(ucm => ucm.ClubMemberId, _ => pair.clubMember.Id)
                    .Generate();

                userClubMembers.Add(userClubMember);
            }

            return userClubMembers;
        }

        public List<Team> GenerateFakeTeams(Institution clubs, int count)
        {
            var teams = new List<Team>();
            var teamId = 1;

            for (int i = 0; i < count; i++)
            {
                var team = new Faker<Team>()
                    //.RuleFor(t => t.Id, _ => teamId++)
                    .RuleFor(t => t.Name, f => $"{f.Commerce.Department()} {f.Random.Word()}")
                    .RuleFor(t => t.Female, f => f.Random.Bool())
                    .RuleFor(t => t.Male, (f, t) => !t.Female) // Exclusividade entre Female e Male
                    .RuleFor(t => t.ClubId, f => f.PickRandom(clubs).Id)
                    .RuleFor(t => t.TeamCategoryId, f => f.PickRandom(1,9))
                    .Generate();

                teams.Add(team);
            }

            return teams;
        }

        public List<TeamCoach> GenerateFakeTeamCoaches(List<Team> teams, List<User> coaches, int coachesPerTeam)
        {
            var teamCoaches = new List<TeamCoach>();
            var teamCoachId = 1;

            foreach (var team in teams)
            {
                var selectedCoaches = coaches.OrderBy(_ => Guid.NewGuid()).Take(coachesPerTeam).ToList();

                foreach (var coach in selectedCoaches)
                {
                    var teamCoach = new Faker<TeamCoach>()
                        //.RuleFor(tc => tc.Id, _ => teamCoachId++)
                        .RuleFor(tc => tc.TeamId, _ => team.Id)
                        .RuleFor(tc => tc.UserId, _ => coach.Id)
                        .RuleFor(tc => tc.IsHeadCoach, f => f.Random.Bool(0.3f)) // 30% de chance de ser Head Coach
                        .Generate();

                    teamCoaches.Add(teamCoach);
                }
            }

            return teamCoaches;
        }

        public List<TeamPlayer> GenerateFakeTeamPlayers(List<Team> teams, List<Player> players, int playersPerTeam)
        {
            var teamPlayers = new List<TeamPlayer>();
            var teamPlayerId = 1;

            foreach (var team in teams)
            {
                var selectedPlayers = players.OrderBy(_ => Guid.NewGuid()).Take(playersPerTeam).ToList();

                foreach (var player in selectedPlayers)
                {
                    var teamPlayer = new Faker<TeamPlayer>()
                        //.RuleFor(tp => tp.Id, _ => teamPlayerId++)
                        .RuleFor(tp => tp.TeamId, _ => team.Id)
                        .RuleFor(tp => tp.PlayerId, _ => player.Id)
                        .Generate();

                    teamPlayers.Add(teamPlayer);
                }
            }

            return teamPlayers;
        }
    }
}
