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
        public readonly char SPLIT_CHAR = ',';
        public readonly string CHARACTER_WILDCARD = "**";
        public readonly string[] ALL_CHARACTERS =
            {   "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o",
                "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O",
                "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};
        //how many characters appear, remember the password is cracked per char and not per "character"
        public int MinLength = 5;
        public int MaxLength = 20;
    }
}
