namespace MembersTeams.Domain.Entities
{
    public class ClubMember : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Partner { get; set; }
        public bool EducationOfficer { get; set; }
        public DateTime DateOfJoining { get; set; } = DateTime.Now;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public ICollection<MinorClubMember> MinorClubMembers { get; set; }
        public ICollection<PlayerResponsible> PlayerResponsibles { get; set; }

        public virtual UserClubMember UserClubMember { get; set; }

        public ClubMember()
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

        public void SetPartner(bool partner)
        {
            Partner = partner;
        }

        public void SetEducationOfficer(bool educationOfficer)
        {
            EducationOfficer = educationOfficer;
        }

        public void SetDateOfJoining(DateTime dateOfJoining)
        {
            DateOfJoining = dateOfJoining;
        }

        public void SetDateOfBirth(DateTime dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void SetAddress(string address)
        {
            Address = address;
        }
    }
}
