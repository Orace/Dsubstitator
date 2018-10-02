// 
// This file is a part of the Dsubstitator project.
// https://github.com/Orace
// 

using System;
using System.Collections.Generic;
using System.Linq;

namespace Dsubstitator
{
    public class Code : IEquatable<Code>
    {
        readonly IEnumerable<int> _code;

        public Code(string s) : this(GetCode(s))
        {
        }

        public Code(IEnumerable<int> code)
        {
            _code = code;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Code) obj);
        }

        public bool Equals(Code other)
        {
            return other != null && _code.SequenceEqual(other._code);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return _code.Aggregate(0, (current, i) => current * 397 ^ i);
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

    public class Data
    {
        private readonly Dictionary<Code, HashSet<string>> _dictionary = new Dictionary<Code, HashSet<string>>();

        public IReadOnlyCollection<string> GetMatch(string word)
        {
            word = word.ToLower();
            var code = new Code(word);
            return _dictionary[code];
        }

        public bool Register(string word)
        {
            word = word.ToLower();
            var code = new Code(word);
            if (!_dictionary.TryGetValue(code, out var set))
            {
                set = new HashSet<string>();
                _dictionary.Add(code, set);
            }
            return set.Add(word);
        }
    }
}