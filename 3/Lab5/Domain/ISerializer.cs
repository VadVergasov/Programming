using Domain.Entities;

namespace Domain {
    public interface ISerializer {
        IEnumerable<Hospital> DeSerializeByLINQ(string fileName);

        IEnumerable<Hospital> DeSerializeXML(string fileName);

        IEnumerable<Hospital> DeSerializeJSON(string fileName);

        void SerializeByLINQ(IEnumerable<Hospital> xxx, string fileName);

        void SerializeXML(IEnumerable<Hospital> xxx, string fileName);

        void SerializeJSON(IEnumerable<Hospital> xxx, string fileName);
    }
}
