using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public class Vertex : IVertex
    {
        public IDictionary<IVertex, IList<IVertexEdge>> Destinations { get; } = new Dictionary<IVertex, IList<IVertexEdge>>();

        public void AddDestination(IVertex target, IVertexEdge edge)
        {
            if (target == null)
                throw new ArgumentNullException("Target was null");

            if (edge == null)
                throw new ArgumentNullException("Edge was null");

            if (Destinations.Keys.Contains(target))
            {
                Destinations[target].Add(edge);
            }
            else
            {
                Destinations.Add(target, new List<IVertexEdge> { edge });
            }
        }
    }
}
