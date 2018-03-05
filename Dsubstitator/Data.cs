// 
// This file is a part of the Dsubstitator project.
// https://github.com/Orace
// 

using System;
using System.Collections.Generic;
using System.Linq;

namespace Dsubstitator
{
    public class Data
    {
        private readonly Dictionary<long, List<string>> _dictionary = new Dictionary<long, List<string>>();

        public IReadOnlyList<string> GetMatch(string word)
        {
            word = word.ToLower();
            var key = GetKey(word);
            var matchs = _dictionary[key];
            var code = GetCode(word).ToList();
            var result = matchs.Where(m => GetCode(m).SequenceEqual(code)).ToList();

            if (matchs.Count != result.Count)
            {
                Console.WriteLine($"{matchs.Count - result.Count} colisions detected");
            }

            return result;
        }

        public void Register(string word)
        {
            word = word.ToLower();
            var key = GetKey(word);
            if (!_dictionary.TryGetValue(key, out var list))
            {
                list = new List<string>();
                _dictionary.Add(key, list);
            }
            list.Add(word);
        }

        private static long GetKey(string word)
        {
            unchecked
            {
                return GetCode(word).Aggregate(0, (current, i) => current * 397 ^ i);
            }
        }

        private static IEnumerable<int> GetCode(string word)
        {
            var knownChar = new Dictionary<char, int>();
            foreach (var c in word)
            {
                if (!knownChar.TryGetValue(c, out var index))
                {
                    index = knownChar.Count;
                    knownChar.Add(c, index);
                }
                yield return index;
            }
        }
    }
}