using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public class VertexEdge : IVertexEdge
    {
        public VertexEdge(IVertex source, IVertex target)
        {
            Source = source ?? 
                throw new ArgumentNullException("Source was null");
            Target = target ?? 
                throw new ArgumentNullException("Target was null");
        }

        public IVertex Source { get; }
        public IVertex Target { get; }
    }
}
