using UnityEngine;

namespace com.runtime.GameDev101Jam
{
    [RequireComponent(typeof(ConnectLineBehaviour))]
    class PlayableNodeEdge : MonoBehaviour
    {
        private ConnectLineBehaviour _connectLine;
        private GameObject _from;
        private GameObject _to;
        private bool _isTwoWay;

        private void Start()
        {
            _connectLine = GetComponent<ConnectLineBehaviour>();

            //cache the nodes
            _isTwoWay = _connectLine.IsTwoWay;
            _from = _connectLine.FromTransform.gameObject;
            _to = _connectLine.ToTransform.gameObject;

            //disable the line
            _connectLine.enabled = false;
        }

        private void Update()
        {
            if (_from.activeSelf && _to.activeSelf)
            {
                _connectLine.enabled = true;
            }
        }
    }
}