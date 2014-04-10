namespace IPAdmin.Models
{
    public class PatentCountry
    {
        public int Id { get; set; }
        public Patent Patent { get; set; }
        public Country Country { get; set; }
        public string SearchCode { get; set; }
    }
}