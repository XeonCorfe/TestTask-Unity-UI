using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public static class Localization
{
    public const string ResourcesPath = "Assets/Resources/";
    public const string LocalizationManagerPath = "Managers/LocalizationManager";
    public const string LocalizationPath = "Localization/";

    private static LocalizationAsset[] _availableLocalizations;
    public static LocalizationAsset[] AvailableLocalizations
    {
        get
        {
            if (_availableLocalizations == null)
            {
                _availableLocalizations = Resources.LoadAll<LocalizationAsset>(LocalizationPath);
            }
            return _availableLocalizations;
        }
    }

    private static LocalizationAsset _currentLocalization;
    public static LocalizationAsset CurrentLocalization { get { return _currentLocalization; } }

    private static string[] _availableLocalizationNames;
    public static string[] AvailableLocalizationNames
    {
        get
        {
            if (_availableLocalizationNames == null)
            {
                _availableLocalizationNames = (from x in AvailableLocalizations select x.name).ToArray();
            }
            return _availableLocalizationNames;
        }
    }

    public static UnityAction LocalizationChanged;

    private static LocalizationManager _locManager;
    private static LocalizationManager LocManager
    {
        get
        {
            if (_locManager == null)
            {
                _locManager = Resources.Load<LocalizationManager>(LocalizationManagerPath);
            }
            return _locManager;
        }
    }

    static Localization()
    {
        LocalizationUpdate();
    }

    public static void LocalizationUpdate()
    {
        if (LocManager.OverrideLocalization)
        {
            SetLocalization(AvailableLocalizationNames[LocManager.OverrideLocalizationIndex]);
        }
        else
        {
            SetLocalization(CultureInfo.CurrentUICulture.ToString());
        }
    }

    public static void SetLocalization(string name)
    {
        foreach (var localization in AvailableLocalizations)
        {
            if (localization.name != name)
                continue;

            _currentLocalization = localization;
            if (LocalizationChanged != null)
                LocalizationChanged.Invoke();
            break;
        }
    }
}