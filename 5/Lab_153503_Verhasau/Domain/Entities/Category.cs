namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public ICollection<Souvenir> Souvenirs { get; } = new List<Souvenir>();
    }
}
