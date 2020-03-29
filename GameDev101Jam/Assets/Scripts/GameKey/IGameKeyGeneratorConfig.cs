using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public interface IGameKeyGeneratorConfig
    {
        float GetMinDifficulty();
        float GetMaxDifficulty();
        string[] GetTokenArray();
        string GetWildCardToken();
        int GetMinLength();
        int GetMaxLength();
    }
}
