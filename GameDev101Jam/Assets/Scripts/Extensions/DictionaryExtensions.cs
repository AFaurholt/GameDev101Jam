using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public static class DictionaryExtensions
    {
        public static Dictionary<TKey, TValue> Combine<TKey, TValue>(this Dictionary<TKey, TValue> current, params Dictionary<TKey, TValue>[] dictionaries)
        {
            Dictionary<TKey, TValue> completeDic = current;

            foreach (var dic in dictionaries)
            {
                foreach (var item in dic)
                {
                    completeDic.Add(item.Key, item.Value);
                }
            }

            return completeDic;
        }
    }
}
