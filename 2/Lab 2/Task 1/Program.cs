int operation = 1;
while (operation != 0) {
    Console.Write("Продолжить (1) или закончить (0): ");
    operation = Convert.ToInt32(Console.Read()!);
    if (operation == 0) {
        break;
    }
    Console.Write("Введите число: ");
    int number = Convert.ToInt32(Console.ReadLine()!);
    if (number % 2 == 0) {
        Console.WriteLine("Четное");
    } else {
        Console.WriteLine("Нечетное");
    }
}
