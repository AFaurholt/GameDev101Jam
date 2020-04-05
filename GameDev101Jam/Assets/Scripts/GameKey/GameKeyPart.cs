using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public class GameKeyPart : IGameKeyPart
    {
        readonly string _actualString;
        float _breakProgress = 0f;

        public string KeyPartString => _actualString;

        public float Progress => _breakProgress;

        public GameKeyPart(string stringPart)
        {
            _actualString = stringPart;
        }

        public bool IsBroken
        {
            get => _breakProgress >= 100f;
        }

        public float AddProgress(float progress)
        {
            return _breakProgress += progress;
        }
    }
}
