namespace Contracts
{
    public class CreateUpdateClubMember
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Partner { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long ExternalClubMemberId { get; set; }
    }

    public class DeleteClubMember
    {
        public long ExternalClubMemberId { get; set; }
    }
}
