namespace Identity.Domain.DTOs
{
    public class UpdateUserDTO
    {
        public long Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public long RoleId { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
