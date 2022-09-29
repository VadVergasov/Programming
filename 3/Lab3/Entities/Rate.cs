namespace Lab3.Entities {
    internal class Rate {
        public string Name { get; set; }
        public int Cost { get; set; }

        public Rate(string name, int cost) {
            Name = name;
            Cost = cost;
        }

        public override bool Equals(object? obj) {
            return obj! is Rate && ((Rate)obj).Name.Equals(Name) && ((Rate)obj).Cost.Equals(Cost);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return $"Название: {Name}, Стоимость: {Cost}";
        }
    }
}
