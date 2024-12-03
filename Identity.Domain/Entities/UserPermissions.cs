namespace Identity.Domain.Entities
{
    public class UserPermissions : BaseEntity
    {
        public bool Edit { get; set; }
        public bool Create { get; set; }
        public bool Delete { get; set; }
        public bool Consult { get; set; }
        public User Users { get; set; }

        public UserPermissions()
        {
            
        }

        public UserPermissions(bool edit, bool create, bool delete, bool consult)
        {
            Edit = edit;
            Create = create;
            Delete = delete;
            Consult = consult;
        }
    }
}
