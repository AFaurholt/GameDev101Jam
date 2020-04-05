using System;
using System.Collections;
using System.Collections.Generic;
using com.runtime.GameDev101Jam;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameKeyGeneratorConfigShould
    {
        readonly float minDiff = 1, maxDiff = 1;
        readonly int minLen = 1, maxLen = 1;
        readonly string[] tokens = new string[] { "AAAA", "a", "123", "abc" };
        readonly string wildcard = "**";
        static readonly string[] allTokens = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "y", "x", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "Y", "X", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        readonly string allTokensString = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWYXZ1234567890";
#pragma warning disable IDE0051 // Remove unused private members
        static IEnumerable<Tuple<string, int>> GetTestCasesAllTokens()
#pragma warning restore IDE0051 // Remove unused private members
        {
            for (int i = 0; i < allTokens.Length; i++)
            {
                yield return new Tuple<string, int>(allTokens[i], i);
            }
        }

        [Test]
        public void GetDifficulty([Values(1f, 2.5f, 10000000f)]float diff)
        {
            IGameKeyGeneratorConfig sut = new GameKeyGeneratorConfig(diff, diff, maxLen, minLen, tokens, wildcard);

            Assert.That(sut.MaxDifficulty, Is.EqualTo(diff));
            Assert.That(sut.MinDifficulty, Is.EqualTo(diff));
        }

        [Test]
        public void ThrowExceptionDifficultyZeroOrLess([Values(-1f, 0f)]float diff)
        {
            Assert.Throws<ArgumentException>(() => new GameKeyGeneratorConfig(diff, diff, maxLen, minLen, tokens, wildcard));
        }

        [Test]
        public void GetTokenArray()
        {
            IGameKeyGeneratorConfig sut = new GameKeyGeneratorConfig(maxDiff, minDiff, maxLen, minLen, tokens, wildcard);

            Assert.That(sut.TokenArray, Is.EqualTo(tokens));
        }

        [Test]
        public void ThrowExceptionTokenArray()
        {
            string[] empty = Array.Empty<string>(), withNull = new string[] { null }, withBlank = new string[] { "" };

            Assert.Throws<ArgumentException>(() => new GameKeyGeneratorConfig(maxDiff, minDiff, maxLen, minLen, empty, wildcard));

            Assert.Throws<ArgumentException>(() => new GameKeyGeneratorConfig(maxDiff, minDiff, maxLen, minLen, withNull, wildcard));

            Assert.Throws<ArgumentException>(() => new GameKeyGeneratorConfig(maxDiff, minDiff, maxLen, minLen, withBlank, wildcard));
        }

        [Test]
        public void GetWildCardToken()
        {
            IGameKeyGeneratorConfig sut = new GameKeyGeneratorConfig(maxDiff, minDiff, maxLen, minLen, tokens, wildcard);

            Assert.That(sut.WildCardToken, Is.EqualTo(wildcard));
        }

        [Test]
        public void ThrowExceptionWildCardNullOrEmpty([Values("", null)]string wildCard)
        {
            Assert.Throws<ArgumentException>(() => new GameKeyGeneratorConfig(maxDiff, minDiff, maxLen, minLen, tokens, wildCard));
        }

        [Test]
        [TestCaseSource("GetTestCasesAllTokens")]
        public void GetAllTokens(Tuple<string, int> item)
        {
            IGameKeyGeneratorConfig sut = new GameKeyGeneratorConfig(maxDiff, minDiff, maxLen, minLen, tokens, wildcard);

            Assert.That(sut.AllTokens[item.Item2], Is.EqualTo(item.Item1));
        }

        [Test]
        public void GetAllTokensAsString()
        {
            IGameKeyGeneratorConfig sut = new GameKeyGeneratorConfig(maxDiff, minDiff, maxLen, minLen, tokens, wildcard);

            Assert.That(sut.GetAllTokensAsString(), Is.EqualTo(allTokensString));
        }

        [Test]
        public void GetLength([Values(1, 2, int.MaxValue)]int len)
        {
            IGameKeyGeneratorConfig sut = new GameKeyGeneratorConfig(maxDiff, minDiff, len, len, tokens, wildcard);

            Assert.That(sut.MaxLength, Is.EqualTo(len));
            Assert.That(sut.MinLength, Is.EqualTo(len));
        }

        [Test]
        public void ThrowExceptionLengthZeroOrLess([Values(0, -1)]int len)
        {
            Assert.Throws<ArgumentException>(() => new GameKeyGeneratorConfig(maxDiff, minDiff, len, len, tokens, wildcard));
        }
    }
}
