using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Entities
{
    public class PlayerTransfer : BaseEntity
    {
        public long PlayerId { get; set; }
        public Player Player { get; set; }
        public string FromClub { get; set; }
        public string ToClub { get; set; }
        public DateTime TransferDate { get; set; }
        public decimal TransferFee { get; set; }

        public PlayerTransfer()
        {
        }

        public void SetPlayerId(long playerId)
        {
            PlayerId = playerId;
        }

        public void SetFromClub(string fromClub)
        {
            FromClub = fromClub;
        }

        public void SetToClub(string toClub)
        {
            ToClub = toClub;
        }

        public void SetTransferDate(DateTime transferDate)
        {
            TransferDate = transferDate;
        }

        public void SetTransferFee(decimal transferFee)
        {
            TransferFee = transferFee;
        }
    }
}
