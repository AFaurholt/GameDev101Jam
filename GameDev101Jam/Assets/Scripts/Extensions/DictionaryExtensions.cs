using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.runtime.GameDev101Jam
{
    public static class DictionaryExtensions
    {
        public static IDictionary<TKey, TValue> Concat<TKey, TValue>(this IDictionary<TKey, TValue> current, params IDictionary<TKey, TValue>[] dictionaries)
        {
            foreach (var dict in dictionaries)
            {
                foreach (var item in dict)
                {
                    current.Add(item.Key, item.Value);
                }
            }

            return current;
        }
    }
}
