using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ClopClustering.Models
{
    public sealed class Cluster<TD>: IEnumerable<Subject<TD>> 
    {
        public string Description { get; private set; }
        public float Width { get; private set; }
        public float Height { get; set; }

        public ConcurrentBag<Subject<TD>> Subjects { get; } 
        public Cluster(string description)
        {
            Description = description;
            Subjects = new ConcurrentBag<Subject<TD>>();
            Width = Height = 1;
        } 
        public Cluster(string description, IEnumerable<Subject<TD>> subjects) : this(description)
        {
            if (subjects != null)
            { 
                foreach (var subject in subjects)
                {
                    Subjects.Add(subject);
                }
            } 
        }

        public override string ToString() => $"Cluster {Description}";
        public IEnumerator<Subject<TD>> GetEnumerator() => Subjects.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); 
        public void InsertSubject(Subject<TD> subject)
        {
            Subjects.Add(subject); 
        }
        public void UpdateDimensions(float width, float height) => (Width, Height) = (width, height);
    } 

}
