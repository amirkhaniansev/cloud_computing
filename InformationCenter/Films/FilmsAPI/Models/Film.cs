namespace FilmsAPI.Models
{
    public class Film
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Stars { get; set; }

        public string Category { get; set; }

        public string Cinema { get; set; }
        public string CinemaAddress { get; set; }
        public bool IsJam  { get; set; }
    }
}
