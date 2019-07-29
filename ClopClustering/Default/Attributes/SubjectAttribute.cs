using ClopClustering.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ClopClustering.Default.Attributes
{
    public sealed class SubjectAttribute : ISubjectAttribute
    {
        public SubjectAttribute(string value, float weight = 0.33f, bool isKey = false)
          => (Key, Weight, IsKeyAttribute) = (value, weight, isKey);  
        public bool IsKeyAttribute { get; private set; }

        public float Weight { get; private set; }

        public string Key { get; private set; }

        public override string ToString() => Key;
    }
     
}
