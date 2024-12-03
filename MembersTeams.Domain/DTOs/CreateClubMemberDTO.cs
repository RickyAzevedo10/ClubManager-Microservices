namespace MembersTeams.Domain.DTOs
{
    public class CreateClubMemberDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Partner { get; set; }
        public bool EducationOfficer { get; set; }
        public DateTime DateOfJoining { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsUser { get; set; }
        public long UserId { get; set; }
    }
}
