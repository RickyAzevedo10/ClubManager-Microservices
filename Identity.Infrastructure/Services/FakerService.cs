using Bogus;
using Identity.Domain.Entities;

namespace Identity.Infrastructure.Services
{
    public class FakerService
    {
        #region Identity Faker
        public List<Institution> GenerateInstitution(int institutionCount)
        {
            int _institutionId = 1;

            var institutions = new Faker<Institution>()
                .RuleFor(i => i.Id, f => _institutionId++)
                .RuleFor(i => i.Name, f => f.Company.CompanyName())
                .RuleFor(i => i.FoundationDate, f => f.Date.Past(100))
                .RuleFor(i => i.Address, f => f.Address.FullAddress())
                .RuleFor(i => i.StadiumName, f => f.Lorem.Word())
                .RuleFor(i => i.StadiumCapacity, f => f.Random.Int(1000, 50000))
                .RuleFor(i => i.StadiumInauguration, f => f.Date.Past(50))
                .RuleFor(i => i.HaveTrainingCenter, f => f.Random.Bool())
                .RuleFor(i => i.Colors, f => f.Commerce.Color())
                .RuleFor(i => i.OfficialWebsiteUrl, f => f.Internet.Url())
                .RuleFor(i => i.SocialMediaLinks, f => f.Internet.Url())
                .Generate(institutionCount);

            return institutions;
        }

        public List<User> GenerateUsers(int userCount, Institution institution, List<UserPermissions> permissions)
        {
            var userPermissionIds = permissions.Select(p => p.Id).OrderBy(id => id).ToList();

            var users = new Faker<User>()
                .RuleFor(u => u.InstitutionId, institution.Id)
                .RuleFor(u => u.Email, f => $"user-{Guid.NewGuid()}@example.com")
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.PasswordHash, f => f.Random.Bytes(20))
                .RuleFor(u => u.PasswordSalt, f => f.Random.Bytes(20))
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.PhoneNumberConfirmed, f => f.Random.Bool())
                .RuleFor(u => u.TwoFactorActivated, f => f.Random.Bool())
                .RuleFor(u => u.AccessFailedCount, f => f.Random.Int(0, 5))
                .RuleFor(u => u.LockoutEnd, f => f.Date.Past(1))
                .RuleFor(u => u.DateOfLastAccess, f => f.Date.Recent())
                .RuleFor(u => u.RefreshToken, f => f.Random.AlphaNumeric(16))
                .RuleFor(u => u.RefreshTokenExpire, f => f.Date.Future())
                .RuleFor(u => u.PasswordResetToken, f => f.Random.AlphaNumeric(8))
                .RuleFor(u => u.PasswordResetTokenExpire, f => f.Date.Future())
                .RuleFor(u => u.UserRoleId, f => f.Random.Int(1, 7))
                .RuleFor(u => u.UserPermissionId, f =>
                {
                    // Atribui o primeiro ID da lista
                    var id = userPermissionIds[0];
                    // Remove o ID atribuído da lista
                    userPermissionIds.RemoveAt(0);
                    return id;
                })
                .Generate(userCount);

            return users;
        }

        public List<UserPermissions> GenerateUserPermissions(int count)
        {
            return new Faker<UserPermissions>()
                .RuleFor(up => up.Edit, f => f.Random.Bool())
                .RuleFor(up => up.Create, f => f.Random.Bool())
                .RuleFor(up => up.Delete, f => f.Random.Bool())
                .RuleFor(up => up.Consult, f => f.Random.Bool())
                .Generate(count);
        }
        #endregion

    }
}
