namespace Identity.Domain.DTOs
{
    public class RecoverPasswordRequestResponseDTO
    {
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}
