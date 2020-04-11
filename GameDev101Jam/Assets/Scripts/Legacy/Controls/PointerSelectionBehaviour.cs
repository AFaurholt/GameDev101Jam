using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace com.runtime.GameDev101Jam
{
    class PointerSelectionBehaviour : MonoBehaviour
    {
        public Image ImageHover = default;
        public Image ImageSelected = default;
        private Canvas canvas = default;
        private void Start()
        {
            canvas = GetComponent<Canvas>();

            SelectableBehaviour.OnMouseDownEvent += SetSelected;
            SelectableBehaviour.OnMouseOverEvent += ShowHover;
            SelectableBehaviour.OnMouseExitEvent += HideHover;

            ImageHover.enabled = false;
            ImageSelected.enabled = false;
        }

        private void ShowHover(Transform tf, Bounds bounds)
        {
            ShowImageAndSetPosition(ImageHover, tf);
        }

        private void HideHover()
        {
            ImageHover.enabled = false;
        }

        private void SetSelected(Transform tf, Bounds bounds)
        {
            ShowImageAndSetPosition(ImageSelected, tf);
            //SetImageBounds(ImageSelected, bounds);
        }

        private void ShowImageAndSetPosition(Image image, Transform tf)
        {
            image.enabled = true;
            image.transform.position = Camera.main.WorldToScreenPoint(tf.position);
        }

        //TODO Needs to translate to screenspace
        private void SetImageBounds(Image image, Bounds bounds)
        {
            Vector2 sizeDelta = bounds.size;
            image.rectTransform.sizeDelta = sizeDelta;
        }
    }
}
