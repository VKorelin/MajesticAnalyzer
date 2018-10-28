namespace MajesticAnalyzer
{
    public interface IPathProvider
    {
        string HomeDirectory { get; }
        
        string[] ChildDirectories { get; }
    }
}