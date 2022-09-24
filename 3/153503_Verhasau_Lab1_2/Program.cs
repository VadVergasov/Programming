using _153503_Verhasau_Lab1_2.Entities;

HMS hms = new();
Journal journal = new();
hms.RatesOrCustomersChanged += journal.RecordChangesInOrdersOrClientsList;
hms.OptionAdded += (sender, e) => Console.WriteLine($"Сообщение: {e.Message}. От {sender?.GetType().Name}\n");
try {
    hms.AddRate(new Rate("Вода", 10));
    hms.AddRate(new Rate("Электричество", 23));
    hms.AddCustomer(new Customer("Вергасов", "Вадим"));
    hms.AddCustomer(new Customer("Щиров", "Павел"));
    hms.AddOption(new Customer("Вергасов", "Вадим"), new Rate("Вода", 10));
    hms.AddOption(new Customer("Вергасов", "Вадим"), new Rate("Электричество", 23));
    hms.AddOption(new Customer("Щиров", "Павел"), new Rate("Вода", 10));
    Console.WriteLine("Стоимость услуг Щиров: " + hms.getSum(new Customer("Щиров", "Павел")));
    Console.WriteLine("Стоимость услуг Вергасов: " + hms.getSum(new Customer("Вергасов", "Вадим")));
    Console.WriteLine("Стоимость всех услуг: " + hms.getFullSum());

    hms.RemoveCustomer(new Customer("Вергасов", "Вадим"));

    hms.AddOption(new Customer("Власенко", "Тимофей"), new Rate("Электричество", 23)); // Нет такого клиента

    hms.RemoveCustomer(new Customer("Власенко", "Тимофей")); // Нет такого клиента
} catch(Exception error) {
    Console.WriteLine($"Тип исключения: {error.GetType().Name}");
    Console.WriteLine(error.Message);
    Console.WriteLine(error.StackTrace);
}
journal.PrintRegisteredEvents();
