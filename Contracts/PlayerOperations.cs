namespace Contracts
{
    public class CreateUpdatePlayer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long ExternalPlayerId { get; set; }
    }

    public class DeletePlayer
    {
        public long ExternalPlayerId { get; set; }
    }
}
