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

        private void Start()
        {
            gameObject.SetActive(IsVisible);
            PortNode = new PortNode(passwordSetting);
            Debug.Log(PortNode.PlayablePassword.PasswordString);
        }
    }
}
