using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.runtime.GameDev101Jam
{
    class PlayableNodeGraph : MonoBehaviour
    {
        [SerializeField] private List<PlayableNodeEdge> nodeEdges = default;
    }
}
