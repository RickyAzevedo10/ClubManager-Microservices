namespace MembersTeams.Domain.Entities
{
    public class Player : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string PreferredFoot { get; set; }
        public long PlayerCategoryId { get; set; }
        public PlayerCategory Category { get; set; }
        public ICollection<PlayerPerformanceHistory> PlayerPerformanceHistories { get; set; }
        public ICollection<PlayerResponsible> PlayerResponsibles { get; set; }
        public ICollection<PlayerContract> PlayerContracts { get; set; }
        public ICollection<PlayerTransfer> PlayerTransfers { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; }

        public Player()
        {

        }

        public void SetFirstName(string firstName)
        {
            FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {
            LastName = lastName;
        }

        public void SetDateOfBirth(DateTime dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
        }

        public void SetNationality(string nationality)
        {
            Nationality = nationality;
        }

        public void SetPosition(string position)
        {
            Position = position;
        }

        public void SetHeight(int height)
        {
            Height = height;
        }

        public void SetWeight(int weight)
        {
            Weight = weight;
        }

        public void SetPreferredFoot(string preferredFoot)
        {
            PreferredFoot = preferredFoot;
        }

        public void SetPlayerCategoryId(long playerCategoryId)
        {
            PlayerCategoryId = playerCategoryId;
        }
    }
}
