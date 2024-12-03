namespace MembersTeams.Domain.DTOs
{
    public class PlayerTransferResponseDTO
    {
        public long Id { get; set; }
        public long PlayerId { get; set; }
        public string FromClub { get; set; }
        public string ToClub { get; set; }
        public DateTime TransferDate { get; set; }
        public decimal TransferFee { get; set; }
    }
}
