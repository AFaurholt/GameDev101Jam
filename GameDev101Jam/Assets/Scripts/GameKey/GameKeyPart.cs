using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public class GameKeyPart : IGameKeyPart
    {
        string _actualString;
        float _breakProgress;

        public GameKeyPart(string stringPart)
        {
            _actualString = stringPart;
        }

        public string GetKeyPartString()
        {
            return _actualString;
        }
    }
}
