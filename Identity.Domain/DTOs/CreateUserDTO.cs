namespace Identity.Domain.DTOs
{
    public class CreateUserDTO
    {
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public long RoleId { get; set; }
        public long InstitutionId { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Edit { get; set; }
        public bool Create { get; set; }
        public bool Delete { get; set; }
        public bool Consult { get; set; }
    }
}
