using ClopClustering.Interfaces;

namespace ClopClustering.Default
{
    public sealed class SubjectComparer : IAttributeComparer
    {  
        public float Compare(ISubjectAttribute a, ISubjectAttribute b) => a.Key.ToLower() == b.Key.ToLower()?  a.Weight * 2    : 0; 
    }
}
