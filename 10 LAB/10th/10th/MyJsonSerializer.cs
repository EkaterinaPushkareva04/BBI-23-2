using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

public class MyJsonSerializer : MySerializer
{
    public override Group[] ReadGroups(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<Group[]>(fs);

        }
    }
    public override void WriteGroups(Group[] groups, string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fs, groups);
        }
    }
}