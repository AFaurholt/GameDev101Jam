using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using com.runtime.GameDev101Jam;

namespace Tests
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


        // A Test behaves as an ordinary method
        [Test]
        public void AllocateProcess()
        {
            Player sut = new Player();
            sut.AllInstalledChips.Add(mockGameCpuChip);
            mockCpu1.AddAllocation(mockProcess1, 0.1f);

            Assert.That(sut.AllGameCpuProcesses,
                Is.EqualTo(new List<IGameCpuProcess> { mockProcess1 }));
        }

        [Test]
        public void TraceTests([Values(0f, 1f, float.MaxValue)]float trace)
        {
            Player sut = new Player
            {
                TraceCurrent = trace,
                TraceMax = trace
            };

            Assert.That(sut.TraceCurrent, Is.EqualTo(trace));
            Assert.That(sut.TraceMax, Is.EqualTo(trace));
        }


        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.

        class MockGameCpuChip : IGameCpuChip
        {
            public List<IGameCpu> Cores { get; } = new List<IGameCpu>();
        }
        class MockGameCpuProcess : IGameCpuProcess
        {
            public float timeLapse = 0f;
            public float ProcessCost => throw new System.NotImplementedException();

            public IGameCpu Handler => throw new System.NotImplementedException();

            public bool IsRunning => true;

            public void Execute(float deltaTime)
            {
                timeLapse += deltaTime;
            }

            public void Pause()
            {
                throw new System.NotImplementedException();
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

            public bool CombineAllocation(Dictionary<IGameCpuProcess, float> allocations)
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
}
