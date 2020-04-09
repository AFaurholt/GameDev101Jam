using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using com.runtime.GameDev101Jam;

namespace TestsSlow
{
    public class PlayerShould
    {
        MockGameCpuChip mockGameCpuChip;
        MockGameCpuProcess mockProcess1;
        MockGameCpu mockCpu1;

        [SetUp]
        public void SetUp()
        {
            mockGameCpuChip = new MockGameCpuChip();
            mockProcess1 = new MockGameCpuProcess();
            mockCpu1 = new MockGameCpu();

            mockGameCpuChip.Cores.Add(mockCpu1);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        [Repeat(3)]
        public IEnumerator Executes()
        {
            GameObject go = new GameObject();
            go.AddComponent<PlayerBehaviour>();
            PlayerBehaviour playerBehaviour = go.GetComponent<PlayerBehaviour>();

            playerBehaviour.AllInstalledChips.Add(mockGameCpuChip);
            mockCpu1.CpuAllocations.Add(mockProcess1, 0f);

            yield return new WaitForSeconds(3);

            Assert.That(mockProcess1.timeLapse, Is.InRange(2.99f, 3.1f));
        }

        [UnityTest]
        [Repeat(3)]
        public IEnumerator ExecutesStopAfterPause()
        {
            GameObject go = new GameObject();
            go.AddComponent<PlayerBehaviour>();
            PlayerBehaviour playerBehaviour = go.GetComponent<PlayerBehaviour>();

            playerBehaviour.AllInstalledChips.Add(mockGameCpuChip);
            mockCpu1.CpuAllocations.Add(mockProcess1, 0f);

            yield return new WaitForSeconds(1);

            Assert.That(mockProcess1.timeLapse, Is.InRange(0.99f, 1.1f));
            mockProcess1.Pause();

            yield return new WaitForSeconds(1);
            Assert.That(mockProcess1.timeLapse, Is.InRange(0.99f, 1.1f));
        }


    }

    class MockGameCpuChip : IGameCpuChip
    {
        public List<IGameCpu> Cores { get; } = new List<IGameCpu>();
    }
    class MockGameCpuProcess : IGameCpuProcess
    {
        public float timeLapse = 0f;
        public float ProcessCost => throw new System.NotImplementedException();

        public IGameCpu Handler => throw new System.NotImplementedException();

        public bool IsRunning { get; private set; } = true;

        public void Execute(float deltaTime)
        {
            timeLapse += deltaTime;
        }

        public void Pause()
        {
            IsRunning = false;
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }
    }
    class MockGameCpu : IGameCpu
    {
        public float Power => throw new System.NotImplementedException();

        public float MaxCapacity => throw new System.NotImplementedException();

        public float CurrentCapacity => throw new System.NotImplementedException();

        public IDictionary<IGameCpuProcess, float> CpuAllocations { get; } = new Dictionary<IGameCpuProcess, float>();

        public float Hrtz => 1;

        public bool AddAllocation(IGameCpuProcess gameCpuProcess, float percentageAllocated)
        {
            CpuAllocations.Add(gameCpuProcess, 0f);
            return true;
        }

        public bool ChangeAllocationPercentage(IGameCpuProcess gameCpuProcess, float value)
        {
            throw new System.NotImplementedException();
        }

        public bool CombineAllocation(IDictionary<IGameCpuProcess, float> allocations)
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
}
