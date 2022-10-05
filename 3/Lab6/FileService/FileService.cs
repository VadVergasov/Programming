using Lab6.Entities;
using Lab6.Interfaces;
using System.Text.Json;

namespace FileService {
    public class FileService : IFileService<Employee> {
        public IEnumerable<Employee> ReadFile(string fileName) {
            return JsonSerializer.Deserialize<IEnumerable<Employee>>(File.ReadAllText(fileName))!;
        }

        public void SaveData(IEnumerable<Employee> data, string fileName) {
            File.WriteAllText(fileName, JsonSerializer.Serialize(data));
        }
    }
}
