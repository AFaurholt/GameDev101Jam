using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.runtime.GameDev101Jam
{
    class PasswordBreaker
    {
        public void CrackPassword(ref PlayablePassword playablePassword, float cpuStrength, Inventory upgradeInventory)
        {
            foreach (KeyValuePair<int, float> item in playablePassword.CurrentProgress)
            {
                if (!(item.Value >= 100f))
                {
                    float increment = Random.Range(cpuStrength / playablePassword.Difficulty, cpuStrength);
                    playablePassword.CurrentProgress[item.Key] += increment / playablePassword.Difficulty;
                }
            }
        }
    }
}
