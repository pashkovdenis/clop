using ClopClustering.Interfaces;
using ClopClustering.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace ClopClustering.Default
{
    public sealed class ClusterFactory<D> : IClusterFactory, IEnumerable<Cluster<D>>
    {
        private readonly IList<Subject<D>> Subjects;
        private readonly IAttributeComparer AttributeComparer;
        private readonly List<Cluster<D>> Clusters;
        private readonly int Thresshold;

        public ClusterFactory(IReadOnlyCollection<Subject<D>> data, IAttributeComparer attrComparer, int treshold = 1)
        {
            Subjects = new List<Subject<D>>(data);
            Clusters = new List<Cluster<D>>(data.Count);
            Thresshold = treshold;
            AttributeComparer = attrComparer;
            Init(data);
        }

        private void Init(IEnumerable<Subject<D>> data)
        {
            if (data == null || !data.Any())
                throw new InvalidOperationException("Data list is required to init factory");
            if (data.Count() != data.Count(x => x.Attributes.Any(a => a.IsKeyAttribute)))
                throw new InvalidOperationException("Not all data contains key attribute");

            Clusters.AddRange(data.GroupBy(d => d.Attributes.First(a => a.IsKeyAttribute).Label).OrderBy(a => a.Key)
                             .Select(x =>
                             {
                                 var cluster = new Cluster<D>(x.Key);
                                 cluster.InsertSubject(x.FirstOrDefault());
                                 return cluster;
                             }));

        }
         
        public void MakeCLusters()
        {
            Parallel.ForEach(Subjects, s =>
            {
                double delta_max = -1;

                if (!Clusters.Any(c => c.Subjects.Contains(s)))
                {
                    Cluster<D> selectedCluster = null;

                    foreach (var cluster in Clusters)
                    {
                        double delta = GetDelta( cluster, s);

                        if (delta > delta_max || delta_max == -1)
                        {
                            delta_max = delta;
                            selectedCluster = cluster;
                        }

                        selectedCluster.InsertSubject(s);
                        RecalculateClusterDimensions(selectedCluster);
                    }
                }
            });
        }
         
        private double GetDelta(  Cluster<D> cluster, Subject<D> subject)
        { 
            var Width_New = cluster.Width;
            var cluSizeNew = cluster.Subjects.Count() + 1;

            if (!cluster.Subjects.Any(s => CompareSubjects(s, subject)))
            {
                Width_New = Width_New + 1;
            }
             
            return cluSizeNew * 2 / Math.Pow(Width_New, 2) -
                cluster.Subjects.Count() / Math.Pow(cluster.Width, 2);
        }
           
        private void RecalculateClusterDimensions(Cluster<D> cluster)
        {
              

        }
         
        private bool CompareSubjects(Subject<D> a, Subject<D> b)
        {
            var similarity = 0.00; 
            if (ReferenceEquals(a, b))
            {
                similarity += 0.33;
            } 
            similarity  += a.Attributes.Sum(at => b.Attributes.Sum(ba => AttributeComparer.Compare(at, ba))); 
            return (similarity >= Thresshold * 0.33);
        }
         
        public IEnumerator<Cluster<D>> GetEnumerator() => Clusters.GetEnumerator(); 
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
