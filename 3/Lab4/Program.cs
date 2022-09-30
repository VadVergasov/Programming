using Lab4.Entities;

List<Passanger> passangers = new List<Passanger>(){
new Passanger(11276, true, "Вадим"),
new Passanger(99935, true, "Павел"),
new Passanger(94461, false, "Тимофей"),
new Passanger(12246, true, "Александр"),
new Passanger(46533, false, "Олег")
};

FileService fs = new();
fs.SaveData(passangers, "passangers.bin");
try {
    File.Move("passangers.bin", "data.bin");
} catch (IOException) {
    File.Delete("data.bin");
    File.Move("passangers.bin", "data.bin");
}

List<Passanger> passangers1 = new List<Passanger>(fs.ReadFile("data.bin"));

Console.WriteLine("Прочитанный файл:");
foreach(var current in passangers1) {
    Console.WriteLine(current);
}

Console.WriteLine("Отсортированные по имени пассажиры:");
foreach(var current in passangers1.OrderBy(x => x, new MyCustomComparer())) {
    Console.WriteLine(current);
}

Console.WriteLine("Отсортированные по ID пассажиры:");
foreach (var current in passangers1.OrderBy(x => x.ID)) {
    Console.WriteLine(current);
}
