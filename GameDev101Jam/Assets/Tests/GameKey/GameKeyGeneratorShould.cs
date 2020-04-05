using System.Collections;
using System.Collections.Generic;
using com.runtime.GameDev101Jam;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Tests
{
    public class GameKeyGeneratorShould
    {
        private readonly float maxDiff1 = 1f;
        private readonly float minDiff1 = 1f;
        private readonly int maxLen1 = 1;
        private readonly int minLen1 = 1;
        private readonly string[] tokensEmpty = Array.Empty<string>();
        private readonly string[] tokensEmptyNullSpot = new string[] { "", "a", null };
        private readonly string[] tokensEmptySpot = new string[] { "" };
        private readonly string[] tokensNullSpot = new string[] { null };
        private readonly string[] tokensLegal = new string[] { "a" };
        private readonly string[] tokensWildcard = new string[] { "**" };
        readonly string defaultWildcard = "**";

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
        public void SetConfigCorrectly()
        {
            IGameKeyGenerator sut1 = new GameKeyGenerator();
            IGameKeyGenerator sut2 = new GameKeyGenerator(minDefaultConfig);

            sut1.Config = minDefaultConfig;

            Assert.That(sut1.Config, Is.EqualTo(minDefaultConfig));
            Assert.That(sut2.Config, Is.EqualTo(minDefaultConfig));
        }

        [Test]
        public void GenerateKeyA()
        {
            IGameKeyGenerator sut = new GameKeyGenerator(minWithAConfig);

            IGameKey gameKey = sut.GenerateGameKey();

            Assert.That(gameKey.PasswordString, Is.EqualTo("a"));
            Assert.That(gameKey.IsCracked, Is.False);
            Assert.That(gameKey.Progress, Is.Zero);
        }

        [Test]
        public void GenerateKeyWildCard()
        {
            IGameKeyGenerator sut = new GameKeyGenerator(minWithWildcardConfig);

            IGameKey gameKey = sut.GenerateGameKey();

            bool isFound = false;
            int index = 0;
            while (!isFound && index > gameKey.PasswordString.Length)
            {
                if (!minWithWildcardConfig.AllTokens.ToString()
                    .Contains(gameKey.PasswordString[index]))
                {
                    isFound = true;
                }

                index++;
            }

            Assert.That(isFound, Is.False);
            Assert.That(gameKey.IsCracked, Is.False);
            Assert.That(gameKey.Progress, Is.Zero);
        }

        class MockGameKeyGeneratorConfig : IGameKeyGeneratorConfig
        {
            readonly float _maxDiff;
            readonly float _minDiff;
            readonly int _maxLen;
            readonly int _minLen;
            readonly string[] _tokens;
            readonly string _wildcard;
            readonly string[] _allTokens = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "y", "x", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "Y", "X", "Z" };

            public MockGameKeyGeneratorConfig(float maxDiff, float minDiff, int maxLen, int minLen, string[] tokens, string wildcard)
            {
                _maxDiff = maxDiff;
                _minDiff = minDiff;
                _maxLen = maxLen;
                _minLen = minLen;
                _tokens = tokens;
                _wildcard = wildcard;
            }

            public float MinDifficulty => _minDiff;

            public float MaxDifficulty => _maxDiff;

            public IReadOnlyList<string> TokenArray => new ReadOnlyCollection<string>(_tokens);

            public string WildCardToken => _wildcard;

            public IReadOnlyList<string> AllTokens => new ReadOnlyCollection<string>(_allTokens);

            public int MinLength => _minLen;

            public int MaxLength => _maxLen;

            public string GetAllTokensAsString()
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in AllTokens)
                {
                    sb.Append(item);
                }

                return sb.ToString();
            }
        }


    }
}
