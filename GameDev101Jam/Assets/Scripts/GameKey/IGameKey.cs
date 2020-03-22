using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public interface IGameKey
    {
        void IsCracked();
        void Crack(float power);
        string GetPasswordString();
    }
}
