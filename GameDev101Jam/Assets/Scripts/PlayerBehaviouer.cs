using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.runtime.GameDev101Jam
{
    public class PlayerBehaviouer : MonoBehaviour
    {
        [SerializeField] private float baseCpuPower = 100f;
        private Dictionary<InventoryItem, float> cpuAllocation;

        [SerializeField] private float traceMax = 100f;
        [SerializeField] private float currentTrace = 0f;
        //private float money;

        private Inventory creditcardInventory;
        private Inventory upgradeInventory;
        private Inventory consumeableInventory;

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