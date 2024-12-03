using System.ComponentModel.DataAnnotations.Schema;

namespace MembersTeams.Domain.Entities
{
    public class UserClubMember : BaseEntity
    {
        public long UserId { get; set; }
        public long ClubMemberId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("ClubMemberId")]
        public ClubMember ClubMember { get; set; }

        public void SetUserId(long userId)
        {
            UserId = userId;
        }

        public void SetClubMemberId(long clubMemberId)
        {
            ClubMemberId = clubMemberId;
        }
    }
}
