using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using com.runtime.GameDev101Jam;

namespace Tests
{
    public class VertexGraphShould
    {
        // A Test behaves as an ordinary method
        [Test]
        public void CreateNewVertex()
        {
            IVertexGraph sut = new VertexGraph();
            IVertex vertex = new MockVertex();
            sut.AddVertex(vertex, null);

            foreach (var item in sut.Vertices)
            {
                Debug.Log(item.GetHashCode());
            }

            Assert.That(sut.Vertices, Is.EqualTo(new HashSet<IVertex> { vertex }));
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
        //public IEnumerator VertexGraphShouldWithEnumeratorPasses()
        //{
        //    // Use the Assert class to test conditions.
        //    // Use yield to skip a frame.
        //    yield return null;
        //}
    }
}
