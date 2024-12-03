namespace Contracts
{
    public class CreateUpdateUser
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string? PhoneNumber { get; set; }
        public long ExternalUserId { get; set; }
    }

    public class DeleteUser
    {
        public long ExternalUserId { get; set; }
    }
}
