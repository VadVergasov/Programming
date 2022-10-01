using Domain.Entities;

List<Hospital> hospitals = new() {
    new Hospital("Test", new List<AdmissionRoom>(){
        new AdmissionRoom(1, 123),
        new AdmissionRoom(2, 3)
    }),
    new Hospital("Another", new List<AdmissionRoom>() {
        new AdmissionRoom(14, 10)
    }),
    new Hospital("Empty"),
    new Hospital("Больница №10", new List<AdmissionRoom>() {
        new AdmissionRoom(51, 43),
        new AdmissionRoom(23, 12),
        new AdmissionRoom(100, 32)
    }),
    new Hospital("Больниица №1", new List<AdmissionRoom>() {
        new AdmissionRoom(4, 1)
    })
};

Serializer serializer = new();

serializer.SerializeByLINQ(hospitals, "test-linq.xml");
serializer.SerializeJSON(hospitals, "test.json");
serializer.SerializeXML(hospitals, "test.xml");

Console.WriteLine("LINQ:");
foreach (var current in serializer.DeSerializeByLINQ("test-linq.xml")) {
    Console.WriteLine(current.ToString());
}

Console.WriteLine("\nJSON:");
foreach (var current in serializer.DeSerializeJSON("test.json")) {
    Console.WriteLine(current.ToString());
}

Console.WriteLine("\nXML:");
foreach (var current in serializer.DeSerializeXML("test.xml")) {
    Console.WriteLine(current.ToString());
}
