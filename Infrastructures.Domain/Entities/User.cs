namespace Infrastructures.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string? PhoneNumber { get; set; }
        public long ExternalUserId { get; set; }
        public User() { }

        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void SetUsername(string username)
        {
            Username = username;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetExternalUserId(long externalUserId)
        {
            ExternalUserId = externalUserId;
        }

    }
}
