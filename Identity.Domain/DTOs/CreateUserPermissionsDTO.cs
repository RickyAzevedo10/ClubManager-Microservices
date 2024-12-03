namespace Identity.Domain.DTOs
{
    public class CreateUserPermissionsDTO
    {
        public bool Edit { get; set; }
        public bool Create { get; set; }
        public bool Delete { get; set; }
        public bool Consult { get; set; }
    }
}
