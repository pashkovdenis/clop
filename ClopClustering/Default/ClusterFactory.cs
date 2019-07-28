using ClopClustering.Interfaces;
using ClopClustering.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ClopClustering.Default
{
    public sealed class ClusterFactory<D> : IClusterFactory
    {
        private readonly IDictionary<string, Subject<D>> Subjects;
        private readonly IAttributeComparer AttributeComparer;
        private readonly List<Cluster<D>> Clusters;
        private readonly int Thresshold;

        public ClusterFactory(IReadOnlyCollection<Subject<D>> data, IAttributeComparer attrComparer, int treshold = 1)
        {
            Subjects = new Dictionary<string, Subject<D>>(data.Count);
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
                             .Select(x => new Cluster<D>(x.Key)));
             
        }








    }
}
