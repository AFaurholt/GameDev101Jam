using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public class GameKey : IGameKey
    {
        IGameKeyPart[] _gameKeyPartArray;

        public GameKey(string[] passwordString)
        {

        }

        //private IGameKeyPart[] GameKeyPartsGenerate(string[] input)
        //{
        //    IGameKeyPart[] gameKeyParts = new IGameKeyPart[input.Length];

        //    for (int i = 0; i < input.Length; i++)
        //    {

        //        gameKeyParts[i] = new GameKeyPart(input[i]);
        //    }
        //}

        public void Crack(float power)
        {
            throw new NotImplementedException();
        }

        public string GetPasswordString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in _gameKeyPartArray)
            {
                sb.Append(item.GetKeyPartString());
            }

            return sb.ToString();
        }

        public void IsCracked()
        {
            throw new NotImplementedException();
        }
    }
}
