using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Entities
{
    public class PlayerPerformanceHistory : BaseEntity
    {
        public long PlayerId { get; set; }
        public Player Player { get; set; }
        public string Season { get; set; }
        public string ClubOpponent { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int MinutesPlayed { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }

        public void SetPlayerId(long playerId)
        {
            PlayerId = playerId;
        }

        public void SetSeason(string season)
        {
            Season = season;
        }

        public void SetClubOpponent(string clubOpponent)
        {
            ClubOpponent = clubOpponent;
        }

        public void SetGoals(int goals)
        {
            Goals = goals;
        }

        public void SetAssists(int assists)
        {
            Assists = assists;
        }

        public void SetMinutesPlayed(int minutesPlayed)
        {
            MinutesPlayed = minutesPlayed;
        }

        public void SetYellowCards(int yellowCards)
        {
            YellowCards = yellowCards;
        }

        public void SetRedCards(int redCards)
        {
            RedCards = redCards;
        }
    }
}
