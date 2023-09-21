﻿namespace Domain.Entities
{
    public class Souvenir
    {
        public int Id;
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public string? Mime { get; set; }
    }
}
