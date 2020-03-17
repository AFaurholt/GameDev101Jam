using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    class PortScanner
    {
        public void ScanForPorts(PortNode portNode, float cpuStrength, Inventory upgradeInventory, PasswordBreakerBehaviour passwordBreaker)
        {
            if (portNode.IsOpen && !portNode.PlayablePassword.IsBroken)
            {
                portNode.CrackPassword(passwordBreaker, cpuStrength);
            }

            if (portNode.PlayablePassword.IsBroken)
            {
                foreach (PortEdge item in portNode.PortEdges)
                {
                    item.To.MakeVisible();
                }
            }
        }
    }
}
