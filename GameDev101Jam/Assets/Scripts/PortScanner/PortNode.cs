using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    class PortNode
    {
        private float _techLevel;
        private string _portName;
        private bool _isOpen;
        private bool _isVisible;
        private PlayablePassword _playablePassword;
        private HashSet<PortEdge> _outgoingPortEdges;

        public PortNode()
        {
            _outgoingPortEdges = new HashSet<PortEdge>();
        }
        public bool IsOpen
        {
            get { return _isOpen; }
            set { _isOpen = value; }
        }

        public PlayablePassword PlayablePassword
        {
            get { return _playablePassword; }
        }

        public HashSet<PortEdge> PortEdges
        {
            get { return _outgoingPortEdges; }
            set { _outgoingPortEdges = value; }
        }

        public void CrackPassword(PasswordBreakerBehaviour passwordBreaker, float cpuStrength)
        {
            passwordBreaker.CrackPassword(ref _playablePassword, cpuStrength, null);
        }

        public void MakeVisible()
        {
            _isVisible = true;
        }
    }
}
