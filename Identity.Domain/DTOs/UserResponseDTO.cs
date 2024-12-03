namespace Identity.Domain.DTOs
{
    public class UserResponseDTO
    {
        public long Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? PhoneNumber { get; set; }
        public UserRoleResponseDTO userRole { get; set; }
        public UserPermissionResponseDTO userPermission { get; set; }
    }
}
