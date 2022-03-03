Console.WriteLine("Введите два числа в каждой строке, чтобы узнать целую часть деления их: ");
long first, second;
first = Convert.ToInt64(Console.ReadLine());
second = Convert.ToInt64(Console.ReadLine());
long result = first / second;
Console.WriteLine("Ответ: " + result);
