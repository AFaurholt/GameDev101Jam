using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public class GameCpu : IGameCpu
    {
        private Dictionary<IGameCpuProcess, float> _cpuAllocations = new Dictionary<IGameCpuProcess, float>();

        public GameCpu(float power, float maxCapacity)
        {
            Power = power;
            MaxCapacity = maxCapacity;
        }
        public float Power { get; }

        public float MaxCapacity { get; }

        public Dictionary<IGameCpuProcess, float> CpuAllocations => _cpuAllocations;

        public float CurrentCapacity => CpuAllocations.Keys.Sum(x => x.Size);

        public bool AddAllocation(IGameCpuProcess gameCpuProcess, float percentageAllocated)
        {
            float currentCap = 0f;
            foreach (var item in CpuAllocations)
            {
                currentCap += item.Key.Size;
            }

            if (MaxCapacity < currentCap + gameCpuProcess.Size)
            {
                return false;
            }
            else
            {
                if (!(CpuAllocations.Count > 0))
                {
                    CpuAllocations.Add(gameCpuProcess, 1f);
                }
                else
                {
                    ChangeAllAllocationsToFitNewValue(null, percentageAllocated, 0f);
                    CpuAllocations.Add(gameCpuProcess, percentageAllocated);
                }

                return true;
            }
        }
        public bool CombineAllocation(Dictionary<IGameCpuProcess, float> allocations)
        {
            float targetSize = allocations.Keys.Sum(x => x.Size);
            if (MaxCapacity < (CurrentCapacity + targetSize))
            {
                return false;
            }
            else
            {
                if (CpuAllocations.Count > 0)
                {
                    Dictionary<IGameCpuProcess, float> newDicCurrent = 
                        new Dictionary<IGameCpuProcess, float>();
                    Dictionary<IGameCpuProcess, float> newDicCombine = 
                        new Dictionary<IGameCpuProcess, float>();

                    foreach (var item in CpuAllocations)
                    {
                        float tmp = item.Value * (allocations.Values.Sum() / 2);
                        newDicCurrent.Add(item.Key, tmp);
                    }
                    foreach (var item in allocations)
                    {
                        float tmp = item.Value * (CpuAllocations.Values.Sum() / 2);
                        newDicCombine.Add(item.Key, tmp);
                    }
                    _cpuAllocations = newDicCurrent.Combine(newDicCombine);
                }
                else
                {
                    _cpuAllocations = allocations;
                }
                return true;
            }
        }

        public bool ChangeAllocationPercentage(IGameCpuProcess gameCpuProcess, float value)
        {
            if (CpuAllocations.ContainsKey(gameCpuProcess))
            {
                ChangeAllAllocationsToFitNewValue(gameCpuProcess, value, CpuAllocations[gameCpuProcess]);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ChangeAllAllocationsToFitNewValue(IGameCpuProcess gameCpuProcessToFit, float newValue, float oldValue)
        {
            Dictionary<IGameCpuProcess, float> newDic = new Dictionary<IGameCpuProcess, float>();

            foreach (var item in CpuAllocations)
            {
                if (item.Key == gameCpuProcessToFit)
                {
                    newDic.Add(gameCpuProcessToFit, newValue);
                }
                else
                {
                    float tmp = ModifyPercentageEvenlyToFitChange(item.Value, oldValue, newValue);
                    newDic.Add(item.Key, tmp);
                }
            }

            _cpuAllocations = newDic;
        }

        private float ModifyPercentageEvenlyToFitChange(float changeTarget, float oldValue, float newValue)
        {
            return changeTarget * (1 + ((oldValue - newValue) / (1 - oldValue)));
        }

        public float GetPowerForProcess(IGameCpuProcess gameCpuProcess)
        {
            return CpuAllocations[gameCpuProcess] * Power;
        }

        public void RemoveAllocation(IGameCpuProcess gameCpuProcess)
        {
            float percentage = CpuAllocations[gameCpuProcess];
            CpuAllocations.Remove(gameCpuProcess);
            ChangeAllAllocationsToFitNewValue(null, 0, percentage);
        }
    }
}
