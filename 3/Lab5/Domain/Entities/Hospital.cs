using System.Text.Json.Serialization;

namespace Domain.Entities {
    public class Hospital {
        public string Name { get; set; } = "";

        public List<AdmissionRoom> Admissions { get; init; } = new();

        public Hospital() { }

        public Hospital(string name) {
            Name = name;
        }

        public Hospital(string name, IEnumerable<AdmissionRoom> admissions) {
            Name = name;
            Admissions = admissions.ToList();
        }

        public void AddAdmissionRoom(AdmissionRoom admission) {
            Admissions.Add(admission);
        }

        public override string ToString() {
            return $"Название: {Name}, Приемные отделения: {string.Join(", ", Admissions)}";
        }
    }
}
