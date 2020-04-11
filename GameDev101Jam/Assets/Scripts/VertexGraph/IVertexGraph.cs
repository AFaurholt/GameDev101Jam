using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public interface IVertexGraph
    {
        HashSet<IVertex> Vertices { get; }
        void AddVertex(IVertex source, IVertex target, bool isOneWay = true);
    }
}
