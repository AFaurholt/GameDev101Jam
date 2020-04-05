using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    class PortEdge
    {
        private PortNode _from;
        private PortNode _to;
        private bool _isOneWay;
        private float _weight;

        public PortNode To
        {
            get { return _to; }
        }
    }
}
