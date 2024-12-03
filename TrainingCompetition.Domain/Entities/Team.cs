namespace TrainingCompetition.Domain.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public bool Female { get; set; }
        public bool Male { get; set; }
        public long ExternalTeamId { get; set; }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetFemale(bool female)
        {
            Female = female;
        }

        public void SetMale(bool male)
        {
            Male = male;
        }

        public void SetExternalTeamId(long externalTeamId)
        {
            ExternalTeamId = externalTeamId;
        }
    }
}
