using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public class GameCpuChip : IGameCpuChip
    {
        public GameCpuChip(List<IGameCpu> cores)
        {
            Cores = cores;
        }
        public GameCpuChip(params IGameCpu[] cores)
        {
            foreach (var item in cores)
            {
                Cores.Add(item);
            }
        }
        public List<IGameCpu> Cores { get; } = new List<IGameCpu>();
    }
}
