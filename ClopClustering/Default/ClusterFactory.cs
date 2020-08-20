using ClopClustering.Interfaces;
using ClopClustering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace ClopClustering.Default
{
    public sealed class ClusterFactory<TD> : IClusterFactory<TD>, IEnumerable<Cluster<TD>>
    {
        private const float Multiplier = 0.43f;
        private readonly IList<Subject<TD>> _subjects;
        private readonly IAttributeComparer _attributeComparer;
        private readonly List<Cluster<TD>> _clusters;
        private readonly int _threshold;

        public ClusterFactory(IReadOnlyCollection<Subject<TD>> data, IAttributeComparer attrComparer, int treshold = 1)
        {
            _subjects = new List<Subject<TD>>(data);
            _clusters = new List<Cluster<TD>>(data.Count);
            _threshold = treshold;
            _attributeComparer = attrComparer;
            Init(data);
        }

        private void Init(IReadOnlyCollection<Subject<TD>> data)
        {
            if (data == null || !data.Any())
                throw new InvalidOperationException("Data list is required to init factory");

            if (data.Count() != data.Count(x => x.Attributes.Any(a => a.IsKeyAttribute)))
                throw new InvalidOperationException("Not all data contains key attribute");

            _clusters.AddRange(data.GroupBy(d => d.GetKey().Key).OrderBy(a => a.Key)
                             .Select(x =>
                             {
                                 var cluster = new Cluster<TD>(x.Key);
                                 cluster.InsertSubject(x.FirstOrDefault());
                                 return cluster;
                             })); 
        }
         
        public void MakeClusters()
        { 
            Parallel.ForEach(_subjects, s =>
            {
                double deltaMax = -1;
                 
                    if (!s.Assigned)
                    {
                        Cluster<TD> selectedCluster = _clusters.First();
                        foreach (var cluster in _clusters)
                        {
                            var delta = GetDelta(cluster, s);

                            if (delta > deltaMax)
                            {
                                deltaMax = delta;
                                selectedCluster = cluster;
                            }
                        }

                        s.Assigned = true;
                        selectedCluster.InsertSubject(s);
                        RecalculateClusterDimensions(selectedCluster);
                    }
               
            });
        }

        public IReadOnlyCollection<Cluster<TD>> GetClusters() => _clusters.AsReadOnly();
         
        #region Calculations 

        private double GetDelta(Cluster<TD> cluster, Subject<TD> subject)
        { 
            var width = cluster.Width;
            var height = cluster.Subjects.Count + 1; 
            if (!cluster.Subjects.Any(s => CompareSubjects(s, subject)))
            {
                width += _threshold;
            } 
            return height * 2 / Math.Pow(width, 2) 
                    - cluster.Subjects.Count / Math.Pow(cluster.Width, 2);
        }
        
        private void RecalculateClusterDimensions(Cluster<TD> cluster)
        {
            var occurence = cluster.Subjects
                .GroupBy(s => s.GetKey()).Select(s => (s.Key, s.Count())).ToList();
            var width = occurence.Count();
            var height = occurence.Sum(s => s.Item2) / (float)width ;
            cluster.UpdateDimensions(width, height );
        }

        private bool CompareSubjects(Subject<TD> a, Subject<TD> b)
        {
            double similarity = 0; 
            similarity  += a.Attributes.Sum(at => b.Attributes.Sum(ba => _attributeComparer.Compare(at, ba)));  
            return similarity >= _threshold * Multiplier;
        }

        #endregion


        public IEnumerator<Cluster<TD>> GetEnumerator() => _clusters.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
