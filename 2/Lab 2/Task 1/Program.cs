// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
int operation = 1;
while (operation != 0) {
    Console.Write("Продолжить (1) или закончить (0): ");
    operation = Convert.ToInt32(Console.ReadLine()!);
    if (operation == 0) {
        break;
    }
    Console.Write("Введите два числа в каждой строке: ");
    long m, n;
    m = Convert.ToInt64(Console.ReadLine()!);
    n = Convert.ToInt64(Console.ReadLine()!);
    if (Task1.CanBeDevided(m, n)) {
        Console.WriteLine(Task1.Devide(m, n));
    } else {
        Console.WriteLine("Нельзя поделить нацело!");
    }
}
