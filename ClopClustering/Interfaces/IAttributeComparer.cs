using System;
using System.Collections.Generic;
using System.Text;

namespace ClopClustering.Interfaces
{
    public interface IAttributeComparer
    {
        bool Compare(ISubjectAttribute a, ISubjectAttribute b); 
    }
}
