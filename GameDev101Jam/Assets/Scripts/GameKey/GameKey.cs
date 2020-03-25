using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public class GameKey : IGameKey
    {
        IGameKeyPart[] _gameKeyPartArray;

        public GameKey(IGameKeyPart[] passwordParts)
        {
            _gameKeyPartArray = passwordParts;
        }

        public float AddProgress(float progress)
        {
            throw new NotImplementedException();
        }

        public void Crack(float power)
        {
            throw new NotImplementedException();
        }

        public string GetPasswordString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in _gameKeyPartArray)
            {
                sb.Append(item.GetKeyPartString());
            }

            return sb.ToString();
        }

        public float GetProgress()
        {
            float progress = 0f;
            foreach (var item in _gameKeyPartArray)
            {
                progress += item.GetProgress();
            }
            progress /= _gameKeyPartArray.Length;

            return progress;
        }

        public bool IsCracked()
        {
            return GetProgress() >= 100f;
        }
    }
}
