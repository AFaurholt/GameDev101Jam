using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public class PasswordBreaker : IPasswordBreaker
    {
        public HashSet<IGameKey> BreakList { get; } = new HashSet<IGameKey>();

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

        public void RemoveFromBreakList(IGameKey gameKey)
        {
            BreakList.Remove(gameKey);
        }
    }
}
