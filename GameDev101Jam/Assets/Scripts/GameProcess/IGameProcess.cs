using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public delegate void OnComplete(IGameProcess caller);
    public delegate void OnPause(IGameProcess caller);
    public delegate void OnStart(IGameProcess caller);
    public interface IGameProcess
    {
        IGameProcessHandler Handler { get; }
        float ProcessCost { get; }
        bool IsRunning { get; }
        GameProcessOption GameProcessOption { get; set; }
        //bool AddToHandler(THandler handler);

        event OnComplete OnCompleteListener;
        event OnPause OnPauseListener;
        event OnStart OnStartListener;

        void Pause();
        void Start();
        void Execute(float deltaTime);
    }
}
