using System.Security.Cryptography;

namespace Lab3.Entities {
    internal class Customer {
        public string Surname { get; set; }
        public string Name { get; set; }
        public List<Rate> Rates { get; }

        public Customer(string surname, string name) {
            Surname = surname;
            Name = name;
            Rates = new List<Rate>();
        }

        public void AddRate(Rate rate) {
            Rates.Add(rate);
        }

        public override bool Equals(object? obj) {
            return obj! is Customer && ((Customer)obj).Name.Equals(Name) && ((Customer)obj).Surname.Equals(Surname);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return $"Фамилия: {Surname}, Имя: {Name}";
        }
    }
}
