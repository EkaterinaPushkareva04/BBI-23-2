using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

abstract class Task
{
    protected string text;
    public string Text
    {
        get => text;
        protected set => text = value;
    }
    public Task(string text)
    {
        this.text = text;
    }
}

class Task1 : Task
{
    [JsonConstructor]
    public Task1(string text) : base(text) { }
    public override string ToString()
    {
        return $"{ChangeWord(text)}";
    }
    private string ChangeWord(string str)
    {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];

            if (Char.IsLetter(c))
            {
                if (i % 2 == 0)
                {
                    result.Append(Char.ToUpper(c));
                }
                else
                {
                    result.Append(Char.ToLower(c));
                }
            }
            else
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }

    class Task2 : Task
    {
        [JsonConstructor]
        public Task2(string text) : base(text) { }
        public override string ToString()
        {
            return $"{ShiftLettersToLeft(text)}";
        }

        private string ShiftLettersToLeft(string text)
        {
            string shiftedText = "";


            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    char shiftedChar = (char)(c - 10);
                    if (char.IsLower(c) && shiftedChar < 'a')
                    {
                        shiftedChar = (char)(shiftedChar + 33);
                    }
                    else if (char.IsUpper(c) && shiftedChar < 'A')
                    {
                        shiftedChar = (char)(shiftedChar + 33);
                    }
                    shiftedText += shiftedChar;
                }
                else
                {
                    shiftedText += c;
                }
            }

            return shiftedText;
        }


        class Json
        {
            public static void Write<T>(T obj, string filePath)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    JsonSerializer.Serialize(fs, obj);
                }
            }
            public static T Read<T>(string filePath)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    return JsonSerializer.Deserialize<T>(fs);
                }
                return default(T);
            }
        }
        class Program
        {
            static void Main()
            {
                Console.Write("\tВывод:\n");
                string text = "В чаще на полянке спит барсук, рядом белочка ищет орешек.";
                Task[] tasks = { new Task1(text), new Task2(text) };
                Console.WriteLine(tasks[0]);
                Console.WriteLine(tasks[1]);

                string path = @"C:\Users\m2304549\Desktop\Test";
                string folderName = "Test";
                path = Path.Combine(path, folderName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string FileName1 = "task_1.json";
                string FileName2 = "task_2.json";
                Console.Write("\n\tДесереализованные файлы:\n");
                FileName1 = Path.Combine(path, FileName1);
                FileName2 = Path.Combine(path, FileName2);

                if (!File.Exists(FileName1))
                {
                    Json.Write<Task1>((Task1)tasks[0], FileName1);
                    Json.Write<Task2>((Task2)tasks[1], FileName2);
                }
                else
                {
                    var t1 = Json.Read<Task1>(FileName1);
                    var t2 = Json.Read<Task2>(FileName2);
                    Console.WriteLine(t1);
                    Console.WriteLine(t2);
                }
            }
        }
    }
}




