namespace Domain.Entities {
    public class AdmissionRoom {
        public int ID { get; set; }

        public int NumberOfPatients { get; set; } = 0;

        public AdmissionRoom() { }

        public AdmissionRoom(int id) {
            ID = id;
        }

        public AdmissionRoom(int id, int numberOfPatients) {
            ID = id;
            NumberOfPatients = numberOfPatients;
        }

        public override string ToString() {
            return $"ID: {ID}, Количество пациентов: {NumberOfPatients}";
        }
    }
}
