using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public interface IGameKeyGeneratorConfig
    {
        float MinDifficulty { get; }
        float MaxDifficulty { get; }
        IReadOnlyList<string> TokenArray { get; }
        string WildCardToken { get; }
        IReadOnlyList<string> AllTokens { get; }
        int MinLength { get; }
        int MaxLength { get; }

        string GetAllTokensAsString();
    }
}
