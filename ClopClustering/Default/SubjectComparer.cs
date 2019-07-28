using ClopClustering.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClopClustering.Default
{
    public sealed class SubjectComparer : IAttributeComparer
    {
        public bool Compare(ISubjectAttribute a, ISubjectAttribute b) => a.Label == b.Label; 
    }
}
