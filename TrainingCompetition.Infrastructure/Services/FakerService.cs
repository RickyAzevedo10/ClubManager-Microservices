using Bogus;
using TrainingCompetition.Domain.Entities;

namespace TrainingCompetition.Infrastructure.Services
{
    public class FakerService
    {
        public List<Player> GenerateFakePlayers(int count)
        {
            var faker = new Faker<Player>()
                .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                .RuleFor(p => p.LastName, f => f.Name.LastName());

            return faker.Generate(count);
        }
       
        public List<Team> GenerateFakeTeams(int count)
        {
            var teams = new List<Team>();

            for (int i = 0; i < count; i++)
            {
                var team = new Faker<Team>()
                    .RuleFor(t => t.Name, f => $"{f.Commerce.Department()} {f.Random.Word()}")
                    .RuleFor(t => t.Female, f => f.Random.Bool())
                    .RuleFor(t => t.Male, (f, t) => !t.Female) 
                    .Generate();

                teams.Add(team);
            }

            return teams;
        }

        public List<Match> GenerateFakeMatches(List<Team> teams, int count)
        {
            var match = new Faker<Match>()
                .RuleFor(m => m.Date, f => f.Date.Future(1))
                .RuleFor(m => m.Opponent, f => f.PickRandom(teams).Name)
                .RuleFor(m => m.Location, f => f.Address.City())
                .RuleFor(m => m.CompetitionType, f => f.PickRandom("Friendly", "League", "Cup"))
                .RuleFor(m => m.TeamId, f => f.PickRandom(teams).Id)
                .RuleFor(m => m.IsCanceled, f => f.Random.Bool())
                .Generate(count);

            return match;
        }

        public List<MatchStatistic> GenerateFakeMatchStatistics(List<Match> matches, List<Player> players, int count)
        {
            var matchStatistic = new Faker<MatchStatistic>()
                .RuleFor(ms => ms.MatchId, f => f.PickRandom(matches).Id)
                .RuleFor(ms => ms.PlayerId, f => f.PickRandom(players).Id)  
                .RuleFor(ms => ms.Goals, f => f.Random.Int(0, 3))
                .RuleFor(ms => ms.Assists, f => f.Random.Int(0, 2))
                .RuleFor(ms => ms.YellowCards, f => f.Random.Int(0, 2))
                .RuleFor(ms => ms.RedCards, f => f.Random.Int(0, 1))
                .RuleFor(ms => ms.MinutesPlayed, f => f.Random.Int(45, 90))
                .RuleFor(ms => ms.Substitutions, f => f.Random.Int(0, 3))
                .Generate(count);
 
            return matchStatistic;
        }

        public List<TrainingSession> GenerateFakeTrainingSessions(List<Team> teams, int count)
        {
            var trainingSession = new Faker<TrainingSession>()
                    .RuleFor(ts => ts.TeamId, f => f.PickRandom(teams).Id)
                    .RuleFor(ts => ts.Name, f => f.Hacker.Noun())
                    .RuleFor(ts => ts.Date, f => f.Date.Future(1))
                    .RuleFor(ts => ts.Duration, f => f.Random.Int(60, 120))
                    .RuleFor(ts => ts.Location, f => f.Address.City())
                    .RuleFor(ts => ts.Objectives, f => f.Lorem.Sentence())
                    .RuleFor(ts => ts.Description, f => f.Lorem.Paragraph())
                    .RuleFor(ts => ts.IsCanceled, f => f.Random.Bool())
                    .Generate(count);

            return trainingSession;
        }

        public List<TrainingAttendance> GenerateFakeTrainingAttendances(List<TrainingSession> sessions, List<Player> players, int count)
        {
            var trainingAttendance = new Faker<TrainingAttendance>()
                    .RuleFor(ta => ta.TrainingSessionId, f => f.PickRandom(sessions).Id)
                    .RuleFor(ta => ta.PlayerId, f => f.PickRandom(players).Id)  
                    .RuleFor(ta => ta.IsPresent, f => f.Random.Bool())
                    .RuleFor(ta => ta.AbsenceReason, f => f.Lorem.Sentence())
                    .Generate(count);
          
            return trainingAttendance;
        }
    }
}
