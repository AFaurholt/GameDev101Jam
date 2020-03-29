using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using com.runtime.GameDev101Jam;
using System;

namespace Tests
{
    [TestFixture]
    public class GameKeyShould
    {
        MockGameKeyPart[] mockGameKeyPartsNoneBroken, mockGameKeyPartsSomeBroken,
            mockGameKeyPartsBroken, mockGameKeyPartsZero;

        [SetUp]
        public void SetUp()
        {
            mockGameKeyPartsBroken = new MockGameKeyPart[]
            {
                new MockGameKeyPart{_theString = "", _theProgress = 100f },
                new MockGameKeyPart{_theString = "", _theProgress = 100f },
                new MockGameKeyPart{_theString = "", _theProgress = 100f },
                new MockGameKeyPart{_theString = "", _theProgress = 100f },
            };

            mockGameKeyPartsSomeBroken = new MockGameKeyPart[]
            {
                new MockGameKeyPart{_theString = "", _theProgress = 100f },
                new MockGameKeyPart{_theString = "", _theProgress = 0f },
                new MockGameKeyPart{_theString = "", _theProgress = 100f },
                new MockGameKeyPart{_theString = "", _theProgress = 99f },
            };

            mockGameKeyPartsNoneBroken = new MockGameKeyPart[]
            {
                new MockGameKeyPart{_theString = "", _theProgress = 90f },
                new MockGameKeyPart{_theString = "", _theProgress = 50f },
                new MockGameKeyPart{_theString = "", _theProgress = 1f },
                new MockGameKeyPart{_theString = "", _theProgress = 0f },
            };

            mockGameKeyPartsZero = new MockGameKeyPart[]
            {
                new MockGameKeyPart{_theString = "a", _theProgress = 0f},
                new MockGameKeyPart{_theString = "b", _theProgress = 0f},
                new MockGameKeyPart{_theString = "c", _theProgress = 0f},
                new MockGameKeyPart{_theString = "d", _theProgress = 0f},
                new MockGameKeyPart{_theString = "e", _theProgress = 0f},
                new MockGameKeyPart{_theString = "f", _theProgress = 0f},
            };
        }

        // A Test behaves as an ordinary method
        [Test]
        public void ReturnFullPasswordString([Values("testString")]string fullString)
        {
            MockGameKeyPart mockGameKeyPart = new MockGameKeyPart
            {
                _theString = fullString
            };
            MockGameKeyPart[] mockGameKeyParts = new MockGameKeyPart[] { mockGameKeyPart };
            IGameKey sut = new GameKey(mockGameKeyParts);

            Assert.That(fullString, Is.EqualTo(sut.GetPasswordString()));
        }

        [Test]
        public void BeBrokenIfAllPartsBroken()
        {
            GameKey sut = new GameKey(mockGameKeyPartsBroken);

            Assert.That(true, Is.EqualTo(sut.IsCracked()));
        }

        [Test]
        public void BeNotBrokenIfSomePartsBroken()
        {
            GameKey sut = new GameKey(mockGameKeyPartsSomeBroken);

            Assert.That(false, Is.EqualTo(sut.IsCracked()));
        }

        [Test]
        public void BeNotBrokenIfNoPartsBroken()
        {
            GameKey sut = new GameKey(mockGameKeyPartsNoneBroken);

            Assert.That(false, Is.EqualTo(sut.IsCracked()));
        }

        [Test]
        public void ReturnProgress()
        {
            MockGameKeyPart[] mockGameKeyPartsNoneBroken = new MockGameKeyPart[]
              {
                new MockGameKeyPart{_theString = "", _theProgress = 90f },
                new MockGameKeyPart{_theString = "", _theProgress = 50f },
                new MockGameKeyPart{_theString = "", _theProgress = 1f },
                new MockGameKeyPart{_theString = "", _theProgress = 0f },
              };

            float actual = (90f + 50f + 1f + 0f) / 4f;
            GameKey sut = new GameKey(mockGameKeyPartsNoneBroken);

            Assert.That(actual, Is.EqualTo(sut.GetProgress()));
        }

        [Test]
        public void CrackPassword()
        {
            IGameKey sut = new GameKey(mockGameKeyPartsZero, 1f);
            sut.Crack(10f);

            Assert.That(sut.GetProgress(), Is.Not.Zero);
            Assert.That(sut.GetProgress(), Is.Not.Null);
            Assert.That(sut.GetProgress(), Is.Not.NaN);
            Assert.That(float.IsInfinity(sut.GetProgress()), Is.False);
        }

        [Test]
        public void CrackException()
        {
            IGameKey sut = new GameKey(mockGameKeyPartsZero);
            Assert.Throws<InvalidOperationException>(() => sut.Crack(10));
        }

        class MockGameKeyPart : IGameKeyPart
        {
            public string _theString;
            public float _theProgress = 0f;
            public float AddProgress(float progress)
            {
                return _theProgress += progress;
            }

            public string GetKeyPartString()
            {
                return _theString;
            }

            public float GetProgress()
            {
                return _theProgress;
            }

            public bool IsBroken()
            {
                return _theProgress >= 100f;
            }
        }

    }
}
