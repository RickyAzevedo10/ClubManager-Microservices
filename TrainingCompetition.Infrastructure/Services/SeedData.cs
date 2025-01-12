using TrainingCompetition.Infra.Contexts;

namespace TrainingCompetition.Infrastructure.Services
{
    public class SeedData
    {
        public static async Task SeedAsync(DataContext context, FakerService fakeService)
        {
            // Verificar se os dados já existem
            if (context.Players.Any())
            {
                return; 
            }
            
            var players = fakeService.GenerateFakePlayers(10);
            await context.Players.AddRangeAsync(players);
            await context.SaveChangesAsync();

            var teams = fakeService.GenerateFakeTeams( 5);
            await context.Teams.AddRangeAsync(teams);
            await context.SaveChangesAsync();

            var matches = fakeService.GenerateFakeMatches(teams, 10);
            await context.Matches.AddRangeAsync(matches);
            await context.SaveChangesAsync();

            var matchStatistics = fakeService.GenerateFakeMatchStatistics(matches, players, 30);
            await context.MatchStatistics.AddRangeAsync(matchStatistics);
            await context.SaveChangesAsync();

            var trainingSessions = fakeService.GenerateFakeTrainingSessions(teams, 5);
            await context.TrainingSessions.AddRangeAsync(trainingSessions);
            await context.SaveChangesAsync();

            var trainingAttendances = fakeService.GenerateFakeTrainingAttendances(trainingSessions, players, 50);
            await context.TrainingAttendances.AddRangeAsync(trainingAttendances);
            await context.SaveChangesAsync();
        }
    }
}
