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

            public Dictionary<IGameCpuProcess, float> CpuAllocations => throw new System.NotImplementedException();

            public float CurrentCapacity => throw new System.NotImplementedException();

            public bool AddAllocation(IGameCpuProcess gameCpuProcess, float percentageAllocated)
            {
                throw new System.NotImplementedException();
            }

            public void ChangeAllocation(IGameCpuProcess gameCpuProcess, float percentageChange)
            {
                throw new System.NotImplementedException();
            }

            public bool ChangeAllocationPercentage(IGameCpuProcess gameCpuProcess, float value)
            {
                throw new System.NotImplementedException();
            }

            public bool CombineAllocation(Dictionary<IGameCpuProcess, float> allocations)
            {
                throw new System.NotImplementedException();
            }

            public float GetPowerForProcess(IGameCpuProcess gameCpuProcess)
            {
                throw new System.NotImplementedException();
            }

            public void RemoveAllocation(IGameCpuProcess gameCpuProcess)
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
