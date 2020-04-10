using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using com.runtime.GameDev101Jam;

namespace Tests
{
    public class GameCpuChipShould
    {
        // A Test behaves as an ordinary method
        [Test]
        public void GetCores()
        {
            IGameCpu gameCpu1 = new MockGameCpu();
            IGameCpu gameCpu2 = new MockGameCpu();
            IGameCpuChip sut = new GameCpuChip(gameCpu1, gameCpu2);
            List<IGameCpu> gameCpus = new List<IGameCpu> {
                gameCpu1,
                gameCpu2
            };
            IGameCpuChip sut2 = new GameCpuChip(gameCpus);
            Assert.That(sut.Cores, Is.EqualTo(new List<IGameCpu>() { gameCpu1, gameCpu2 }));
            Assert.That(sut2.Cores, Is.EqualTo(new List<IGameCpu>() { gameCpu1, gameCpu2 }));
        }

        private class MockGameCpu : IGameCpu
        {
            public float Power => throw new System.NotImplementedException();

            public float MaxCapacity => throw new System.NotImplementedException();

            public float CurrentCapacity => throw new System.NotImplementedException();

            public float Hrtz => throw new System.NotImplementedException();

            public List<IGameProcess> GameProcesses => throw new System.NotImplementedException();

            HashSet<GameCpuAllocation> IGameCpu.CpuAllocations => throw new System.NotImplementedException();

            public bool AddAllocation(IGameProcess gameCpuProcess, float percentageAllocated)
            {
                throw new System.NotImplementedException();
            }

            public bool ChangeAllocationPercentage(IGameProcess gameCpuProcess, float value)
            {
                throw new System.NotImplementedException();
            }

            public bool CombineAllocation(IDictionary<IGameProcess, float> allocations)
            {
                throw new System.NotImplementedException();
            }

            public bool CombineAllocation(HashSet<GameCpuAllocation> allocations)
            {
                throw new System.NotImplementedException();
            }

            public float GetPowerForProcess(IGameProcess gameCpuProcess)
            {
                throw new System.NotImplementedException();
            }

            public bool RemoveAllocation(IGameProcess gameCpuProcess)
            {
                throw new System.NotImplementedException();
            }

            public bool TryGetCpuAllocationByProcess(IGameProcess gameProcess, out GameCpuAllocation result)
            {
                throw new System.NotImplementedException();
            }
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        //[UnityTest]
        //public IEnumerator GameCpuChipShouldWithEnumeratorPasses()
        //{
        //    // Use the Assert class to test conditions.
        //    // Use yield to skip a frame.
        //    yield return null;
        //}
    }
}
