using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Entities
{
    public class MinorClubMember : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; } = DateTime.Now;
        public bool Partner { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public long GuardianId { get; set; }
        public ClubMember Guardian { get; set; }

        public MinorClubMember() { }

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

        public void SetDateOfJoining(DateTime dateOfJoining)
        {
            DateOfJoining = dateOfJoining;
        }

        public void SetPartner(bool partner)
        {
            Partner = partner;
        }

        public void SetAddress(string address)
        {
            Address = address;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetGuardianId(long guardianId)
        {
            GuardianId = guardianId;
        }
    }
}
