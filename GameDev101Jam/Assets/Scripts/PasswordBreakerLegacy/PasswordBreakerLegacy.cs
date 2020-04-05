using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.runtime.GameDev101Jam
{
    class PasswordBreakerLegacy
    {
        public void CrackPassword(ref PlayablePassword playablePassword, float cpuStrength, Inventory upgradeInventory)
        {
            Dictionary<int, float> newProgress = new Dictionary<int, float>();
            foreach (KeyValuePair<int, float> item in playablePassword.CurrentProgress)
            {
                if (!(item.Value >= 100f))
                {
                    float increment = Random.Range(cpuStrength / playablePassword.Difficulty, cpuStrength);
                    newProgress.Add(item.Key, playablePassword.CurrentProgress[item.Key] + increment / playablePassword.Difficulty);
                }
            }
            playablePassword.CurrentProgress = newProgress;
        }
    }
}
