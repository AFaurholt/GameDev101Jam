using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public class VertexGraph : IVertexGraph
    {
        public HashSet<IVertex> Vertices { get; } = new HashSet<IVertex>();

        public void AddVertex(IVertex source, IVertex target, bool isOneWay = true)
        {
            if(source != null) Vertices.Add(source);
            if(target != null) Vertices.Add(target);
        }
    }
}
