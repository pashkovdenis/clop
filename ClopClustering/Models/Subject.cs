using ClopClustering.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClopClustering.Models
{    

    public sealed class Subject <D>
    {
        public IEnumerable<ISubjectAttribute> Attributes { get; private set; } 
        public  D Data { get; private set; }
        public Subject() => Attributes = Enumerable.Empty<ISubjectAttribute>();        
        public Subject(IEnumerable<ISubjectAttribute> attributes) => Attributes = attributes;
        public Subject(D data, IEnumerable<ISubjectAttribute> attributes) => (Data, Attributes) = (data, attributes); 
        public override string ToString() => Data?.ToString() ?? "Empty Subject";
        public ISubjectAttribute GetKey() => Attributes.FirstOrDefault(a => a.IsKeyAttribute);
        public bool Assigned { get; set; } = false;

    } 
    
    
}
