using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public interface IPlayer
    {
        float TraceCurrent { get; set; }
        float TraceMax { get; set; }
        List<IGameCpuChip> AllInstalledChips { get; }
        List<IGameCpu> AllCores { get; }
        List<IGameProcess> AllGameCpuProcesses { get; }
        void UpdateAllRunningProcesses(float deltaTime);
    }
}
