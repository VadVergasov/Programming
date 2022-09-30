namespace Lab4.Interfaces {
    internal interface IFileService<T> {
        IEnumerable<T> ReadFile(string fileName);

        void SaveData(IEnumerable<T> data, string fileName);
    }
}
