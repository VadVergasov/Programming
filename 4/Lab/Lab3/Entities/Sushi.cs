using SQLite;

namespace Lab.Lab3.Entities {

    [Table("Sushi")]
    public class Sushi {
        [PrimaryKey, AutoIncrement, Indexed]
        public int Id { get; set; }
        [NotNull, Unique]
        public string Name { get; set; } = "";
        [Indexed]
        public int SetId { get; set; }
    }

}
