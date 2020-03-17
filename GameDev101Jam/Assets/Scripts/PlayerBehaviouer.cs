using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.runtime.GameDev101Jam
{
    public class PlayerBehaviouer : MonoBehaviour
    {
        [SerializeField] private float _baseCpuPower = 100f;
        private Dictionary<InventoryItem, float> _cpuAllocation;

        [SerializeField] private float _traceMax = 100f;
        [SerializeField] private float _currentTrace = 0f;
        
        private float money;

        private Inventory _creditcardInventory;
        private Inventory _upgradeInventory;
        private Inventory _consumeableInventory;

        private PasswordBreakerBehaviour _passwordBreakerBehaviour;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}