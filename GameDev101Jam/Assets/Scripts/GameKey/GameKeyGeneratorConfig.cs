using System;
using System.Text;

namespace com.runtime.GameDev101Jam
{
    public class GameKeyGeneratorConfig : IGameKeyGeneratorConfig
    {
        readonly float _maxDiff, _minDiff;
        readonly int _maxLen, _minLen;
        readonly string[] _tokens, _allTokens = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "y", "x", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "Y", "X", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        readonly string _wildcard;

        public GameKeyGeneratorConfig(float maxDiff, float minDiff, int maxLen, int minLen, string[] tokens, string wildcard)
        {
            if (maxDiff <= 0f || minDiff <= 0f)
            {
                throw new ArgumentException("Difficulty cannot be zero or less");
            }

            if (maxLen <= 0 || minLen <= 0)
            {
                throw new ArgumentException("Length cannot be zero or less");
            }

            if (tokens.Length > 0)
            {
                for (int i = 0; i < tokens.Length; i++)
                {
                    if (string.IsNullOrEmpty(tokens[i]))
                    {
                        throw new ArgumentException("Empty spot in token array");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Tokens cannot be empty array");
            }

            if (string.IsNullOrEmpty(wildcard))
            {
                throw new ArgumentException("Wild card cannot be null or empty");
            }

            _maxDiff = maxDiff;
            _minDiff = minDiff;
            _maxLen = maxLen;
            _minLen = minLen;
            _tokens = tokens;
            _wildcard = wildcard;
        }

        public string[] GetAllTokens()
        {
            return _allTokens;
        }

        public string GetAllTokensAsString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in GetAllTokens())
            {
                sb.Append(item);
            }

            return sb.ToString();
        }

        public float GetMaxDifficulty()
        {
            return _maxDiff;
        }

        public int GetMaxLength()
        {
            return _maxLen;
        }

        public float GetMinDifficulty()
        {
            return _minDiff;
        }

        public int GetMinLength()
        {
            return _minLen;
        }

        public string[] GetTokenArray()
        {
            return _tokens;
        }

        public string GetWildCardToken()
        {
            return _wildcard;
        }
    }
}