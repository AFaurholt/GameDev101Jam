using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    //TODO? git gud and make struct
    //TODO! make interface and test suite
    public class GameCpuAllocation
    {
        public GameCpuAllocation(float percentageAlloc, IGameProcess gameProcess)
        {
            PercentageAllocation = percentageAlloc;
            GameProcess = gameProcess;
        }

        public float PercentageAllocation{ get; set; }
        public IGameProcess GameProcess { get; }
    }
}
