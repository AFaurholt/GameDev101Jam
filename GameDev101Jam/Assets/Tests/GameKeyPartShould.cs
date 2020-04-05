using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using com.runtime.GameDev101Jam;

namespace Tests
{
    public class GameKeyPartShould
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ReturnString([Values("testString", "foo bar", "whatever")]string stringPart)
        {
            IGameKeyPart sut = new GameKeyPart(stringPart);

            Assert.That(stringPart, Is.EqualTo(sut.GetKeyPartString()));
        }

        [Test]
        public void ReturnTrueIfBroken()
        {
            IGameKeyPart sut = new GameKeyPart("whatever");
            sut.AddProgress(100f);

            Assert.That(true, Is.EqualTo(sut.IsBroken()));
        }

        [Test]
        public void ReturnProgress([Values(0f, 1f, 1001f, 40f, 23f)]float val)
        {
            IGameKeyPart sut = new GameKeyPart("whatever");
            sut.AddProgress(val);

            Assert.That(val, Is.EqualTo(sut.GetProgress()));
        }

       
    }
}
