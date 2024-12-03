namespace MembersTeams.Domain.DTOs
{
    public class UpdateMinorClubMemberDTO
    {
        public long? Id { get; set; }
        public long? MemberId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool Partner { get; set; } // se é socio ou nao
        public DateTime DateOfJoining { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
