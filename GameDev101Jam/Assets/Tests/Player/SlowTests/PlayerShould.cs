using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using com.runtime.GameDev101Jam;
using System.Linq;

namespace TestsSlow
{
    public class PlayerShould
    {
        static MockGameCpuChip mockGameCpuChip;
        static MockGameCpuProcess mockProcess1;
        static MockGameCpu mockCpu1;

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
            IPlayer playerBehaviour = go.GetComponent<PlayerBehaviour>();
            playerBehaviour.AllInstalledChips.Add(mockGameCpuChip);

            mockCpu1.AddAllocation(mockProcess1, 1f);

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
            mockCpu1.CpuAllocations.Add(new GameCpuAllocation(0f, mockProcess1));

            yield return new WaitForSeconds(1);

            Assert.That(mockProcess1.timeLapse, Is.InRange(0.99f, 1.1f));
            mockProcess1.Pause();

            yield return new WaitForSeconds(1);
            Assert.That(mockProcess1.timeLapse, Is.InRange(0.99f, 1.1f));
        }

        class MockGameCpuChip : IGameCpuChip
        {
            List<IGameCpu> _cores = new List<IGameCpu>();
            public List<IGameCpu> Cores
            {
                get
                {
                    return _cores;
                }
            }
        }
        class MockGameCpuProcess : IGameProcess
        {
            public float timeLapse = 0f;
            public float ProcessCost => throw new System.NotImplementedException();
            public bool IsRunning { get; private set; } = true;
            public GameProcessOption GameProcessOption { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

            IGameProcessHandler IGameProcess.Handler => throw new System.NotImplementedException();

            public event OnComplete OnCompleteListener;
            public event OnPause OnPauseListener;
            public event OnStart OnStartListener;

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
            HashSet<GameCpuAllocation> _allocations = new HashSet<GameCpuAllocation>();

            public float Power => throw new System.NotImplementedException();
            public float MaxCapacity => throw new System.NotImplementedException();
            public float CurrentCapacity => throw new System.NotImplementedException();
            public float Hrtz => 1;
            public List<IGameProcess> GameProcesses
            {
                get
                {
                    List<IGameProcess> gameProcesses = new List<IGameProcess>();

                    foreach (var item in CpuAllocations)
                    {
                        gameProcesses.Add(item.GameProcess);
                    }

                    return gameProcesses;
                }
            }

            public HashSet<GameCpuAllocation> CpuAllocations
            {
                get
                {
                    return _allocations;
                }
            }

            public bool AddAllocation(IGameProcess gameCpuProcess, float percentageAllocated)
            {
                bool result = _allocations.Add(new GameCpuAllocation(percentageAllocated, gameCpuProcess));

                return true;
            }

            public bool ChangeAllocationPercentage(IGameProcess gameCpuProcess, float value)
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
    }


}
