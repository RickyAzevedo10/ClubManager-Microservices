namespace Financial.Domain.DTOs
{
    public class EntityResponseDTO
    {
        public long Id { get; set; }
        public bool Internal { get; set; }
        public bool External { get; set; }
        public int? ClubMemberId { get; set; }
        public int? PlayerId { get; set; }
        public string EntityType { get; set; }
        public string EntityName { get; set; }
    }
}
