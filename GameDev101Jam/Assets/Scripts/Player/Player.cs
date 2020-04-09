using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public class Player : IPlayer
    {
        public float TraceCurrent { get; set; }
        public float TraceMax { get; set; }

        public List<IGameCpuProcess> AllGameCpuProcesses
        {
            get
            {
                List<IGameCpuProcess> allProcesses = new List<IGameCpuProcess>();
                foreach (var item in AllCores)
                {
                    allProcesses.AddRange(item.CpuAllocations.Keys);
                }

                return allProcesses;
            }
        }

        public List<IGameCpuChip> AllInstalledChips { get; } = new List<IGameCpuChip>();

        public List<IGameCpu> AllCores
        {
            get
            {
                List<IGameCpu> allCores = new List<IGameCpu>();
                foreach (var item in AllInstalledChips)
                {
                    allCores.AddRange(item.Cores);
                }

                return allCores;
            }
        }

        public void UpdateAllRunningProcesses(float deltaTime)
        {
            foreach (IGameCpuProcess item in AllGameCpuProcesses.Where(x => x.IsRunning))
            {
                item.Execute(deltaTime);
            }
        }
    }
}
