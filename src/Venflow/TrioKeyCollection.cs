﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Venflow
{

    internal class TrioKeyCollection<TKeyTwo, TKeyThree, TValue>
        where TKeyTwo : notnull
        where TKeyThree : notnull
        where TValue : class
    {
        private readonly TValue[] _oneToValue;
        private readonly IDictionary<TKeyTwo, TValue> _twoToOne;
        private readonly IDictionary<TKeyThree, TValue> _threeToOne;

        internal int Count => _oneToValue.Length;

        internal TrioKeyCollection(TValue[] firstCollction, Dictionary<TKeyTwo, TValue> twoToOne, Dictionary<TKeyThree, TValue> threeToOne)
        {
            _oneToValue = firstCollction;
            _twoToOne = twoToOne;
            _threeToOne = threeToOne;
        }

        internal TValue this[int key] => _oneToValue[key];

        internal bool TryGetValue(TKeyTwo key,
#if !NET48
            [NotNullWhen(true)]
#endif
            out TValue? value)
        {
            return _twoToOne.TryGetValue(key, out value);
        }

        internal bool TryGetValue(TKeyThree key,
#if !NET48
            [NotNullWhen(true)]
#endif
            out TValue? value)
        {
            return _threeToOne.TryGetValue(key, out value);
        }
    }
}
