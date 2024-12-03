namespace TrainingCompetition.Domain.Entities
{
    public class MatchStatistic : BaseEntity
    {
        public long MatchId { get; set; }
        public long PlayerId { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int MinutesPlayed { get; set; }
        public int Substitutions { get; set; }

        public Match Match { get; set; }
        public Player Player { get; set; }

        public void SetMatchId(long matchId)
        {
            MatchId = matchId;
        }

        public void SetPlayerId(long playerId)
        {
            PlayerId = playerId;
        }

        public void SetGoals(int goals)
        {
            Goals = goals;
        }

        public void SetAssists(int assists)
        {
            Assists = assists;
        }

        public void SetYellowCards(int yellowCards)
        {
            YellowCards = yellowCards;
        }

        public void SetRedCards(int redCards)
        {
            RedCards = redCards;
        }

        public void SetMinutesPlayed(int minutesPlayed)
        {
            MinutesPlayed = minutesPlayed;
        }

        public void SetSubstitutions(int substitutions)
        {
            Substitutions = substitutions;
        }

    }
}
