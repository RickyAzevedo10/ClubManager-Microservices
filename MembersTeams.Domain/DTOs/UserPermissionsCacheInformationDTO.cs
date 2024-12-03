namespace MembersTeams.Domain.DTOs
{
    public class UserPermissionsCacheInformationDTO
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public bool Edit { get; set; }
        public bool Create { get; set; }
        public bool Delete { get; set; }
        public bool Consult { get; set; }


        public UserPermissionsCacheInformationDTO(long id, string email, bool edit, bool delete, bool consult, bool create)
        {
            Id = id;
            Email = email;
            Edit = edit;
            Create = create;
            Delete = delete;
            Consult = consult;
        }
    }
}
