using Lab3.Entities;

HMS hms = new();
Journal journal = new();
hms.RatesOrCustomersChanged += journal.RecordChangesInRatesOrCustomers;
hms.CustomerRatesChanged += journal.RecordChangesInCustomerRates;

hms.AddRate(new Rate("Вода", 10));
hms.AddRate(new Rate("Электричество", 23));
hms.AddCustomer(new Customer("Вергасов", "Вадим"));
hms.AddCustomer(new Customer("Щиров", "Павел"));
hms.AddOption(new Customer("Вергасов", "Вадим"), new Rate("Вода", 10));
hms.AddOption(new Customer("Вергасов", "Вадим"), new Rate("Вода", 10));
hms.AddOption(new Customer("Вергасов", "Вадим"), new Rate("Электричество", 23));
hms.AddOption(new Customer("Щиров", "Павел"), new Rate("Вода", 10));

Console.WriteLine($"Услуги в отсортированном по стоимости порядке:");
foreach (var current in hms.GetRatesByCost()) {
    Console.WriteLine(current);
}

Console.WriteLine($"Сумма всех услуг: {hms.GetFullSum()}");

Customer client = new Customer("Щиров", "Павел");
Console.WriteLine($"Сумма услуг клиента {client}: {hms.GetCustomerSum(client)}");

Console.WriteLine($"Больше всего заплатил клиент с именем: {hms.GetCustomerByCost()}");

int sum = 9;
Console.WriteLine($"Количество клиентов с суммой по услугам больше {sum}: {hms.GetCustomersCountWithSumMore(sum)}");

client = new Customer("Вергасов", "Вадим");
Console.WriteLine($"Клиент {client} заплатил по услугам следующие значения:");
foreach (var current in hms.GetCustomerListSum(client)) {
    Console.WriteLine(current.Item1 + " " + current.Item2);
}

