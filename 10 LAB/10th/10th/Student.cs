
using _10th;
using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;


[Serializable, XmlInclude(typeof(Student))]
public partial class Student : Person, IReportable
{
    [XmlAttribute("ID")]
    public int ID { get; set; }
    [XmlIgnore]
    [JsonIgnore]
    public int[,] Marks { get; set; } = new int[2, 10];
    [XmlIgnore]
    public double AverageMark { get;  set; }
    [XmlAttribute("Misses")]
    public int Misses { get; set; }
    [XmlAttribute("BadStudents")]
    public bool BadStudent { get; set; }

    public Student() { }

    [JsonConstructor]
    public Student(int iD, int[,] marks, double averageMark, int misses, bool badStudent)
    {
        ID = iD;
        Marks = marks;
        AverageMark = averageMark;
        Misses = misses;
        BadStudent = badStudent;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, ID: {ID}, Average Mark: {AverageMark}, Misses: {Misses}, Bad Student: {BadStudent}";
    }

    string IReportable.GenerateReport()
    {
        return $"Name: {Name}, Age: {Age}, ID: {ID}, Average Mark: {AverageMark}, Misses: {Misses}, Matrix of Marks: {PrintMatrix()}, Bad Student: {BadStudent}";
    }

    private string PrintMatrix()
    {
        string result = "";
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                result += Marks[i, j] + " ";
            }
        }
        return result;
    }
}
