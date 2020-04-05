using System.Collections;
using System.Collections.Generic;
using com.runtime.GameDev101Jam;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PasswordBreakerShould
    {
        [Test]
        public void AddToBreakList()
        {
            IPasswordBreaker sut1 = new PasswordBreaker();
            IGameKey gameKey = new MockGameKey();

            sut1.AddToBreakList(gameKey);

            Assert.That(sut1.BreakList, Is.EqualTo(new List<IGameKey>() { gameKey }));
        }

        [Test]
        public void NoDuplicateAdd()
        {
            IPasswordBreaker sut1 = new PasswordBreaker();
            IGameKey gameKey = new MockGameKey();

            sut1.AddToBreakList(gameKey);
            sut1.AddToBreakList(gameKey);

            Assert.That(sut1.BreakList, Is.EqualTo(new List<IGameKey>() { gameKey }));
        }

        [Test]
        public void RemoveFromBreakList()
        {
            IPasswordBreaker sut1 = new PasswordBreaker();
            IGameKey gameKey1 = new MockGameKey();
            IGameKey gameKey2 = new MockGameKey();

            sut1.AddToBreakList(gameKey1);
            sut1.RemoveFromBreakList(gameKey1);

            Assert.That(sut1.BreakList, Is.EqualTo(new List<IGameKey>()));

            sut1.AddToBreakList(gameKey1);
            sut1.AddToBreakList(gameKey2);

            sut1.RemoveFromBreakList(gameKey1);

            Assert.That(sut1.BreakList, Is.EqualTo(new List<IGameKey>() { gameKey2 }));

        }

        [Test]
        public void Crack([Values(10f, 10000f, float.MaxValue)]float crackPower)
        {
            IPasswordBreaker sut1 = new PasswordBreaker();
            MockGameKey gameKey1 = new MockGameKey();
            MockGameKey gameKey2 = new MockGameKey();

            sut1.AddToBreakList(gameKey1, gameKey2);
            sut1.Crack(crackPower);

            Assert.That(gameKey1.crackWasRun, Is.True);
            Assert.That(gameKey2.crackWasRun, Is.True);
            Assert.That(gameKey1.runWithPower, Is.EqualTo(crackPower));
            Assert.That(gameKey2.runWithPower, Is.EqualTo(crackPower));
        }

        class MockGameKey : IGameKey
        {
            public bool crackWasRun = false;
            public float runWithPower = 0f;

            public float Progress => throw new System.NotImplementedException();

            public string PasswordString => throw new System.NotImplementedException();

            bool IGameKey.IsCracked => throw new System.NotImplementedException();

            public void Crack(float power)
            {
                crackWasRun = true;
                runWithPower = power;
            }
        }


        //// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        //// `yield return null;` to skip a frame.
        //[UnityTest]
        //public IEnumerator PasswordBreakerShouldWithEnumeratorPasses()
        //{
        //    // Use the Assert class to test conditions.
        //    // Use yield to skip a frame.
        //    yield return null;
        //}
    }
}
