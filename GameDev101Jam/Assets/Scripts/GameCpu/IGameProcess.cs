using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public interface IGameProcess<THandler>
    {
        THandler Handler { get; }
        float ProcessCost { get; }
        bool IsRunning { get; }
        //bool AddToHandler(THandler handler);
        void Pause();
        void Start();
        void Execute(float deltaTime);
    }
}
