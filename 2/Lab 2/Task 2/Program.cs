// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
string[] input = new string[1];
while (input.Length != 2) {
    Console.WriteLine("Введите координаты: ");
    input = Console.ReadLine()!.Split(' ');
}
double x = Convert.ToDouble(input[0]), y = Convert.ToDouble(input[1]);
Console.WriteLine(Task2.Location(x, y));
