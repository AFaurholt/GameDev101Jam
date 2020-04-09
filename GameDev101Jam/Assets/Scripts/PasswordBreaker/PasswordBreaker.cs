using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public class PasswordBreaker : IPasswordBreaker
    {
        float _timeLapse = 0f;

        public PasswordBreaker(float size)
        {
            ProcessCost = size;
        }

        public HashSet<IGameKey> BreakList { get; } = new HashSet<IGameKey>();
        public IGameCpu Handler { get; private set; }
        public float ProcessCost { get; }
        public bool IsRunning { get; private set; } = false;

        public void AddToBreakList(IGameKey gameKey)
        {
            BreakList.Add(gameKey);
        }
        public void AddToBreakList(params IGameKey[] gameKeys)
        {
            foreach (var item in gameKeys)
            {
                AddToBreakList(item);
            }
        }
        public void Crack(float power)
        {
            foreach (var item in BreakList)
            {
                item.Crack(power);
            }
        }
        public void Execute(float deltaTime)
        {
            _timeLapse += deltaTime;
            CrackIfTimeLapseReached();
        }
        public void Pause()
        {
            IsRunning = false;
        }
        public void RemoveFromBreakList(IGameKey gameKey)
        {
            BreakList.Remove(gameKey);
        }
        public void Start()
        {
            IsRunning = true;
        }

        private void CrackIfTimeLapseReached()
        {
            if (IsTimeLapseReached())
            {
                Crack(Handler.GetPowerForProcess(this));
                RewindTimeLapse();
            }
        }
        private bool IsTimeLapseReached()
        {
            return _timeLapse >= Handler.Hrtz;
        }
        private void RewindTimeLapse()
        {
            _timeLapse -= Handler.Hrtz;
        }
    }
}
