
using _10th;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

[Serializable, XmlInclude(typeof(Group))]
public class Group : ICountable, IReportable
{
    public Group() { }
    [JsonConstructor]
    public Group(string name, List<Student> students)
    {
        Name = name;
        Students = students;
    }

    [XmlAttribute("Name")]
    public string Name { get; set; }
    [XmlArray("List of students")]
    public List<Student> Students { get; set; } = new List<Student>();

    public override string ToString()
    {
        return $"Group: {Name}. Number of Students: {Students.Count}";
    }

    public void SortStudentsByAverageMark()
    {
        Students = Students.OrderBy(s => s.AverageMark).ToList();
    }

    public void Reverse()
    {
        Students.Reverse();
    }
    string IReportable.GenerateReport()
    {
        double averageMarkGroup = Students.Count > 0 ? Students.Sum(s => s.AverageMark) / Students.Count : 0;
        double averageMisses = Students.Count > 0 ? Students.Sum(s => s.Misses) / Students.Count : 0;
        int failingStudents = Students.Count(s => s.AverageMark < 3.5);
        double failingPercentage = Students.Count > 0 ? ((double)failingStudents / Students.Count) * 100 : 0;

        return $"Group: {Name}, Number of Students: {Students.Count}, Average Mark: {averageMarkGroup}, Average Misses per Student: {averageMisses}, Number of Failing Students: {failingStudents}, Percentage of Failing Students: {failingPercentage}%.";
    }
    int ICountable.Count()
    {
        return Students.Count;
    }

    int ICountable.Count(double mark)
    {
        return Students.Count(s => s.AverageMark == mark);
    }

    public static bool operator <(Group a, Group b) {
        for(int i = 1; i <= 5; ++i)
        {
            if(a.Students.Count(s => s.AverageMark == i) <
                b.Students.Count(s => s.AverageMark == i))
            {
                return true;
            }
        }
        return false;
    }

    public static bool operator >(Group a, Group b)
    {
        for (int i = 1; i <= 5; ++i)
        {
            if (a.Students.Count(s => s.AverageMark == i) <
                b.Students.Count(s => s.AverageMark == i))
            {
                return false;
            }
        }
        return true;
    }
    int ICountable.Percentage(double from, double to)
    {
        return Students.Count(s => s.AverageMark >= from && s.AverageMark <= to);
    }
}