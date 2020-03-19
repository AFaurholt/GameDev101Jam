using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.runtime.GameDev101Jam
{
    class PlayableNodeBehaviour : MonoBehaviour
    {
        public float TechLevel = 1;
        public string PortName = "Unnamed";
        public bool IsOpen = true;
        public bool IsVisible = false;
        public PortNode PortNode;

        public PlayablePasswordSetting passwordSetting = default;

        //probably needs to be moved out
        public delegate void OnAddToCracking(PlayableNodeBehaviour playableNode);
        public static event OnAddToCracking OnAddToCrackingEvent;

        private void Start()
        {
            gameObject.SetActive(IsVisible);
            PortNode = new PortNode(passwordSetting);
            Debug.Log(PortNode.PlayablePassword.PasswordString);
            Debug.Log(PortNode.PlayablePassword.Difficulty);
        }

        private void OnMouseDown()
        {
            //TODO needs to be activated by a UI menu item
            if (IsVisible)
            {
                OnAddToCrackingEvent?.Invoke(this);
            }
        }
    }
}
