namespace MembersTeams.Domain.DTOs
{
    public class UpdatePlayerContractDTO
    {
        public long? Id { get; set; }
        public long PlayerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Salary { get; set; }
        public string ContractType { get; set; }
    }
}
