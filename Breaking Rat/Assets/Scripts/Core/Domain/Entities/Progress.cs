using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BreakingRat.Domain.Entities
{
    [Serializable]
    public class Progress : ISerializationCallbackReceiver
    {
        public Dictionary<int, int> Records = new();

        [SerializeField] private int[] _keys;
        [SerializeField] private int[] _values;

        public void OnBeforeSerialize()
        {
            _keys = Records.Keys.ToArray();
            _values = Records.Values.ToArray();
        }

        public void OnAfterDeserialize()
        {
            for (int i = 0; i < _keys.Length; i++)
                Records.Add(_keys[i], _values[i]);
        }
    }
}
