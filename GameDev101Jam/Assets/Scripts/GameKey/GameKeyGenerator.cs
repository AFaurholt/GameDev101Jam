using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace com.runtime.GameDev101Jam
{
    public class GameKeyGenerator : IGameKeyGenerator
    {
        IGameKeyGeneratorConfig _config;

        public GameKeyGenerator() { }
        public GameKeyGenerator(IGameKeyGeneratorConfig config)
        {
            Config = config;
        }

        public IGameKeyGeneratorConfig Config
        {
            get
            {
                if (_config == null)
                {
                    throw new InvalidOperationException("Config cannot be null");
                }
                return _config;
            }
            set => _config = value;
        }

        public IGameKey GenerateGameKey()
        {
            IGameKeyGeneratorConfig config = Config;

            if (config.TokenArray.Count > 0)
            {
                for (int i = 0; i < config.TokenArray.Count; i++)
                {
                    if (string.IsNullOrEmpty(config.TokenArray[i]))
                    {
                        throw new InvalidOperationException("Empty spot in token array");
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Token array cannot be zero length");
            }

            int tokenLen = Random.Range(config.MinLength, config.MaxLength + 1);
            IGameKeyPart[] parts = new GameKeyPart[tokenLen];
            IReadOnlyList<string> configTokens = config.TokenArray;
            IReadOnlyList<string> allTokens = config.AllTokens;

            for (int i = 0; i < tokenLen; i++)
            {

                int tokenIndex = Random.Range(0, configTokens.Count);
                string currToken = configTokens[tokenIndex];
                if (currToken == config.WildCardToken)
                {
                    int allIndex = Random.Range(0, allTokens.Count);
                    parts[i] = new GameKeyPart(allTokens[allIndex]);
                }
                else
                {
                    parts[i] = new GameKeyPart(currToken);
                }
            }

            float diff = Random.Range(config.MinDifficulty, config.MaxDifficulty);

            IGameKey gameKey = new GameKey(parts, diff);

            return gameKey;
        }
    }
}
