using System;
using System.Collections.Generic;
using System.Text;

namespace ClopClustering.Interfaces
{
    public interface IClusterFactory<D>
    {
        IReadOnlyCollection<Models.Cluster<D>> GetClasters();
        IEnumerator<Models.Cluster<D>> GetEnumerator();
        void MakeClusters();
    }

}
