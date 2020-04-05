using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    //So I have this handler which I'm not really sure what to do with atm but 
    //I'm sure it's there so things can subscribe to when the cracks finish
    public interface IPasswordBreakerHandler
    {
        void RegisterPlayer(IPlayer player);
    }
}
