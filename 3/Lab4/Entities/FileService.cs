using Lab4.Interfaces;

namespace Lab4.Entities {
    internal class FileService : IFileService<Passanger> {
        public IEnumerable<Passanger> ReadFile(string fileName) {
            using BinaryReader reader = new(File.Open(fileName, FileMode.Open));
            while (reader.PeekChar() > -1) {
                yield return new Passanger(reader);
            }
        }

        public void SaveData(IEnumerable<Passanger> data, string fileName) {
            if (File.Exists(fileName)) {
                File.Delete(fileName);
            }
            using BinaryWriter writer = new(File.Open(fileName, FileMode.OpenOrCreate));
            foreach (var item in data) {
                writer.Write(item.ToString());
            }
        }
    }
}
