using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public interface IGameProcessHandler
    {
        float ExecutionInterval { get; }

        void SubscribeToProcess(IGameProcess gameProcess, GameProcessOption processOption);
        void UnsubscribeToProcess(IGameProcess gameProcess);
        float GetPowerForProcess(IGameProcess gameProcess);
    }
}
