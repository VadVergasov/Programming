namespace Lab6.Entities {
    public class Employee {
        public int ID { get; set; }
        public bool OnVacation { get; set; }
        public string Name { get; set; } = "";

        public Employee(int id, bool onVacation, string name) {
            ID = id;
            OnVacation = onVacation;
            Name = name;
        }

        public override string ToString() {
            return $"ID: {ID}, В отпуске: {OnVacation}, Имя: {Name}";
        }
    }
}
