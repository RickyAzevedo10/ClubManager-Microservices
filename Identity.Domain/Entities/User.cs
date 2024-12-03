using Identity.Domain.DTOs;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Domain.Entities
{
    public class User : BaseEntity
    {
        public long InstitutionId { get; set; }
        public Institution Institution { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorActivated { get; set; } = false;
        public int AccessFailedCount { get; set; } = 0;
        public DateTime? LockoutEnd { get; set; }
        public DateTime? DateOfLastAccess { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpire { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpire { get; set; }
        public long UserRoleId { get; set; }
        public UserRoles UserRole { get; set; }
        public long UserPermissionId { get; set; }
        public UserPermissions UserPermission { get; set; }

        //TODO: validar ligações
        //public ICollection<TeamCoach> TeamCoaches { get; set; }
        //public ICollection<MaintenanceRequest> MaintenanceRequests { get; set; }
        //public ICollection<MaintenanceHistory> MaintenanceHistory { get; set; }
        //public ICollection<FacilityReservation> FacilityReservation { get; set; }
        //public virtual UserClubMember UserClubMember { get; set; }
        //public virtual Expense Expense { get; set; }
        //public virtual Revenue Revenue { get; set; }

        private User() { }

        public User(CreateUserDTO user)
        {
            Email = user.Email;
            SetPassword(user.Password);
            Username = user.Username;
            UserRoleId = user.RoleId;
            InstitutionId = user.InstitutionId;
            PhoneNumber = user.PhoneNumber;
        }

        public void UpdateRefreshToken(string refreshToken, int expiresHours)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpire = DateTime.UtcNow.AddHours(expiresHours);
        }

        public void SetPassword(string password)
        {
            using var hmac = new HMACSHA256();
            PasswordSalt = hmac.Key;
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void SetRoleId(long roleId)
        {
            UserRoleId = roleId;
        }

        public void SetPasswordResetToken(string passwordResetToken)
        {
            PasswordResetToken = passwordResetToken;
        }

        public void SetPasswordResetTokenExpire(int expiresHours)
        {
            PasswordResetTokenExpire = DateTime.UtcNow.AddHours(expiresHours);
        }

        public void SetUserPermissionId(long userPermissionId)
        {
            UserPermissionId = userPermissionId;
        }

        public bool ValidatePassword(string password)
        {
            using var hmac = new HMACSHA256(PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return StructuralComparisons.StructuralEqualityComparer.Equals(computedHash, PasswordHash);
        }

        public void UpdateLastAccessDate()
        {
            DateOfLastAccess = DateTime.UtcNow;
        }
    }
}
