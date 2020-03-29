using System.Collections;
using System.Collections.Generic;
using com.runtime.GameDev101Jam;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Linq;
using System.Text;

namespace Tests
{
    public class GameKeyGeneratorShould
    {
        float maxDiff1 = 1f, minDiff1 = 1f;
        int maxLen1 = 1, minLen1 = 1;
        string[] tokensEmpty = new string[] { }, tokensEmptyNullSpot = new string[] { "", "a", null }, tokensEmptySpot = new string[] { "" }, tokensNullSpot = new string[] { null }, tokensLegal = new string[] { "a" }, tokensWildcard = new string[] { "**" };

        string defaultWildcard = "**";

        MockGameKeyGeneratorConfig minDefaultConfig, minWithEmptyNullSpotsConfig, minWithEmptySpotsConfig, minWithNullSpotsConfig, minWithAConfig, minWithWildcardConfig;

        [SetUp]
        public void SetUp()
        {
            minDefaultConfig = new MockGameKeyGeneratorConfig(maxDiff1, minDiff1, maxLen1, minLen1, tokensEmpty, defaultWildcard);
            minWithEmptyNullSpotsConfig = new MockGameKeyGeneratorConfig(maxDiff1, minDiff1, maxLen1, minLen1, tokensEmptyNullSpot, defaultWildcard);

            minWithEmptySpotsConfig = new MockGameKeyGeneratorConfig(maxDiff1, minDiff1, maxLen1, minLen1, tokensEmptySpot, defaultWildcard);
            minWithNullSpotsConfig = new MockGameKeyGeneratorConfig(maxDiff1, minDiff1, maxLen1, minLen1, tokensNullSpot, defaultWildcard);

            minWithAConfig = new MockGameKeyGeneratorConfig(maxDiff1, minDiff1, maxLen1, minLen1, tokensLegal, defaultWildcard);
            minWithWildcardConfig = new MockGameKeyGeneratorConfig(maxDiff1, minDiff1, maxLen1, minLen1, tokensWildcard, defaultWildcard);
        }

        // A Test behaves as an ordinary method
        [Test]
        public void ThrowExceptionIfNoConfigOnGenerate()
        {
            IGameKeyGenerator sut = new GameKeyGenerator();
            Assert.Throws<InvalidOperationException>(() => sut.GenerateGameKey());
        }

        [Test]
        public void ThrowExceptionIfConfigTokensEmptyOnGenerate()
        {
            IGameKeyGenerator sut = new GameKeyGenerator(minDefaultConfig);

            Assert.Throws<InvalidOperationException>(() => sut.GenerateGameKey());
        }

        [Test]
        public void ThrowExceptionIfConfigTokensEmptySpotOnGenerate()
        {
            IGameKeyGenerator sut1 = new GameKeyGenerator(minWithEmptyNullSpotsConfig);
            IGameKeyGenerator sut2 = new GameKeyGenerator(minWithEmptySpotsConfig);
            IGameKeyGenerator sut3 = new GameKeyGenerator(minWithNullSpotsConfig);

            Assert.Throws<InvalidOperationException>(() => sut1.GenerateGameKey());
            Assert.Throws<InvalidOperationException>(() => sut2.GenerateGameKey());
            Assert.Throws<InvalidOperationException>(() => sut3.GenerateGameKey());
        }

        [Test]
        public void ThrowExceptionIfNoConfigOnGetConfig()
        {
            IGameKeyGenerator sut = new GameKeyGenerator();

            Assert.Throws<InvalidOperationException>(() => sut.GetConfig());
        }

        [Test]
        public void ConfigNotNull()
        {
            IGameKeyGenerator sut = new GameKeyGenerator(minWithAConfig);

            Assert.DoesNotThrow(() => sut.GetConfig());
        }

        [Test]
        public void SetConfigCorrectly()
        {
            IGameKeyGenerator sut1 = new GameKeyGenerator();
            IGameKeyGenerator sut2 = new GameKeyGenerator(minDefaultConfig);

            sut1.SetConfig(minDefaultConfig);

            Assert.That(sut1.GetConfig(), Is.EqualTo(minDefaultConfig));
            Assert.That(sut2.GetConfig(), Is.EqualTo(minDefaultConfig));
        }

        [Test]
        public void GenerateKeyA()
        {
            IGameKeyGenerator sut = new GameKeyGenerator(minWithAConfig);

            IGameKey gameKey = sut.GenerateGameKey();

            Assert.That(gameKey.GetPasswordString(), Is.EqualTo("a"));
            Assert.That(gameKey.IsCracked(), Is.False);
            Assert.That(gameKey.GetProgress(), Is.Zero);
        }

        [Test]
        public void GenerateKeyWildCard()
        {
            IGameKeyGenerator sut = new GameKeyGenerator(minWithWildcardConfig);

            IGameKey gameKey = sut.GenerateGameKey();

            bool isFound = false;
            int index = 0;
            while (!isFound && index > gameKey.GetPasswordString().Length)
            {
                if (!minWithWildcardConfig.GetAllTokens().ToString()
                    .Contains(gameKey.GetPasswordString()[index]))
                {
                    isFound = true;
                }

                index++;
            }

            Assert.That(isFound, Is.False);
            Assert.That(gameKey.IsCracked(), Is.False);
            Assert.That(gameKey.GetProgress(), Is.Zero);
        }

        class MockGameKeyGeneratorConfig : IGameKeyGeneratorConfig
        {
            float _maxDiff;
            float _minDiff;
            int _maxLen;
            int _minLen;
            string[] _tokens;
            string _wildcard;

            string[] _allTokens = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "y", "x", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "Y", "X", "Z" };

            public MockGameKeyGeneratorConfig(float maxDiff, float minDiff, int maxLen, int minLen, string[] tokens, string wildcard)
            {
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
}
