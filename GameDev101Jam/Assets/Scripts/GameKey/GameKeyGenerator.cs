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
        public GameKeyGenerator(IGameKeyGeneratorConfig config)
        {
            Config = config ?? throw new InvalidOperationException("Config cannot be null");
        }
        public IGameKeyGeneratorConfig Config
        {
            get { return _config; }
            set
            {
                CheckForConfigErrors(value);
                _config = value;
            }
        }

        public IGameKey GenerateGameKey()
        {
            IGameKeyPart[] parts = GenerateGameKeyPartsArray(GenerateLength());

            return new GameKey(parts, GenerateDifficulty());
        }

        private void FillGameKeyPartArrayAtIndex(ref IGameKeyPart[] gameKeyParts, int index)
        {
            string currToken = GenerateToken();
            gameKeyParts[index] = GenerateKeyPart(currToken);
        }
        private void FillGameKeyPartArray(out IGameKeyPart[] gameKeyParts, int length)
        {
            gameKeyParts = new GameKeyPart[length];

            for (int i = 0; i < length; i++)
            {
                FillGameKeyPartArrayAtIndex(ref gameKeyParts, i);
            }
        }
        private IGameKeyPart[] GenerateGameKeyPartsArray(int length)
        {
            FillGameKeyPartArray(out IGameKeyPart[] parts, length);

            return parts;
        }
        private IGameKeyPart GenerateKeyPart(string token)
        {
            return new GameKeyPart(TokenOrAllToken(token));
        }
        private string TokenOrAllToken(string token)
        {
            return IsTokenWildCard(token) ? Config.AllTokens[GenerateAllTokenIndex()] : token;
        }
        private int GenerateAllTokenIndex()
        {
            return Random.Range(0, Config.AllTokens.Count);
        }
        private bool IsTokenWildCard(string token)
        {
            return token == Config.WildCardToken;
        }
        private string GenerateToken()
        {
            int tokenIndex = Random.Range(0, Config.TokenArray.Count);
            return Config.TokenArray[tokenIndex];
        }
        private int GenerateLength()
        {
            return Random.Range(Config.MinLength, Config.MaxLength + 1);
        }
        private float GenerateDifficulty()
        {
            return Random.Range(Config.MinDifficulty, Config.MaxDifficulty);
        }
        private void CheckForConfigErrors(IGameKeyGeneratorConfig config)
        {
            CheckConfigTokenArrayNotEmpty(config);
            CheckConfigTokenArrayHasNoEmptySpots(config);
        }
        private void CheckConfigTokenArrayHasNoEmptySpots(IGameKeyGeneratorConfig config)
        {
            for (int i = 0; i < config.TokenArray.Count; i++)
            {
                CheckIsNullOrEmptyAtTokenArrayIndex(config, i);
            }
        }
        private void CheckIsNullOrEmptyAtTokenArrayIndex(IGameKeyGeneratorConfig config, int index)
        {
            if (string.IsNullOrEmpty(config.TokenArray[index]))
            {
                throw new InvalidOperationException("Empty spot in token array");
            }
        }
        private void CheckConfigTokenArrayNotEmpty(IGameKeyGeneratorConfig config)
        {
            if (!(config.TokenArray.Count > 0))
            {
                throw new InvalidOperationException("Token array cannot be zero length");
            }
        }
    }
}
