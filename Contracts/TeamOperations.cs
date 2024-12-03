namespace Contracts
{
    public class CreateUpdateTeam
    {
        public string Name { get; set; }
        public bool Female { get; set; }
        public bool Male { get; set; }
        public long ExternalTeamId { get; set; }
    }

    public class DeleteTeam
    {
        public long ExternalTeamId { get; set; }
    }
}
