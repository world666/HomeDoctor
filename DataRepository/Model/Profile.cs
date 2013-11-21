namespace DataRepository.Model
{
    public class Profile
    {
        public int Id { get; set; }

        public string SkypeLogin { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int? Age { get; set; }

        public decimal Money { get; set; }
    }
}