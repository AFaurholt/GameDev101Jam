using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public interface IPasswordBreaker : IGameCpuProcess
    {
        HashSet<IGameKey> BreakList { get; }
        void AddToBreakList(IGameKey gameKey);
        void AddToBreakList(params IGameKey[] gameKeys);
        void RemoveFromBreakList(IGameKey gameKey);
        void Crack(float power);
    }
}
