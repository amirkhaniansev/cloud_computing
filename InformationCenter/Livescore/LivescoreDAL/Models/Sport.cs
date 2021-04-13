namespace LivescoreDAL.Models
{
    public class Sport : ModelBase
    {
        public int Id { get; set; }

        public int? OriginCountryId { get; set; }

        public string Name { get; set; }

        public string LogoURL { get; set; }
    }
}