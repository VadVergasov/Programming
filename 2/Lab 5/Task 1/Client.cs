namespace Task_1 {
    public class Client {
        private string? surname, name;

        public string? Surname {
            get => surname;
            set => surname = value;
        }

        public string? Name {
            get => name;
            set => name = value;
        }

        public Client() {
            Surname = null;
            Name = null;
        }

        public Client(string surname, string name) {
            Surname = surname;
            Name = name;
        }
    }
}
