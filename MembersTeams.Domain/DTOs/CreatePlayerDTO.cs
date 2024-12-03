namespace MembersTeams.Domain.DTOs
{
    public class CreatePlayerDTO
    {
        public int PlayerCategoryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string PreferredFoot { get; set; }
        public List<CreateResponsiblePlayerDTO> ResponsiblePlayer { get; set; }
    }

    public class CreateResponsiblePlayerDTO
    {
        public int ResponsibleId { get; set; }
        public string Relationship { get; set; }
        public bool IsPrimaryContact { get; set; }
    }
}
