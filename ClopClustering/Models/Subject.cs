using System.Collections.Generic;
using System.Linq;
using ClopClustering.Default.Attributes;

namespace ClopClustering.Models
{
    public sealed class Subject <TD>
    {
        public IEnumerable<SubjectAttribute> Attributes { get; private set; } 
        public  TD Data { get; private set; }
        public Subject() => Attributes = Enumerable.Empty<SubjectAttribute>();        
        public Subject(IEnumerable<SubjectAttribute> attributes) => Attributes = attributes;
        public Subject(TD data, IEnumerable<SubjectAttribute> attributes) => (Data, Attributes) = (data, attributes); 
        public override string ToString() => Data?.ToString() ?? "Empty Subject";
        public SubjectAttribute GetKey() => Attributes.FirstOrDefault(a => a.IsKeyAttribute);
        public bool Assigned { get; set; } = false;
    }
}
