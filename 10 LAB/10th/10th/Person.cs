using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
[Serializable, XmlInclude(typeof(Person))]
public class Person
{
    [XmlAttribute("Surname")]
    public string Surname { get; set; }
    [XmlAttribute("Name")]
    public string Name { get; set; }
    [XmlAttribute("Age")]
    public int Age { get; set; }

    public Person() { }
    [JsonConstructor]
    public Person(string surname, string name, int age)
    {
        Surname = surname;
        Name = name;
        Age = age;
    }

    public override string ToString()
    {
        return $"Surname: {Surname}, Name: {Name}, Age: {Age}";
    }
}
