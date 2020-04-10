using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public interface IGameCpu
    {
        float Power { get; }
        float MaxCapacity { get; }
        float CurrentCapacity { get; }
        float Hrtz { get; }
        HashSet<GameCpuAllocation> CpuAllocations { get; }
        List<IGameProcess> GameProcesses { get; }

        bool AddAllocation(IGameProcess gameCpuProcess, float percentageAllocated);
        bool CombineAllocation(HashSet<GameCpuAllocation> allocations);
        bool RemoveAllocation(IGameProcess gameCpuProcess);
        float GetPowerForProcess(IGameProcess gameCpuProcess);
        bool ChangeAllocationPercentage(IGameProcess gameCpuProcess, float value);
        bool TryGetCpuAllocationByProcess(IGameProcess gameProcess, out GameCpuAllocation result);
    }
}
