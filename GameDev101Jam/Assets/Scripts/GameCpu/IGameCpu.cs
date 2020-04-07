﻿using System;
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
        Dictionary<IGameCpuProcess, float> CpuAllocations { get; }
        bool AddAllocation(IGameCpuProcess gameCpuProcess, float percentageAllocated);
        bool CombineAllocation(Dictionary<IGameCpuProcess, float> allocations);
        void RemoveAllocation(IGameCpuProcess gameCpuProcess);
        float GetPowerForProcess(IGameCpuProcess gameCpuProcess);
        bool ChangeAllocationPercentage(IGameCpuProcess gameCpuProcess, float value);
    }
}
