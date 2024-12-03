﻿namespace TrainingCompetition.Domain.DTOs
{
    public class UpdateMatchStatisticDTO
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int MinutesPlayed { get; set; }
        public int Substitutions { get; set; }
    }
}
