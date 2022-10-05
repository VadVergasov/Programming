using Lab6.Entities;
using System.Reflection;

var DLL = Assembly.LoadFile(Directory.GetCurrentDirectory() + "\\FileService.dll");

List<Employee> employees = new() {
    new Employee(321, false, "Вадим"),
    new Employee(1, true, "Иван"),
    new Employee(43, false, "Павел"),
    new Employee(1235, false, "Александр"),
    new Employee(654321, true, "Тимофей")
};
var type = DLL.GetType("FileService.FileService");
var SaveData = type.GetMethod("SaveData");
var LoadData = type.GetMethod("ReadFile");
object? service = Activator.CreateInstance(type);

SaveData?.Invoke(service, new object[] { employees, "test.json" });

List<Employee> result = new(LoadData?.Invoke(service, new object[] { "test.json" }) as IEnumerable<Employee>);
foreach(var current in result) {
    Console.WriteLine(current);
}
