using System;
using System.IO;
using System.Xml.Serialization;

public class MyXmlSerializer : MySerializer
{
    public override Group[] ReadGroups(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Group[]));
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            return (Group[])serializer.Deserialize(fs);

        }
    }
    public override void WriteGroups(Group[] groups, string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Group[]));
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            serializer.Serialize(fs, groups);
        }
    }
}