namespace MembersTeams.Domain.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public bool Female { get; set; }
        public bool Male { get; set; }
        public long ClubId { get; set; }
        public long TeamCategoryId { get; set; }
        public Institution Club { get; set; }

        public TeamCategory TeamCategories { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; }
        public ICollection<TeamCoach> TeamCoaches { get; set; }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetFemale(bool female)
        {
            Female = female;
        }

        public void SetMale(bool male)
        {
            Male = male;
        }

        public void SetClubId(long clubId)
        {
            ClubId = clubId;
        }

        public void SetTeamCategoryId(long teamCategoryId)
        {
            TeamCategoryId = teamCategoryId;
        }
    }
}
