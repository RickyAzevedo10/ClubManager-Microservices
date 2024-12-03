namespace Identity.Domain.Entities
{
    public class Institution : BaseEntity
    {
        public string Name { get; set; }
        public string? Abbreviation { get; set; }
        public string? Logo { get; set; }
        public DateTime FoundationDate { get; set; }
        public string Address { get; set; }
        public string? Colors { get; set; }
        public string? StadiumName { get; set; }
        public int StadiumCapacity { get; set; }
        public DateTime StadiumInauguration { get; set; }
        public bool HaveTrainingCenter { get; set; }
        public string? TrainingCenterAddress { get; set; }
        public string? OfficialWebsiteUrl { get; set; }
        public string? SocialMediaLinks { get; set; }
        public ICollection<User> User { get; set; } = [];

        public Institution() { }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetAbbreviation(string? abbreviation)
        {
            Abbreviation = abbreviation;
        }

        public void SetLogo(string? logo)
        {
            Logo = logo;
        }

        public void SetFoundationDate(DateTime foundationDate)
        {
            FoundationDate = foundationDate;
        }

        public void SetAddress(string address)
        {
            Address = address;
        }

        public void SetColors(string? colors)
        {
            Colors = colors;
        }

        public void SetStadiumName(string? stadiumName)
        {
            StadiumName = stadiumName;
        }

        public void SetStadiumCapacity(int stadiumCapacity)
        {
            StadiumCapacity = stadiumCapacity;
        }

        public void SetStadiumInauguration(DateTime stadiumInauguration)
        {
            StadiumInauguration = stadiumInauguration;
        }

        public void SetHaveTrainingCenter(bool haveTrainingCenter)
        {
            HaveTrainingCenter = haveTrainingCenter;
        }

        public void SetTrainingCenterAddress(string? trainingCenterAddress)
        {
            TrainingCenterAddress = trainingCenterAddress;
        }

        public void SetOfficialWebsiteUrl(string? officialWebsiteUrl)
        {
            OfficialWebsiteUrl = officialWebsiteUrl;
        }

        public void SetSocialMediaLinks(string? socialMediaLinks)
        {
            SocialMediaLinks = socialMediaLinks;
        }
    }
}
