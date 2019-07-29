using ClopClustering.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClopClustering.Default
{
    public sealed class SubjectComparer : IAttributeComparer
    {  
        public float Compare(ISubjectAttribute a, ISubjectAttribute b) => a.Key.ToLower() == b.Key.ToLower()?  (float) a.Weight * 2    : 0; 
    }
}
