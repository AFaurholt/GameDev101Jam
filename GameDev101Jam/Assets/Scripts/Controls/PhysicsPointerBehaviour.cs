using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace com.runtime.GameDev101Jam
{
    class PhysicsPointerBehaviour : MonoBehaviour
    {
        public Canvas cursorCanvas = default;
        private Image cursorImage;

        private void Start()
        {
            cursorImage = cursorCanvas.GetComponentInChildren<Image>();
            Cursor.visible = false;
        }

        private void Update()
        {
            cursorImage.transform.position = Input.mousePosition;
        }
    }
}
