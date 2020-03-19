using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.runtime.GameDev101Jam
{
    class SelectableBehaviour : MonoBehaviour
    {
        public delegate void OnMouseDownDelegate(Transform tf, Bounds bounds);
        public delegate void OnMouseOverDelegate(Transform tf, Bounds bounds);
        public delegate void OnMouseExitDelegate();
        public static event OnMouseDownDelegate OnMouseDownEvent; 
        public static event OnMouseOverDelegate OnMouseOverEvent;
        public static event OnMouseExitDelegate OnMouseExitEvent;

        private bool _isSelectable = true;
        private Renderer r = default;
        private void Start()
        {
            r = GetComponent<Renderer>();
        }
        private void OnMouseDown()
        {
            if (_isSelectable && OnMouseDownEvent != null)
            {
                OnMouseDownEvent.Invoke(transform, r.bounds);
            }
        }

        private void OnMouseOver()
        {
            if (_isSelectable && OnMouseOverEvent != null)
            {
                OnMouseOverEvent.Invoke(transform, r.bounds);
            }
        }

        private void OnMouseExit()
        {
            OnMouseExitEvent?.Invoke();
        }
    }


}
