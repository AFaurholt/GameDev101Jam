using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.runtime.GameDev101Jam
{
    public class ConnectLineBehaviour : MonoBehaviour
    {
        public Transform FromTransform = default;
        public Transform ToTransform = default;
        public bool IsTwoWay = false;

        private LineRenderer _lineRenderer = default;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            Vector3[] points = new Vector3[2];
            points[0] = FromTransform.position;
            points[1] = ToTransform.position;
            _lineRenderer.SetPositions(points);
        }
    }
}