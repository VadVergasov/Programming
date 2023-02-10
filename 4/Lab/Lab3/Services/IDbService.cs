using Lab.Lab3.Entities;

namespace Lab.Lab3.Services {
    public interface IDbService {

        IEnumerable<Set> GetAllSets();
        IEnumerable<Sushi> GetSetSushi(int id);
        void Init();

    }
}
