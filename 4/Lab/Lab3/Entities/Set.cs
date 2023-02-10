using SQLite;

namespace Lab.Lab3.Entities {

    [Table("Sets")]
    public class Set {
        [PrimaryKey, AutoIncrement, Indexed]
        public int Id { get; set; }
        [NotNull, Unique]
        public string Name { get; set; } = "";
        [NotNull]
        public double Cost { get; set; }
    }

}
