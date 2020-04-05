using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace com.runtime.GameDev101Jam
{
    public class GameKey : IGameKey
    {
        readonly IGameKeyPart[] _gameKeyPartArray;
        readonly float _difficulty;

        public float Progress
        {
            get
            {
                float progress = 0f;
                foreach (var item in _gameKeyPartArray)
                {
                    progress += item.Progress;
                }

                progress /= _gameKeyPartArray.Length;
                return progress;
            }
        }

        string IGameKey.PasswordString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in _gameKeyPartArray)
                {
                    sb.Append(item.KeyPartString);
                }

                return sb.ToString();
            }
        }

        bool IGameKey.IsCracked => Progress >= 100f;

        public GameKey(IGameKeyPart[] passwordParts)
        {
            _gameKeyPartArray = passwordParts;
        }

        public GameKey(IGameKeyPart[] passwordParts, float difficulty) : this(passwordParts)
        {
            _difficulty = difficulty;
        }

        public void Crack(float power)
        {
            if (_difficulty > 0)
            {


                for (int i = 0; i < _gameKeyPartArray.Length; i++)
                {
                    float progress = Random.Range(power / _difficulty, power);
                    progress /= _difficulty;
                    _gameKeyPartArray[i].AddProgress(progress);
                }
            }
            else
            {
                throw new InvalidOperationException("Difficulty is less than 0 or uninitialised");
            }

        }
    }
}
