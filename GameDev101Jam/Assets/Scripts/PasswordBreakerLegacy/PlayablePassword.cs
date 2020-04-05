using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    class PlayablePassword
    {
        private string _passwordString;
        private Dictionary<int, float> _currentProgress;
        private float _difficulty;

        public PlayablePassword(string passwordString, float difficulty)
        {
            Difficulty = difficulty;

            _passwordString = passwordString;
            _currentProgress = new Dictionary<int, float>();

            for (int i = 0; i < passwordString.Length; i++)
            {
                _currentProgress.Add(i, 0f);
            }
        }

        public Dictionary<int, float> CurrentProgress
        {
            get { return _currentProgress; }
            set { _currentProgress = value; }
        }

        public float Difficulty
        {
            get { return _difficulty; }
            private set { _difficulty = value; }
        }

        public string PasswordString
        {
            get { return _passwordString; }
        }

        public bool IsBroken
        {
            get
            {
                //if no value is under 100, pass is broken
                return !(_currentProgress.Values.Any(x => x < 100f));
            }
        }

    }
}
