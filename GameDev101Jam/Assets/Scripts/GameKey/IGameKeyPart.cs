using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public interface IGameKeyPart
    {
        string GetKeyPartString();
        bool IsBroken();
        float AddProgress(float progress);
        float GetProgress();
    }
}
