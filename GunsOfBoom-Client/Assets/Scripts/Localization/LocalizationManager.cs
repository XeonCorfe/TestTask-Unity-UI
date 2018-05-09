using UnityEngine;

public class LocalizationManager : ScriptableObject
{
    [SerializeField]
    private bool _overrideLocalization;
    public bool OverrideLocalization { get { return _overrideLocalization; } }

    [SerializeField]
    private int _overrideLocalizationIndex;
    public int OverrideLocalizationIndex { get { return _overrideLocalizationIndex; } }
}
