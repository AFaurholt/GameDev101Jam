using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using com.runtime.GameDev101Jam;
using System.Linq;
using System;

namespace Tests
{
    public class GameCpuShould
    {
        List<IGameCpuProcess> gameCpuProcesses1Times5, gameCpuProcesses2Times5;
        Dictionary<IGameCpuProcess, float> gameCpuProcesses1Times5AsAllocation, gameCpuProcesses2Times5AsAllocation;

        [SetUp]
        public void SetUp()
        {
            gameCpuProcesses1Times5 = new List<IGameCpuProcess> {
                new MockCpuProcess(1f),
                new MockCpuProcess(1f),
                new MockCpuProcess(1f),
                new MockCpuProcess(1f),
                new MockCpuProcess(1f)
            };

            gameCpuProcesses2Times5 = new List<IGameCpuProcess> {
                new MockCpuProcess(2f),
                new MockCpuProcess(2f),
                new MockCpuProcess(2f),
                new MockCpuProcess(2f),
                new MockCpuProcess(2f)
            };

            gameCpuProcesses1Times5AsAllocation = new Dictionary<IGameCpuProcess, float>();
            gameCpuProcesses2Times5AsAllocation = new Dictionary<IGameCpuProcess, float>();

            foreach (var item in gameCpuProcesses1Times5)
            {
                gameCpuProcesses1Times5AsAllocation.Add(item, 0.2f);
            }
            foreach (var item in gameCpuProcesses2Times5)
            {
                gameCpuProcesses2Times5AsAllocation.Add(item, 0.2f);
            }
        }

        // A Test behaves as an ordinary method
        [Test]
        public void GetCurrentCapacity()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);
            foreach (var item in gameCpuProcesses1Times5)
            {
                gameCpu.AddAllocation(item, 0.1f);
            }

            Assert.That(gameCpu.CurrentCapacity, Is.EqualTo(5f));
        }

        [Test]
        public void AddAllocationFirstItem()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);
            IGameCpuProcess gameCpuProcess = new MockCpuProcess(1f);
            gameCpu.AddAllocation(gameCpuProcess, 0.1f);

            Assert.That(gameCpu.CpuAllocations[gameCpuProcess], Is.EqualTo(1f));
        }

        [Test]
        public void AddAllocationOverCapacity()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);
            IGameCpuProcess gameCpuProcess = new MockCpuProcess(101f);
            bool actual = gameCpu.AddAllocation(gameCpuProcess, 0.1f);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void CombineAllocationOnEmpty()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);
            gameCpu.CombineAllocation(gameCpuProcesses1Times5AsAllocation);

            Assert.That(gameCpu.CpuAllocations.Values.Sum(), Is.EqualTo(1f));
        }

        [Test]
        public void CombineAllocationOverCapacityOnEmpty()
        {

            IGameCpu gameCpu = new GameCpu(1f, 1f);
            bool actual = gameCpu.CombineAllocation(gameCpuProcesses1Times5AsAllocation);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void CombineAllocationWithExisting()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);
            gameCpu.CombineAllocation(gameCpuProcesses1Times5AsAllocation);
            gameCpu.CombineAllocation(gameCpuProcesses2Times5AsAllocation);

            Dictionary<IGameCpuProcess, float> copy1 =
                new Dictionary<IGameCpuProcess, float>();
            Dictionary<IGameCpuProcess, float> copy2 =
                new Dictionary<IGameCpuProcess, float>();
            foreach (var item in gameCpuProcesses1Times5)
            {
                copy1.Add(item, 0.1f);
            }
            foreach (var item in gameCpuProcesses2Times5)
            {
                copy2.Add(item, 0.1f);
            }

            Dictionary<IGameCpuProcess, float> expected = copy1;
            expected.Combine(copy2);

            Assert.That(gameCpu.CpuAllocations, Is.EqualTo(expected));
        }

        [Test]
        public void GetPower([Values(1f, float.MaxValue, 100f)]float power)
        {
            IGameCpu gameCpu = new GameCpu(power, 1f);

            Assert.That(gameCpu.Power, Is.EqualTo(power));
        }

        [Test]
        public void GetPowerForAllocation()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);
            bool combineResult = gameCpu.CombineAllocation(gameCpuProcesses1Times5AsAllocation);
            float expected = 100f * 0.2f;

            Assert.That(combineResult, Is.True);

            int i = 0;
            foreach (var item in gameCpu.CpuAllocations)
            {
                Assert.That(gameCpu.GetPowerForProcess(item.Key), Is.EqualTo(expected));
                Debug.Log($"{i} success");
                i++;
            }

        }

        [Test]
        public void ChangeAllToFit()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);
            gameCpu.CombineAllocation(gameCpuProcesses1Times5AsAllocation);
            IGameCpuProcess gameCpuProcess1 = new MockCpuProcess(1f);
            IGameCpuProcess gameCpuProcess2 = new MockCpuProcess(1f);

            gameCpu.AddAllocation(gameCpuProcess1, 0.5f);
            gameCpu.AddAllocation(gameCpuProcess2, 0.5f);

            Assert.That(gameCpu.CpuAllocations[gameCpuProcess1], Is.EqualTo(0.25f));
            Assert.That(gameCpu.CpuAllocations[gameCpuProcess2], Is.EqualTo(0.5f));

            gameCpu.ChangeAllocationPercentage(gameCpuProcess2, 0.25f);

            Assert.That(gameCpu.CpuAllocations[gameCpuProcess1], Is.EqualTo(0.375f));
            Assert.That(gameCpu.CpuAllocations[gameCpuProcess2], Is.EqualTo(0.25f));
        }

        [Test]
        public void ChangeAllToFitNoContain()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);
            gameCpu.CombineAllocation(gameCpuProcesses1Times5AsAllocation);
            IGameCpuProcess gameCpuProcess1 = new MockCpuProcess(1f);
            IGameCpuProcess gameCpuProcess2 = new MockCpuProcess(1f);

            gameCpu.AddAllocation(gameCpuProcess1, 0.5f);

            bool changeResult = gameCpu.ChangeAllocationPercentage(gameCpuProcess2, 0.5f);

            Assert.That(changeResult, Is.False);
        }

        [Test]
        public void RemoveProcess()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);
            gameCpu.CombineAllocation(gameCpuProcesses1Times5AsAllocation);
            IGameCpuProcess gameCpuProcess1 = new MockCpuProcess(1f);

            gameCpu.AddAllocation(gameCpuProcess1, 0.5f);
            gameCpu.RemoveAllocation(gameCpuProcess1);

            bool actual = gameCpu.CpuAllocations.ContainsKey(gameCpuProcess1);

            Assert.That(actual, Is.False);

            foreach (var item in gameCpu.CpuAllocations)
            {
                Assert.That(item.Value, Is.EqualTo(0.2f));
            }
        }

        private class MockCpuProcess : IGameCpuProcess
        {
            public MockCpuProcess(float size)
            {
                Size = size;
            }

            public float Size { get; }
            public void Execute(float power)
            {
                throw new System.NotImplementedException();
            }
        }
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        //[UnityTest]
        //public IEnumerator GameCpuShouldWithEnumeratorPasses()
        //{
        //    // Use the Assert class to test conditions.
        //    // Use yield to skip a frame.
        //    yield return null;
        //}
    }
}
