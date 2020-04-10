using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public class GameCpu : IGameCpu, IGameProcessHandler
    {
        public GameCpu(float power, float maxCapacity)
        {
            Power = power;
            MaxCapacity = maxCapacity;
        }
        public GameCpu(float power, float maxCapacity, float hrtz) : this(power, maxCapacity)
        {
            Hrtz = hrtz;
        }

        public float Power { get; }
        public float MaxCapacity { get; }
        public float CurrentCapacity => CpuAllocations.Sum(x => x.GameProcess.ProcessCost);
        public float Hrtz { get; }
        public HashSet<GameCpuAllocation> CpuAllocations { get; } = new HashSet<GameCpuAllocation>();

        public List<IGameProcess> GameProcesses =>
            (from alloc in CpuAllocations
             select alloc.GameProcess).ToList();

        public float ExecutionInterval => Hrtz;

        public bool AddAllocation(IGameProcess gameCpuProcess, float percentageAllocated)
        {
            float currentCap = CpuAllocations.Sum(x => x.GameProcess.ProcessCost);

            if (MaxCapacity < currentCap + gameCpuProcess.ProcessCost)
            {
                return false;
            }

            if (!(CpuAllocations.Count > 0))
            {
                return CpuAllocations.Add(new GameCpuAllocation(1f, gameCpuProcess));
            }
            else
            {
                var newAlloc = new GameCpuAllocation(percentageAllocated, gameCpuProcess);
                if (CpuAllocations.Add(newAlloc))
                {
                    ChangeAllAllocationsToFitNewValue(newAlloc.GameProcess, percentageAllocated, 0f);
                    return true;
                }

                return false;
            }
        }
        public bool CombineAllocation(HashSet<GameCpuAllocation> allocations)
        {
            float targetSize = allocations.Sum(x => x.GameProcess.ProcessCost);
            if (MaxCapacity < (CurrentCapacity + targetSize))
            {
                return false;
            }
            if (!(CpuAllocations.Count > 0))
            {
                foreach (var item in allocations)
                {
                    CpuAllocations.Add(item);
                }

            }
            else
            {
                float adjustment = 2f;
                foreach (var item in allocations)
                {
                    if (!CpuAllocations.Add(item))
                    {
                        adjustment -= item.PercentageAllocation;
                    }
                }
                foreach (var item in CpuAllocations)
                {
                    item.PercentageAllocation /= adjustment;
                }
            }

            return true;
        }
        public bool ChangeAllocationPercentage(IGameProcess gameCpuProcess, float value)
        {
            if (TryGetCpuAllocationByProcess(gameCpuProcess, out GameCpuAllocation result))
            {
                ChangeAllAllocationsToFitNewValue(gameCpuProcess, value, result.PercentageAllocation);
                result.PercentageAllocation = value;

                return true;
            }

            return false;
        }
        public float GetPowerForProcess(IGameProcess gameCpuProcess)
        {
            if (TryGetCpuAllocationByProcess(gameCpuProcess, out GameCpuAllocation result))
            {
                return result.PercentageAllocation * Power;
            }

            throw new ArgumentException("IGameProcess not found");
        }
        public bool RemoveAllocation(IGameProcess gameCpuProcess)
        {
            if (TryGetCpuAllocationByProcess(gameCpuProcess, out GameCpuAllocation result))
            {
                CpuAllocations.Remove(result);
                ChangeAllAllocationsToFitNewValue(null, 0, result.PercentageAllocation);
                return true;
            }

            return false;
        }
        private void ChangeAllAllocationsToFitNewValue(IGameProcess process, float newValue, float oldValue)
        {
            foreach (var item in CpuAllocations)
            {
                if (!(item.GameProcess == process))
                {
                    item.PercentageAllocation = ModifyPercentageEvenlyToFitChange(item.PercentageAllocation, oldValue, newValue);
                }
            }
        }
        private float ModifyPercentageEvenlyToFitChange(float changeTarget, float oldValue, float newValue)
        {
            return changeTarget * (1 + ((oldValue - newValue) / (1 - oldValue)));
        }

        //TODO incremental implementation
        public void SubscribeToProcess(IGameProcess gameProcess, GameProcessOption processOption)
        {
            gameProcess.OnCompleteListener += RemoveAllocatedOnComplete;
        }
        public void UnsubscribeToProcess(IGameProcess gameProcess)
        {
            gameProcess.OnCompleteListener -= RemoveAllocatedOnComplete;
        }
        private void RemoveAllocatedOnComplete(IGameProcess caller) { }
        public bool TryGetCpuAllocationByProcess(IGameProcess gameProcess, out GameCpuAllocation result)
        {
            var enumerator = CpuAllocations.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.GameProcess == gameProcess)
                {
                    result = enumerator.Current;
                    return true;
                }
            }

            result = default;
            return false;
        }
    }
}
