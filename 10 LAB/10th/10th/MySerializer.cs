public abstract class MySerializer
{
    public abstract Group[] ReadGroups(string filePath);
    public abstract void WriteGroups(Group[] groups, string filePath);
}
