using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace com.runtime.GameDev101Jam
{
    public class PlayerBehaviour : MonoBehaviour, IPlayer
    {
        IPlayer _playerData = new Player();

        public float TraceCurrent { get => _playerData.TraceCurrent; set => _playerData.TraceCurrent = value; }
        public float TraceMax { get => _playerData.TraceMax; set => _playerData.TraceMax = value; }

        public List<IGameCpuChip> AllInstalledChips => _playerData.AllInstalledChips;

        public List<IGameCpu> AllCores => _playerData.AllCores;

        public List<IGameProcess> AllGameCpuProcesses => _playerData.AllGameCpuProcesses;

        public void UpdateAllRunningProcesses(float deltaTime)
        {
            _playerData.UpdateAllRunningProcesses(deltaTime);
        }

        private void FixedUpdate()
        {
            float fixTime = Time.fixedDeltaTime;
            _playerData.UpdateAllRunningProcesses(fixTime);
        }
    }
}