using ClopClustering.Interfaces;
using ClopClustering.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ClopClustering.Default
{
    public sealed class ClusterFactory<D>: IClusterFactory
    {
        private readonly IDictionary<string, Subject<D>> Subjects;
        private readonly IAttributeComparer AttributeComparer;
        private readonly IList<Cluster<D>> Clusters;
        private readonly int Thresshold;

        public ClusterFactory(IReadOnlyCollection<Subject<D>> data, 
                              IAttributeComparer attrComparer,
                              int treshold = 1)
        {
            Subjects = new Dictionary<string, Subject<D>>(data.Count);
            Clusters = new List<Cluster<D>>(data.Count);
            Thresshold = treshold;
            AttributeComparer = attrComparer;
            Init(data);
        }
  
        private void Init(IEnumerable<Subject<D>> data)
        {

        }
        

         
    }
}
