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

        public List<IGameProcess> AllGameCpuProcesses
        {
            get
            {
                List<IGameProcess> gameProcesses = new List<IGameProcess>();

                foreach (var item in AllCores)
                {
                    gameProcesses.AddRange(item.GameProcesses);
                }

                return gameProcesses;
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
            foreach (IGameProcess item in AllGameCpuProcesses.Where(x => x.IsRunning))
            {
                item.Execute(deltaTime);
            }
        }
    }
}
