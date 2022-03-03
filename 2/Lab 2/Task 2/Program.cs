string[] input = new string[1];
while (input.Length != 2) {
    Console.WriteLine("Введите координаты: ");
    input = Console.ReadLine()!.Split(' ');
}
double x = Convert.ToDouble(input[0]), y = Convert.ToDouble(input[1]);
if (x * x + y * y > 3 * 3 && x * x + y * y < 7 * 7) {
    Console.WriteLine("Да");
} else if (x * x + y * y == 3 * 3 || x * x + y * y == 7 * 7) {
    Console.WriteLine("На границе");
} else {
    Console.WriteLine("Нет");
}
