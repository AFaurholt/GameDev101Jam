using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using com.runtime.GameDev101Jam;

namespace Tests
{
    [TestFixture]
    public class GameKeyShould
    {
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
            MockGameKeyPart[] mockGameKeyPartsBroken = new MockGameKeyPart[]
            {
                new MockGameKeyPart{_theString = "", _theProgress = 100f },
                new MockGameKeyPart{_theString = "", _theProgress = 100f },
                new MockGameKeyPart{_theString = "", _theProgress = 100f },
                new MockGameKeyPart{_theString = "", _theProgress = 100f },
            };

            GameKey sut = new GameKey(mockGameKeyPartsBroken);

            Assert.That(true, Is.EqualTo(sut.IsCracked()));
        }

        [Test]
        public void BeNotBrokenIfSomePartsBroken()
        {
            MockGameKeyPart[] mockGameKeyPartsSomeBroken = new MockGameKeyPart[]
            {
                new MockGameKeyPart{_theString = "", _theProgress = 100f },
                new MockGameKeyPart{_theString = "", _theProgress = 0f },
                new MockGameKeyPart{_theString = "", _theProgress = 100f },
                new MockGameKeyPart{_theString = "", _theProgress = 99f },
            };
            GameKey sut = new GameKey(mockGameKeyPartsSomeBroken);

            Assert.That(false, Is.EqualTo(sut.IsCracked()));
        }
        [Test]
        public void BeNotBrokenIfNoPartsBroken()
        {
            MockGameKeyPart[] mockGameKeyPartsNoneBroken = new MockGameKeyPart[]
              {
                new MockGameKeyPart{_theString = "", _theProgress = 90f },
                new MockGameKeyPart{_theString = "", _theProgress = 50f },
                new MockGameKeyPart{_theString = "", _theProgress = 1f },
                new MockGameKeyPart{_theString = "", _theProgress = 0f },
              };

            GameKey sut = new GameKey(mockGameKeyPartsNoneBroken);

            Assert.That(false, Is.EqualTo(sut.IsCracked()));
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
