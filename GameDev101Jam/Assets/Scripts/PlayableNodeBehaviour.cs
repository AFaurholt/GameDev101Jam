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
        //Only for setting in the editor
        [SerializeField] private float _techLevel = 1;
        [SerializeField] private string _portName = "Unnamed";
        [SerializeField] private bool _isOpen = true;
        [SerializeField] private bool _isVisible = false;

        public PortNode PortNode;

        [SerializeField] private PlayablePasswordSetting _passwordSetting = default;

        //probably needs to be moved out
        public delegate void OnAddToCracking(PlayableNodeBehaviour playableNode);
        public static event OnAddToCracking OnAddToCrackingEvent;

        private void Start()
        {
            gameObject.SetActive(_isVisible);
            PortNode = new PortNode(_passwordSetting, _portName, _techLevel, _isOpen, _isVisible);

            Debug.Log(PortNode.PlayablePassword.PasswordString);
            Debug.Log(PortNode.PlayablePassword.Difficulty);
        }

        private void OnMouseDown()
        {
            //TODO needs to be activated by a UI menu item
            if (PortNode.IsVisible)
            {
                OnAddToCrackingEvent?.Invoke(this);
            }
        }
    }
}
