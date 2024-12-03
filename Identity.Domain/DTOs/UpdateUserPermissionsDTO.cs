namespace Identity.Domain.DTOs
{
    public class UpdateUserPermissionsDTO
    {
        public long UserId { get; set; }
        public bool Edit { get; set; }
        public bool Create { get; set; }
        public bool Delete { get; set; }
        public bool Consult { get; set; }
    }
}
