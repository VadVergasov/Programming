using Lab.Lab3.Entities;
using SQLite;

namespace Lab.Lab3.Services {
    public class SQLiteService : IDbService {
        private readonly SQLiteConnection Db = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"database.db"),
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache);

        public IEnumerable<Set> GetAllSets() {
            return Db.Table<Set>().ToList();
        }

        public IEnumerable<Sushi> GetSetSushi(int id) {
            return Db.Table<Sushi>().Where(obj => obj.SetId == id).ToList();
        }

        public void Init() {
            Db.DropTable<Set>();
            Db.DropTable<Sushi>();
            Db.CreateTable<Set>();
            Db.Insert(new Set { Cost = 45.99, Name = "Сяке сет" });
            Db.Insert(new Set { Cost = 36.99, Name = "Кунсей сет" });
            Db.Insert(new Set { Cost = 43.99, Name = "Бонсай сет" });
            Db.Insert(new Set { Cost = 48.99, Name = "Викэнд сет" });
            Db.Insert(new Set { Cost = 48.99, Name = "Клаб Хаус сет" });
            Db.Insert(new Set { Cost = 48.99, Name = "Вабисаби сет" });
            Db.CreateTable<Sushi>();
            Db.Insert(new Sushi { Name = "Ролл с лососем", SetId = 1 });
            Db.Insert(new Sushi { Name = "Ролл с лососем сливочным сыром и огурцом", SetId = 1 });
            Db.Insert(new Sushi { Name = "Ролл с лососем сливочным сыром и авокадо", SetId = 1 });
            Db.Insert(new Sushi { Name = "Ролл с лососем и сливочным сыром", SetId = 1 });
            Db.Insert(new Sushi { Name = "Ролл с лососем, луком и соусом Спайси", SetId = 1 });
            Db.Insert(new Sushi { Name = "Ролл в тартаре из копчёного лосося со сливочным сыром огурцом, соусом Цезарь, и чипсами из ржаного хлеба", SetId = 2 });
            Db.Insert(new Sushi { Name = "Ролл в тартаре из копчёного лосося со сливочным с авокадо, цедрой лимона, и жареными каперсами", SetId = 2 });
            Db.Insert(new Sushi { Name = "Ролл в тартаре из копчёного лосося со сливочным салатом айсберг, соусом Спайси", SetId = 2 });
            Db.Insert(new Sushi { Name = "Ролл в тунце со сливочным сыром, салатом айсберг и кунжутным маслом", SetId = 3 });
            Db.Insert(new Sushi { Name = "Ролл в огурце с креветкой в темпуре, сливочным сыром, соусом Терияки и кунжутом", SetId = 3 });
            Db.Insert(new Sushi { Name = "Ролл в сыре чеддер со сливочным сыром, огурцом, цыплёнком Ким чи, соусом Цезарь и картофелем пай", SetId = 3 });
            Db.Insert(new Sushi { Name = "Ролл в лососе со сливочным сыром и огурцом", SetId = 4 });
            Db.Insert(new Sushi { Name = "Ролл в маринованном морском окуне со сливочным сыром, огурцом и кунжутом Ким чи", SetId = 4 });
            Db.Insert(new Sushi { Name = "Ролл с тунцом, со сливочным сыром, яблоком, салатом айсберг, черным перцем и соусом Цезарь", SetId = 4 });
            Db.Insert(new Sushi { Name = "Ролл морских водорослях со сливочным сыром, краб миксом, такуаном, соусом Ореховым и кунжутом", SetId = 4 });
            Db.Insert(new Sushi { Name = "Ролл в огурце со сливочным сыром, мидиями, яблоком, соусом Терияки и кунжутом", SetId = 4 });
            Db.Insert(new Sushi { Name = "Ролл в миксе лосося, окуня и икры Тобико со сливочным сыром и соусом Терияки", SetId = 5 });
            Db.Insert(new Sushi { Name = "Ролл в опаленном беконе с лососем жаренным в соусе Терияки и сливочным сыром", SetId = 5 });
            Db.Insert(new Sushi { Name = "Ролл в сурими с копченым лососем, огурцом, сливочным сыром и соусом Спайси", SetId = 5 });
            Db.Insert(new Sushi { Name = "Ролл в нори с копченым лососем, сливочным сыром, огурцом и соусом Спайси", SetId = 5 });
            Db.Insert(new Sushi { Name = "Ролл в нори с лососем в соусе Терияки, сливочным сыром, огурцом и соусом Спайси", SetId = 5 });
            Db.Insert(new Sushi { Name = "Ролл в огурце с копченым лососем, сливочным сыром, соусом Терияки и кунжутом", SetId = 6 });
            Db.Insert(new Sushi { Name = "Ролл в миксе копченого лосося и морского окуня со сливочным сыром и тобико с сливочным сыром, салатом айсберг, картофелем пай", SetId = 6 });
            Db.Insert(new Sushi { Name = "Ролл в кунжуте с лососем, сливочным сыром, авокадо и огурцом", SetId = 6 });
            Db.Insert(new Sushi { Name = "Ролл в хлопьях тунца с лососем, сливочным сыром и авокадо", SetId = 6 });
        }
    }
}
