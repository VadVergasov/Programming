namespace Lab4.Entities {
    internal class Passanger {
        public int ID;
        public bool Active;
        public string Name;

        public Passanger() {
            Name = "";
        }

        public Passanger(int id, bool active, string name) {
            ID = id;
            Active = active;
            Name = name;
        }

        public Passanger(BinaryReader reader) {
            var buf = reader.ReadString();
            string[] data = buf.Split(", ");
            ID = int.Parse(data[0]);
            if (data[1] == "Активный") {
                Active = true;
            } else {
                Active = false;
            }
            Name = data[2];
        }

        public override string ToString() {
            return $"{ID}, {(Active ? "Активный" : "Неактивный")}, {Name}";
        }
    }
}
