using System.Collections.Generic;

namespace ClopClustering.Interfaces
{
    public interface IClusterFactory<TD>
    {
        IReadOnlyCollection<Models.Cluster<TD>> GetClusters();
        IEnumerator<Models.Cluster<TD>> GetEnumerator();
        void MakeClusters();
    }

}
