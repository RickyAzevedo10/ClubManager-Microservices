namespace Financial.Domain.Entities
{
    public class ClubMember : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Partner { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

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

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
