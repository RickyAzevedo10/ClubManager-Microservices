namespace MembersTeams.Domain.DTOs
{
    public class CreatePlayerTransferDTO
    {
        public long PlayerId { get; set; }
        public string FromClub { get; set; }
        public string ToClub { get; set; }
        public DateTime TransferDate { get; set; }
        public decimal TransferFee { get; set; }
    }
}
