using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.runtime.GameDev101Jam
{
    public class PlayerBehaviouer : MonoBehaviour
    {
        [SerializeField] private float _baseCpuPower = 100f;
        private Dictionary<InventoryItem, float> _cpuAllocation;
        private float _maxCpuAllocation = 100f;

        [SerializeField] private float _traceMax = 100f;
        [SerializeField] private float _currentTrace = 0f;
        
        private float money;

        private Inventory _creditcardInventory;
        private Inventory _upgradeInventory;
        private Inventory _consumeableInventory;

        private PasswordBreaker _passwordBreakerBehaviour;
        private List<PlayableNodeBehaviour> _portNodesBeingCracked = new List<PlayableNodeBehaviour>();

        // Start is called before the first frame update
        void Start()
        {
            PlayableNodeBehaviour.OnAddToCrackingEvent += AddOrRemoveNodeBeingCracked;
        }

        // Update is called once per frame
        void Update()
        {

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