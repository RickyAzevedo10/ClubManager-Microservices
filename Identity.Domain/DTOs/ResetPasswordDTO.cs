namespace Identity.Domain.DTOs
{
    public class ResetPasswordDTO
    {
        public string? Token { get; set; }
        public string? NewPassword { get; set; }
        public string? RepeatNewPassword { get; set; }
    }
}
