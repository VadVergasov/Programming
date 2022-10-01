using Domain;
using Domain.Entities;
using System;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;

public class Serializer : ISerializer {
    public IEnumerable<Hospital> DeSerializeByLINQ(string fileName) {
        XDocument document = XDocument.Load(fileName);
        return from hospital in document.Element("hospitals")!.Descendants("hospital")
               select new Hospital(hospital.Attribute("name")!.Value,
                                   from admission in hospital.Element("admissions")!.Descendants("admission")
                                   select new AdmissionRoom(Convert.ToInt32(admission.Attribute("id")!.Value),
                                                            Convert.ToInt32(admission.Element("number_of_patients")!.Value)));
    }

    public IEnumerable<Hospital> DeSerializeJSON(string fileName) {
        return JsonSerializer.Deserialize<IEnumerable<Hospital>>(File.ReadAllText(fileName))!;
    }

    public IEnumerable<Hospital> DeSerializeXML(string fileName) {
        XmlSerializer xmlSerializer = new(typeof(List<Hospital>));
        using (FileStream fs = new(fileName, FileMode.Open)) {
            return xmlSerializer.Deserialize(fs)! as List<Hospital>;
        }
    }

    public void SerializeByLINQ(IEnumerable<Hospital> hospitals, string fileName) {
        XDocument document = new();
        XElement all = new("hospitals", from hospital in hospitals
                                        select new XElement("hospital",
                                                            new XAttribute("name", hospital.Name),
                                                            new XElement("admissions",
                                                            from admission in hospital.Admissions
                                                            select new XElement("admission",
                                                                                new XAttribute("id", admission.ID),
                                                                                new XElement("number_of_patients", admission.NumberOfPatients)))));
        document.Add(all);
        document.Save(fileName);
    }

    public void SerializeJSON(IEnumerable<Hospital> hospitals, string fileName) {
        File.WriteAllText(fileName, JsonSerializer.Serialize(hospitals));
    }

    public void SerializeXML(IEnumerable<Hospital> hospitals, string fileName) {
        XmlSerializer xmlSerializer = new(typeof(List<Hospital>));
        using (FileStream fs = new(fileName, FileMode.OpenOrCreate)) {
            xmlSerializer.Serialize(fs, hospitals);
        }
    }
}

