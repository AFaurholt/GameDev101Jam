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

       
    }
}
