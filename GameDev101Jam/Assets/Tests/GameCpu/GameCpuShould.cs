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
        List<IGameProcess> gameCpuProcesses1Times10, gameCpuProcesses2Times5;
        HashSet<GameCpuAllocation> gameCpuProcesses1Times10AsAllocation, gameCpuProcesses2Times5AsAllocation;

        [SetUp]
        public void SetUp()
        {
            gameCpuProcesses1Times10 = new List<IGameProcess> {
                new MockCpuProcess(1f),
                new MockCpuProcess(1f),
                new MockCpuProcess(1f),
                new MockCpuProcess(1f),
                new MockCpuProcess(1f),
                new MockCpuProcess(1f),
                new MockCpuProcess(1f),
                new MockCpuProcess(1f),
                new MockCpuProcess(1f),
                new MockCpuProcess(1f)
            };

            gameCpuProcesses2Times5 = new List<IGameProcess> {
                new MockCpuProcess(2f),
                new MockCpuProcess(2f),
                new MockCpuProcess(2f),
                new MockCpuProcess(2f),
                new MockCpuProcess(2f)
            };

            gameCpuProcesses1Times10AsAllocation = new HashSet<GameCpuAllocation>();
            gameCpuProcesses2Times5AsAllocation = new HashSet<GameCpuAllocation>();

            foreach (var item in gameCpuProcesses1Times10)
            {
                gameCpuProcesses1Times10AsAllocation.Add(new GameCpuAllocation(0.1f, item));
            }
            foreach (var item in gameCpuProcesses2Times5)
            {
                gameCpuProcesses2Times5AsAllocation.Add(new GameCpuAllocation(0.2f, item));
            }
        }

        // A Test behaves as an ordinary method
        [Test]
        public void GetCurrentCapacity()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);
            foreach (var item in gameCpuProcesses1Times10)
            {
                gameCpu.AddAllocation(item, 0.1f);
            }

            Assert.That(gameCpu.CurrentCapacity, Is.EqualTo(10f));
        }

        [Test]
        public void AddAllocationFirstItem()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);
            IGameProcess gameCpuProcess = new MockCpuProcess(1f);
            Debug.Log(gameCpu.GameProcesses.Sum(x => x.ProcessCost));
            gameCpu.AddAllocation(gameCpuProcess, 0.1f);
            Debug.Log(gameCpu.GameProcesses.Sum(x => x.ProcessCost));
            Debug.Log(gameCpu.TryGetCpuAllocationByProcess(gameCpuProcess, out GameCpuAllocation result1));
            gameCpu.TryGetCpuAllocationByProcess(gameCpuProcess, out GameCpuAllocation result);
            Assert.That(result.PercentageAllocation, Is.EqualTo(1f));
        }

        [Test]
        public void AddAllocationOverCapacity()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);
            IGameProcess gameCpuProcess = new MockCpuProcess(101f);
            bool actual = gameCpu.AddAllocation(gameCpuProcess, 0.1f);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void CombineAllocationOnEmpty()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);
            gameCpu.CombineAllocation(gameCpuProcesses1Times10AsAllocation);

            Assert.That(gameCpu.CpuAllocations.Sum(x => x.PercentageAllocation), Is.EqualTo(1f));
        }

        [Test]
        public void CombineAllocationOverCapacityOnEmpty()
        {

            IGameCpu gameCpu = new GameCpu(1f, 1f);
            bool actual = gameCpu.CombineAllocation(gameCpuProcesses1Times10AsAllocation);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void CombineAllocationWithExisting()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);
            gameCpu.CombineAllocation(gameCpuProcesses1Times10AsAllocation);
            gameCpu.CombineAllocation(gameCpuProcesses2Times5AsAllocation);

            HashSet<GameCpuAllocation> copy1 = new HashSet<GameCpuAllocation>(gameCpuProcesses1Times10AsAllocation);
            HashSet<GameCpuAllocation> copy2 = new HashSet<GameCpuAllocation>(gameCpuProcesses2Times5AsAllocation);

            foreach (var item in copy1)
            {
                item.PercentageAllocation /= 2;
            }
            foreach (var item in copy2)
            {
                item.PercentageAllocation /= 2;
                copy1.Add(item);
            }

            var expected = copy1;

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

            bool combineResult = gameCpu.CombineAllocation(gameCpuProcesses1Times10AsAllocation);

            float expected = 100f * 0.1f;

            Assert.That(combineResult, Is.True);

            int i = 0;
            foreach (var item in gameCpu.CpuAllocations)
            {
                Assert.That(gameCpu.GetPowerForProcess(item.GameProcess), Is.EqualTo(expected));
                Debug.Log($"{i} success");
                i++;
            }

        }

        [Test]
        public void ChangeAllToFit()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);

            Debug.Log($"count before combine {gameCpu.CpuAllocations.Count}");

            gameCpu.CombineAllocation(gameCpuProcesses1Times10AsAllocation);

            Debug.Log($"Sum of alloc {gameCpu.CpuAllocations.Sum(x => x.PercentageAllocation)}");
            Debug.Log($"Count {gameCpu.CpuAllocations.Count}");

            IGameProcess gameCpuProcess1 = new MockCpuProcess(1f);
            IGameProcess gameCpuProcess2 = new MockCpuProcess(1f);

            gameCpu.AddAllocation(gameCpuProcess1, 0.5f);

            Debug.Log($"Sum of alloc after 1: {gameCpu.CpuAllocations.Sum(x => x.PercentageAllocation)}");
            Debug.Log($"Count {gameCpu.CpuAllocations.Count}");

            foreach (var item in gameCpu.CpuAllocations)
            {
                Debug.Log($"alloc is {item.PercentageAllocation}");
            }

            gameCpu.AddAllocation(gameCpuProcess2, 0.5f);

            Debug.Log($"Sum of alloc after 2: {gameCpu.CpuAllocations.Sum(x => x.PercentageAllocation)}");
            Debug.Log($"Count {gameCpu.CpuAllocations.Count}");

            foreach (var item in gameCpu.CpuAllocations)
            {
                Debug.Log($"alloc is {item.PercentageAllocation}");
            }

            gameCpu.TryGetCpuAllocationByProcess(gameCpuProcess1, out GameCpuAllocation result1);
            gameCpu.TryGetCpuAllocationByProcess(gameCpuProcess2, out GameCpuAllocation result2);

            Assert.That(result1.PercentageAllocation, Is.EqualTo(0.25f), "result1 first check");
            Assert.That(result2.PercentageAllocation, Is.EqualTo(0.5f), "result2 first check");

            bool boolRes = gameCpu.ChangeAllocationPercentage(gameCpuProcess2, 0.25f);

            Debug.Log(boolRes);
            Debug.Log($"Sum of alloc after 3: {gameCpu.CpuAllocations.Sum(x => x.PercentageAllocation)}");
            Debug.Log($"Count {gameCpu.CpuAllocations.Count}");

            foreach (var item in gameCpu.CpuAllocations)
            {
                Debug.Log($"alloc is {item.PercentageAllocation}");
            }

            Assert.That(result1.PercentageAllocation, Is.EqualTo(0.375f), "result1 2nd check");
            Assert.That(result2.PercentageAllocation, Is.EqualTo(0.25f), "result2 2nd check");
        }

        [Test]
        public void ChangeAllToFitNoContain()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);
            gameCpu.CombineAllocation(gameCpuProcesses1Times10AsAllocation);
            IGameProcess gameCpuProcess1 = new MockCpuProcess(1f);
            IGameProcess gameCpuProcess2 = new MockCpuProcess(1f);

            gameCpu.AddAllocation(gameCpuProcess1, 0.5f);

            bool changeResult = gameCpu.ChangeAllocationPercentage(gameCpuProcess2, 0.5f);

            Assert.That(changeResult, Is.False);
        }

        [Test]
        public void RemoveProcess()
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f);

            gameCpu.CombineAllocation(gameCpuProcesses1Times10AsAllocation);

            IGameProcess gameCpuProcess1 = new MockCpuProcess(1f);

            foreach (var item in gameCpu.CpuAllocations)
            {
                Debug.Log(item.PercentageAllocation);
            }

            gameCpu.AddAllocation(gameCpuProcess1, 0.5f);

            Debug.Log("After add");
            foreach (var item in gameCpu.CpuAllocations)
            {
                Debug.Log(item.PercentageAllocation);
            }

            gameCpu.RemoveAllocation(gameCpuProcess1);

            bool actual = gameCpu.TryGetCpuAllocationByProcess(gameCpuProcess1, out _);

            Debug.Log("after remove");
            foreach (var item in gameCpu.CpuAllocations)
            {
                Debug.Log(item.PercentageAllocation);
            }

            Assert.That(actual, Is.False);

            foreach (var item in gameCpu.CpuAllocations)
            {
                Assert.That(item.PercentageAllocation, Is.EqualTo(0.1f));
            }
        }

        [Test]
        public void GetHrtz([Values(0f, 1f, float.MaxValue)]float hrtz)
        {
            IGameCpu gameCpu = new GameCpu(100f, 100f, hrtz);

            Assert.That(gameCpu.Hrtz, Is.EqualTo(hrtz));
        }

        private class MockCpuProcess : IGameProcess
        {
            public MockCpuProcess(float size)
            {
                ProcessCost = size;
            }

            public float ProcessCost { get; }
            public bool IsRunning => throw new NotImplementedException();
            public GameProcessOption GameProcessOption { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public IGameProcessHandler Handler => throw new NotImplementedException();

            public event OnComplete OnCompleteListener;
            public event OnPause OnPauseListener;
            public event OnStart OnStartListener;

            public void Execute(float power)
            {
                throw new System.NotImplementedException();
            }
            public void Pause()
            {
                throw new NotImplementedException();
            }
            public void Start()
            {
                throw new NotImplementedException();
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
