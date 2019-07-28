using ClopClustering.Interfaces;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ClopClustering.Models
{
    public sealed class Cluster<D>: IEnumerable<Subject<D>> 
    {
        public string Description { get; private set; } 
        public bool IsValidCluster { get;}
        public float Width { get; private set; }
        public float Height { get; private set; }
        private ConcurrentBag<Subject<D>> Subjects { get; } 
        public Cluster(string description)
        {
            Description = description;
            Subjects = new ConcurrentBag<Subject<D>>();
            Width = Height = 1;
        }

        public Cluster(string description, IEnumerable<Subject<D>> subjects) : this(description)
        {


        }

        public override string ToString() => $"Cluster {Description}";
        public IEnumerator<Subject<D>> GetEnumerator() => Subjects.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); 
        public void InsertSubject(Subject<D> subject) => Subjects.Add(subject);
        public void UpdateDimensions(float width, float height) => (Width, Height) = (width, height);
    } 

}
