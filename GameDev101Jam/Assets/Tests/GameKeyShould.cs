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
            IGameKey sut = new GameKey();

            Assert.That(fullString, Is.EqualTo(sut.GetPasswordString()));
        }

        
    }
}
