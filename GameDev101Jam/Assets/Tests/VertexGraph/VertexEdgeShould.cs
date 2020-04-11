using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using com.runtime.GameDev101Jam;

namespace Tests
{
    public class VertexEdgeShould
    {
        // A Test behaves as an ordinary method
        [Test]
        public void VertexEdgeCtor()
        {
            IVertex vertex1 = new MockVertex();
            IVertex vertex2 = new MockVertex();

            IVertexEdge sut = new VertexEdge(vertex1, vertex2);

            Assert.That(sut.Source, Is.EqualTo(vertex1));
            Assert.That(sut.Target, Is.EqualTo(vertex2));
        }

        class MockVertex : IVertex
        {
            public IDictionary<IVertex, IList<IVertexEdge>> Destinations => throw new System.NotImplementedException();

            public void AddDestination(IVertex target, IVertexEdge edge)
            {
                throw new System.NotImplementedException();
            }
        }
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        //[UnityTest]
        //public IEnumerator VertexEdgeShouldWithEnumeratorPasses()
        //{
        //    // Use the Assert class to test conditions.
        //    // Use yield to skip a frame.
        //    yield return null;
        //}
    }
}
