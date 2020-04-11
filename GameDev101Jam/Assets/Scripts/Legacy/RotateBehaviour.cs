using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.runtime.GameDev101Jam
{
    public class RotateBehaviour : MonoBehaviour
    {
        private void Start()
        {
            float randomOffset = Random.Range(1f, 5f);
            transform.localRotation *= Quaternion.Euler(randomOffset, randomOffset, randomOffset);
        }

        private void FixedUpdate()
        {
            transform.localRotation *= Quaternion.Euler(1f, 0f, 1f);
        }
    }
}