using _153503_Verhasau_Lab1_2.Collections;

namespace _153503_Verhasau_Lab1_2.Entities {
    internal class Customer {
        public string Surname { get; set; }
        public string Name { get; set; }

        public Customer(string surname, string name) {
            Surname = surname;
            Name = name;
        }

        public override bool Equals(object? obj) {
            return obj! is Customer && ((Customer)obj).Name.Equals(Name) && ((Customer)obj).Surname.Equals(Surname);
        }

        public override string ToString() {
            return $"Фамилия: {Surname}, Имя: {Name}";
        }
    }
}
