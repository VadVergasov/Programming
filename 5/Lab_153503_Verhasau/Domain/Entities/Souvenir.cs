namespace Domain.Entities
{
    public class Souvenir
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public string? Mime { get; set; }
    }
}
