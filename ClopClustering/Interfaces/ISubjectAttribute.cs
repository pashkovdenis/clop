namespace ClopClustering.Interfaces
{
    public interface ISubjectAttribute
    {
        string Key { get; }
        bool IsKeyAttribute { get;}
        float Weight { get; }   
    }
}
