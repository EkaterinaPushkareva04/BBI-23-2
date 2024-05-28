
using _10th;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

class Program
{
    static Student GenerateStudent()
    {
        Random rnd = new Random();
        Student student = new Student();
        student.Surname = "L" + rnd.Next(1000, 9999).ToString();
        student.Misses = rnd.Next(0, 42);
        student.Name = "N" + rnd.Next(1000, 9999).ToString();
        student.Age = rnd.Next(18, 42);
        student.BadStudent = Convert.ToBoolean(rnd.Next(0, 1));
        student.ID = rnd.Next(0, 1000);
        int a = rnd.Next(1, 5);
        int b = rnd.Next(1, 10);
        int[,] m = new int[a,b];
        int sum = 0;
        for(int i = 0; i < a; ++i)
        {
            for(int j =  0; j < b; ++j)
            {
                m[i, j] = rnd.Next(1, 5);
                sum += m[i, j];
            }
        }
        student.AverageMark = sum/(a * b);
        student.Marks = m;
        return student;
    }


    static void Main()
    {
        // Создание массива студентов
        Student[] students = new Student[100];
        // Создание массива групп
        Group[] groups = new Group[5];

        // Инициализация и добавление студентов в группы
        for (int i = 0; i < groups.Length; i++)
        {
            groups[i] = new Group { Name = $"Group {i + 1}" };
            for (int j = 0; j < 20; j++)
            {
                groups[i].Students.Add(students[j + i * 20]);
            }
        }

        // Создание массива из 50 студентов
        Student[] studentsRandom = new Student[50];

        // Заполнение групп студентами из массива в случайном порядке
        Random rand = new Random();
        foreach (Student student in studentsRandom.OrderBy(x => rand.Next()))
        {
            Group randomGroup = groups[rand.Next(groups.Length)];
            randomGroup.Students.Add(student);
        }
        string path = @"C:\Users\user\Desktop"; //путь до рабочего стола
        string folderName = "Test";
        path = Path.Combine(path, folderName);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string fileName1 = "raw_data.xml";
        string fileName2 = "raw_data.json";
        string fileName3 = "data.xml";
        string fileName4 = "data.json";
        fileName1 = Path.Combine(path, fileName1);
        fileName2 = Path.Combine(path, fileName2);
        MyXmlSerializer myXmlSerializer = new MyXmlSerializer();

        MyJsonSerializer myJsonSerializer = new MyJsonSerializer(); 

        Group[] newGroups = new Group[3];
        for(int i = 0; i < 3; ++i)
        {
            List<Student> tmp = new List<Student> { };
            for(int j = 0; j < 10 + i * 2; ++j)
            {
                tmp.Add(GenerateStudent());
            }
            newGroups[i] = new Group("Name" + i.ToString(), tmp);
        }

        myXmlSerializer.WriteGroups(newGroups, fileName1);
        myJsonSerializer.WriteGroups(newGroups, fileName2);
        for (int i = 0; i < 3; ++i)
        {
            newGroups[i].SortStudentsByAverageMark();
        }
        myJsonSerializer.WriteGroups(newGroups, fileName4);
        for (int i = 0; i < 3; ++i)
        {
            newGroups[i].SortStudentsByAverageMark();
            newGroups[i].Reverse();
        }
        myXmlSerializer.WriteGroups(newGroups, fileName3);

        var answer = myXmlSerializer.ReadGroups(fileName1);
        foreach(var i in answer)
        {
            Console.WriteLine(i.ToString());
        }
        answer = myJsonSerializer.ReadGroups(fileName2);
        foreach (var i in answer)
        {
            Console.WriteLine(i.ToString());
        }
        Group worst = null;

        if (newGroups[0] < newGroups[1] && newGroups[0] < newGroups[2])
        {
            worst = newGroups[0];
        }
        else if (newGroups[1] < newGroups[0] && newGroups[1] < newGroups[2])
        {
            worst = newGroups[1];
        }
        else
        {
            worst = newGroups[2];
        }
        IReportable ir = worst as IReportable;
        Console.WriteLine();
        Console.WriteLine(ir.GenerateReport());
        foreach(var i in worst.Students)
        {
            Console.WriteLine(i.ToString());
        }
    }
}