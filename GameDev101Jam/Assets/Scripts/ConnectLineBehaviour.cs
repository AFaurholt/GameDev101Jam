using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.runtime.GameDev101Jam
{
    public class ConnectLineBehaviour : MonoBehaviour
    {
        public Transform PointATransform = default;
        public Transform PointBTransform = default;

        private LineRenderer _lineRenderer = default;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            Vector3[] points = new Vector3[2];
            points[0] = PointATransform.position;
            points[1] = PointBTransform.position;
            _lineRenderer.SetPositions(points);
        }
    }
}