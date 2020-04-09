using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public class GameCpu : IGameCpu
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
        public IDictionary<IGameCpuProcess, float> CpuAllocations { get; private set; } = new Dictionary<IGameCpuProcess, float>();
        public float CurrentCapacity => CpuAllocations.Keys.Sum(x => x.ProcessCost);
        public float Hrtz { get; }

        public bool AddAllocation(IGameCpuProcess gameCpuProcess, float percentageAllocated)
        {
            float currentCap = 0f;
            foreach (var item in CpuAllocations)
            {
                currentCap += item.Key.ProcessCost;
            }

            if (MaxCapacity < currentCap + gameCpuProcess.ProcessCost)
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
        public bool CombineAllocation(IDictionary<IGameCpuProcess, float> allocations)
        {
            float targetSize = allocations.Keys.Sum(x => x.ProcessCost);
            if (MaxCapacity < (CurrentCapacity + targetSize))
            {
                return false;
            }
            else
            {
                if (CpuAllocations.Count > 0)
                {
                    IDictionary<IGameCpuProcess, float> newDicCurrent =
                        new Dictionary<IGameCpuProcess, float>();
                    IDictionary<IGameCpuProcess, float> newDicCombine =
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
                    CpuAllocations = newDicCurrent.Concat(newDicCombine);
                }
                else
                {
                    CpuAllocations = allocations;
                }
                return true;
            }
        }
        public bool ChangeAllocationPercentage(IGameCpuProcess gameCpuProcess, float value)
        {
            bool result = false;
            if (CpuAllocations.ContainsKey(gameCpuProcess))
            {
                ChangeAllAllocationsToFitNewValue(gameCpuProcess, value, CpuAllocations[gameCpuProcess]);
                result = true;
            }

            return result;
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

            CpuAllocations = newDic;
        }
        private float ModifyPercentageEvenlyToFitChange(float changeTarget, float oldValue, float newValue)
        {
            return changeTarget * (1 + ((oldValue - newValue) / (1 - oldValue)));
        }
    }
}
