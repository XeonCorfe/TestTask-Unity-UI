using System;
using UnityEngine;

public class LocalizationAsset : ScriptableObject
{
    [SerializeField]
    private LocalizationRow[] _rows;

    public void Init()
    {
        foreach (var row in _rows)
        {
            row.SetBaseObj(this);
        }
    }

    public string GetValueByKey(string key)
    {
        foreach (var row in _rows)
        {
            if (row.LocalizationKey == key)
                return row.Value;
        }
        return key;
    }

    [Serializable]
    public class LocalizationRow
    {
        private LocalizationAsset _baseObj;
        [SerializeField]
        private string _localizationKey;

        [TextArea(1, 10)]
        [SerializeField]
        private string _value;

        public LocalizationAsset BaseObj { get { return _baseObj; } }
        public string LocalizationKey { get { return _localizationKey; } }
        public string Value { get { return _value; } }

        public void SetBaseObj(LocalizationAsset baseObj)
        {
            if (_baseObj != null)
                throw new AccessViolationException();
            _baseObj = baseObj;
        }
    }
}
