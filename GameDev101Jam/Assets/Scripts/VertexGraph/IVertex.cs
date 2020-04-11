using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public interface IVertex
    {
        IDictionary<IVertex, IList<IVertexEdge>> Destinations { get; }

        void AddDestination(IVertex target, IVertexEdge edge);
    }
}
