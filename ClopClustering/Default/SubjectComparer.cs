using ClopClustering.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClopClustering.Default
{
    public sealed class SubjectComparer : IAttributeComparer
    {
        public float Compare(ISubjectAttribute a, ISubjectAttribute b) => a.Label == b.Label ? 0.33f : 0; 
    }
}
