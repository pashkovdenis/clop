using System;
using System.Collections.Generic;
using System.Text;

namespace ClopClustering.Interfaces
{
    public interface IAttributeComparer
    {
        float Compare(ISubjectAttribute a, ISubjectAttribute b); 
    }
}
