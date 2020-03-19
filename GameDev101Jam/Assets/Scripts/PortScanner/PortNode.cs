using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.runtime.GameDev101Jam
{
    class PortNode
    {
        private float _techLevel;
        private string _portName;
        private bool _isOpen;
        private bool _isVisible;
        private PlayablePassword _playablePassword;
        private HashSet<PortEdge> _outgoingPortEdges;

        public PortNode()
        {
            _outgoingPortEdges = new HashSet<PortEdge>();
        }

        public PortNode(PlayablePasswordSetting playablePasswordSetting) : this()
        {
            string passwordString = "";
            int pwLength = Random.Range(playablePasswordSetting.MinLength, playablePasswordSetting.MaxLength + 1);
            string[] allowedChars = playablePasswordSetting.AllowedCharacters.Split(playablePasswordSetting.SPLIT_CHAR);
            int maxChar = allowedChars.Length;
            for (int i = 0; i < pwLength; i++)
            {
                int randCharIndex = Random.Range(0, maxChar);
                string randChar = allowedChars[randCharIndex];
                if (randChar == playablePasswordSetting.CHARACTER_WILDCARD)
                {
                    randCharIndex = Random.Range(0, playablePasswordSetting.ALL_CHARACTERS.Length);
                    passwordString += playablePasswordSetting.ALL_CHARACTERS[randCharIndex];
                }
                else
                {
                    passwordString += randChar;
                }
            }
            _playablePassword = new PlayablePassword(passwordString, Random.Range(playablePasswordSetting.DifficultyMin, playablePasswordSetting.DifficultyMax));
        }
        public bool IsOpen
        {
            get { return _isOpen; }
            set { _isOpen = value; }
        }

        public PlayablePassword PlayablePassword
        {
            get { return _playablePassword; }
        }

        public HashSet<PortEdge> PortEdges
        {
            get { return _outgoingPortEdges; }
            set { _outgoingPortEdges = value; }
        }

        public void CrackPassword(PasswordBreaker passwordBreaker, float cpuStrength)
        {
            passwordBreaker.CrackPassword(ref _playablePassword, cpuStrength, null);
        }

        public void MakeVisible()
        {
            _isVisible = true;
        }
    }
}
