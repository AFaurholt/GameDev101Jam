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
            SetConfig(config);
        }

        public IGameKey GenerateGameKey()
        {
            IGameKeyGeneratorConfig config = GetConfig();

            if (config.GetTokenArray().Length > 0)
            {
                for (int i = 0; i < config.GetTokenArray().Length; i++)
                {
                    if (string.IsNullOrEmpty(config.GetTokenArray()[i]))
                    {
                        throw new InvalidOperationException("Empty spot in token array");
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Token array cannot be zero length");
            }

            int tokenLen = Random.Range(config.GetMinLength(), config.GetMaxLength() + 1);
            IGameKeyPart[] parts = new GameKeyPart[tokenLen];
            string[] configTokens = config.GetTokenArray();
            string[] allTokens = config.GetAllTokens();

            for (int i = 0; i < tokenLen; i++)
            {

                int tokenIndex = Random.Range(0, configTokens.Length);
                string currToken = configTokens[tokenIndex];
                if (currToken == config.GetWildCardToken())
                {
                    int allIndex = Random.Range(0, allTokens.Length);
                    parts[i] = new GameKeyPart(allTokens[allIndex]);
                }
                else
                {
                    parts[i] = new GameKeyPart(currToken);
                }
            }

            float diff = Random.Range(config.GetMinDifficulty(), config.GetMaxDifficulty());

            IGameKey gameKey = new GameKey(parts, diff);

            return gameKey;
        }

        public IGameKeyGeneratorConfig GetConfig()
        {
            if (_config == null)
            {
                throw new InvalidOperationException("Config not set");
            }
            return _config;
        }

        public void SetConfig(IGameKeyGeneratorConfig config)
        {
            _config = config;
        }
    }
}
