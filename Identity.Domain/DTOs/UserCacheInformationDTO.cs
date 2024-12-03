namespace Identity.Domain.DTOs
{
    public class UserCacheInformationDTO
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public UserCacheInformationDTO(long id, string email, string roleName)
        {
            Id = id;
            Email = email;
            Role = roleName;
        }
    }
}
