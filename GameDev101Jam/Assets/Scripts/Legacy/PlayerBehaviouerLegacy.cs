using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace com.runtime.GameDev101Jam
{
    public class PlayerBehaviouerLegacy : MonoBehaviour
    {
        [SerializeField] private float _baseCpuPower = 100f;
        private Dictionary<InventoryItem, float> _cpuAllocation;
        private float _maxCpuAllocation = 100f;
        [SerializeField] private float _crackingIntervalSec = 1f;
        private float _currentIntervalSec = 0f;

        [SerializeField] private float _traceMax = 100f;
        [SerializeField] private float _currentTrace = 0f;
        
        private float money;

        private Inventory _creditcardInventory;
        private Inventory _upgradeInventory;
        private Inventory _consumeableInventory;

        private PasswordBreakerLegacy _passwordBreaker = new PasswordBreakerLegacy();
        private List<PlayableNodeBehaviour> _portNodesBeingCracked = new List<PlayableNodeBehaviour>();

        // Start is called before the first frame update
        void Start()
        {
            PlayableNodeBehaviour.OnAddToCrackingEvent += AddOrRemoveNodeBeingCracked;
        }

        // Update is called once per frame
        void Update()
        {
            _currentIntervalSec += Time.deltaTime;
            if (_currentIntervalSec >= _crackingIntervalSec)
            {
                _currentIntervalSec = 0;
                foreach (var item in _portNodesBeingCracked)
                {
                    item.PortNode.CrackPassword(_passwordBreaker, _baseCpuPower / _portNodesBeingCracked.Count);

                    DebugPwCurrentProgress(item);
                }
            }
        }

        void DebugPwCurrentProgress(PlayableNodeBehaviour playableNode)
        {
            foreach (var pair in playableNode.PortNode.PlayablePassword.CurrentProgress)
            {
                
                char currentCharMarked = playableNode.PortNode.PlayablePassword.PasswordString[pair.Key];
                string formattedString = $"<b><color=yellow>{currentCharMarked}</color></b>";
                StringBuilder sb = new StringBuilder(playableNode.PortNode.PlayablePassword.PasswordString);
                sb.Replace(currentCharMarked.ToString(), formattedString, pair.Key, 1);
                formattedString = sb.ToString();
                Debug.Log($"{formattedString} : {pair.Key} : {pair.Value}");
            }
            Debug.Log($"Password is broken: {playableNode.PortNode.PlayablePassword.IsBroken}");
        }
        void AddNodeToBeingCracked(PlayableNodeBehaviour playableNode)
        {
            _portNodesBeingCracked.Add(playableNode);
        }

        void RemoveNodeFromBeingCracked(PlayableNodeBehaviour playableNode)
        {
            _portNodesBeingCracked.Remove(playableNode);
        }

        void AddOrRemoveNodeBeingCracked(PlayableNodeBehaviour playableNode)
        {
            if (_portNodesBeingCracked.Contains(playableNode))
            {
                RemoveNodeFromBeingCracked(playableNode);
            }
            else
            {
                AddNodeToBeingCracked(playableNode);
            }
            DebugNodeCrackList();
        }

        private void DebugNodeCrackList()
        {
            if (_portNodesBeingCracked.Count > 0)
            {

                foreach (var item in _portNodesBeingCracked)
                {
                    Debug.Log(item.PortNode.PlayablePassword.PasswordString);
                }
            }
            else
            {
                Debug.Log("Crack list is empty");
            }
        }
    }
}