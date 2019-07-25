using System;
using System.Collections.Generic;
using System.Text;

namespace ClopClustering.Interfaces
{
    public interface ISubjectAttribute
    {
        string Label { get; }
        bool IsKeyAttribute { get;}
        float Weight { get; }   
    }
}
