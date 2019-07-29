using ClopClustering.Interfaces;
using ClopClustering.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Concurrent;

namespace ClopClustering.Default
{
    public sealed class ClusterFactory<D> : IClusterFactory<D>, IEnumerable<Cluster<D>>
    {
        public const float Multiplier = 0.43f;
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

            Clusters.AddRange(data.GroupBy(d => d.GetKey().Key).OrderBy(a => a.Key)
                             .Select(x =>
                             {
                                 var cluster = new Cluster<D>(x.Key);
                                 cluster.InsertSubject(x.FirstOrDefault());
                                 return cluster;
                             })); 
        }
         
        public void MakeClusters()
        { 
            Parallel.ForEach(Subjects, s =>
            {
                double delta_max = -1; 

                    if (!s.Assigned)
                    {
                        Cluster<D> selectedCluster = Clusters.First();
                        foreach (var cluster in Clusters)
                            {
                                var delta = GetDelta(cluster, s);

                                if (delta > delta_max)
                                {
                                    delta_max = delta;
                                    selectedCluster = cluster;
                                }
                            }
                        s.Assigned = true;
                        selectedCluster.InsertSubject(s);
                        RecalculateClusterDimensions(selectedCluster);
                    }
            });
        }

        public IReadOnlyCollection<Cluster<D>> GetClasters() => Clusters.AsReadOnly();
         
        #region Calculations 

        private double GetDelta(Cluster<D> cluster, Subject<D> subject)
        { 
            var width = cluster.Width;
            var height = cluster.Subjects.Count + 1;

            if (!cluster.Subjects.Any(s => CompareSubjects(s, subject)))
            {
                width += Thresshold;
            }

            return height * 2 / Math.Pow(width, 2) 
                    - cluster.Subjects.Count / Math.Pow(cluster.Width, 2);
        }
        
        private void RecalculateClusterDimensions(Cluster<D> cluster)
        {
            var occurencies = cluster.Subjects.GroupBy(s => s.GetKey()).Select(s => (s.Key, s.Count()));
            var width = occurencies.Count();
            var height = occurencies.Sum(s => s.Item2) / (float)width ;
            cluster.UpdateDimensions(width, height );
        }

        private bool CompareSubjects(Subject<D> a, Subject<D> b)
        {
            double similarity = 0; 
            similarity  += a.Attributes.Sum(at => b.Attributes.Sum(ba => AttributeComparer.Compare(at, ba)));  
            return similarity >= Thresshold * Multiplier;
        }

        #endregion
        public IEnumerator<Cluster<D>> GetEnumerator() => Clusters.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
