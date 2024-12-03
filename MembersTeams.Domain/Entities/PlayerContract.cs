using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Entities
{
    public class PlayerContract : BaseEntity
    {
        public long PlayerId { get; set; }
        public Player Player { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Salary { get; set; }
        public string ContractType { get; set; }

        public PlayerContract()
        {
        }

        public void SetPlayerId(long playerId)
        {
            PlayerId = playerId;
        }

        public void SetStartDate(DateTime startDate)
        {
            StartDate = startDate;
        }

        public void SetEndDate(DateTime endDate)
        {
            EndDate = endDate;
        }

        public void SetSalary(decimal salary)
        {
            Salary = salary;
        }

        public void SetContractType(string contractType)
        {
            ContractType = contractType;
        }
    }
}
