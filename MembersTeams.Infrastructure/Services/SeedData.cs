using MembersTeams.Infra.Contexts;

namespace MembersTeams.Infrastructure.Services
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

            var institutions = context.Institution;

            var users = fakeService.GenerateUsers(15); 
            await context.User.AddRangeAsync(users);
            await context.SaveChangesAsync();

            var guardians = fakeService.GenerateFakeClubMembers(6);
            await context.ClubMember.AddRangeAsync(guardians);
            await context.SaveChangesAsync();

            var minorMembers = fakeService.GenerateFakeMinorClubMembers(guardians, 4);
            await context.MinorClubMember.AddRangeAsync(minorMembers);
            await context.SaveChangesAsync();

            var players = fakeService.GenerateFakePlayers(10);
            await context.Player.AddRangeAsync(players);
            await context.SaveChangesAsync();

            var playerContracts = fakeService.GenerateFakePlayerContracts(players, 5);
            await context.PlayerContract.AddRangeAsync(playerContracts);
            await context.SaveChangesAsync();

            var playerHistories = fakeService.GenerateFakePlayerPerformanceHistories(players, 8);
            await context.PlayerPerformanceHistory.AddRangeAsync(playerHistories);
            await context.SaveChangesAsync();

            var playerTransfers = fakeService.GenerateFakePlayerTransfers(players, 3);
            await context.PlayerTransfer.AddRangeAsync(playerTransfers);
            await context.SaveChangesAsync();

            var playerResponsibles = fakeService.GenerateFakePlayerResponsibles(players, guardians, 2);
            await context.PlayerResponsible.AddRangeAsync(playerResponsibles);
            await context.SaveChangesAsync();

            var userClubMembers = fakeService.GenerateFakeUserClubMembers(users, guardians);
            await context.UserClubMembers.AddRangeAsync(userClubMembers);
            await context.SaveChangesAsync();

            var teams = fakeService.GenerateFakeTeams(institutions.First(), 5);
            await context.Team.AddRangeAsync(teams);
            await context.SaveChangesAsync();

            var teamCoaches = fakeService.GenerateFakeTeamCoaches(teams, users, 2);
            await context.TeamCoach.AddRangeAsync(teamCoaches);
            await context.SaveChangesAsync();

            var teamPlayers = fakeService.GenerateFakeTeamPlayers(teams, players, 5);
            await context.TeamPlayer.AddRangeAsync(teamPlayers);
            await context.SaveChangesAsync();
        }
    }
}
