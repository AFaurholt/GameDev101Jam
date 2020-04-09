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

        public GameKey(IGameKeyPart[] passwordParts, float difficulty)
        {
            CheckDifficultyError(difficulty);
            _difficulty = difficulty;
            _gameKeyPartArray = passwordParts;
        }

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

        public string PasswordString
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

        public bool IsCracked => Progress >= 100f;

        public void Crack(float power)
        {
            for (int i = 0; i < _gameKeyPartArray.Length; i++)
            {
                _gameKeyPartArray[i].AddProgress(GenerateProgress(power));
            }
        }

        private float GenerateProgress(float power)
        {
            return Random.Range(power / _difficulty, power) / _difficulty;
        }
        private void CheckDifficultyError(float difficulty)
        {
            if (difficulty <= 0)
            {
                throw new InvalidOperationException("Difficulty is less than 0 or uninitialised");
            }
        }
    }
}
