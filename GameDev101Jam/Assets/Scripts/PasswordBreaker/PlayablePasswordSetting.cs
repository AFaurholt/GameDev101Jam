using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.runtime.GameDev101Jam
{
    [CreateAssetMenu(fileName = "PlayablePasswordSetting", menuName = "PlayablePassword/PlayablePasswordSetting", order = 1)]

    class PlayablePasswordSetting : ScriptableObject
    {
        public float DifficultyMin = 1;
        public float DifficultyMax = 1;

        //comma seperated list of characters, use ** for all characters
        public string AllowedCharacters = "**";

        //how many characters appear, remember the password is cracked per char and not per "character"
        public int MinLength = 5;
        public int MaxLength = 20;
    }
}
