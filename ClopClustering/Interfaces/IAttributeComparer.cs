using ClopClustering.Default.Attributes;

namespace ClopClustering.Interfaces
{
    public interface IAttributeComparer
    {
        float Compare(SubjectAttribute a, SubjectAttribute b); 
    }
}
