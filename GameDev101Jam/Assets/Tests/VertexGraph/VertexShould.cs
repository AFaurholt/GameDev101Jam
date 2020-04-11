using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using com.runtime.GameDev101Jam;

namespace Tests
{
    public class VertexShould
    {
        // A Test behaves as an ordinary method
        [Test]
        public void GetDestinationsOnEmpty()
        {
            IVertex sut = new Vertex();

            Assert.That(sut.Destinations, Is.Empty);
        }

        [Test]
        public void GetDestinationsWithVertices()
        {
            IVertex sut1 = new Vertex();
            IVertex sut2 = new Vertex();
            IVertex sut3 = new Vertex();

            IVertexEdge edge1 = new MockVertexEdge();
            IVertexEdge edge2 = new MockVertexEdge();

            sut1.AddDestination(sut2, edge1);
            sut1.AddDestination(sut3, edge2);

            IDictionary<IVertex, IList<IVertexEdge>> expected = new Dictionary<IVertex, IList<IVertexEdge>> {
                {sut2, new List<IVertexEdge>{ edge1 } },
                {sut3, new List<IVertexEdge>{ edge2 } }};

            Assert.That(sut1.Destinations, Is.EqualTo(expected));
        }

        class MockVertexEdge : IVertexEdge
        {
            public IVertex Source => throw new System.NotImplementedException();

            public IVertex Target => throw new System.NotImplementedException();
        }
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        //[UnityTest]
        //public IEnumerator VertexShouldWithEnumeratorPasses()
        //{
        //    // Use the Assert class to test conditions.
        //    // Use yield to skip a frame.
        //    yield return null;
        //}
    }
}
