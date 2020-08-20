using ClopClustering.Default.Attributes;
using ClopClustering.Interfaces;

namespace ClopClustering.Default
{
    public sealed class SubjectComparer : IAttributeComparer
    {  
        public float Compare(SubjectAttribute a, SubjectAttribute b) => a.Key.ToLower() == b.Key.ToLower()?  a.Weight * 2    : 0; 
    }
}
