using System.Linq;
using UnityEngine;

public static class ScreenManager
{
    public const string ResourcesPath = "Assets/Resources/";
    public const string ScreensPath = "Screens/";

    private static Screen[] _availableScreens;
    public static Screen[] AvailableScreens
    {
        get
        {
            if (_availableScreens == null)
            {
                _availableScreens = Resources.LoadAll<Screen>(ScreensPath);
            }
            return _availableScreens;
        }
    }

    private static Screen _activeScreen;
    private static Screen _activeModalScreen;
    
    public static Screen GetScreenByKey(ScreenName name)
    {
        return AvailableScreens.FirstOrDefault(screen => screen.ScreenId == name);
    }

    public static Screen LoadScreen(ScreenName screen)
    {
        var loadedScreen = GetScreenByKey(screen);
        if (loadedScreen == null)
        {
            Debug.LogError("Screen not found: " + screen);
            return null;
        }

        if (_activeModalScreen != null)
        {
            Object.Destroy(_activeModalScreen.gameObject);
        }

        if (!loadedScreen.IsModal && _activeScreen != null)
        {
            Object.Destroy(_activeScreen.gameObject);
            _activeScreen = null;
        }

        _activeModalScreen = null;

        var spawnedScreen = Object.Instantiate(loadedScreen.gameObject).GetComponent<Screen>();

        if (spawnedScreen.IsModal)
        {
            _activeModalScreen = spawnedScreen;
        }
        else
        {
            _activeScreen = spawnedScreen;
        }

        return spawnedScreen;
    }

    public delegate void PopupDelegate(bool result);
    private static PopupDelegate _cachedMethod;

    public static void ShowPopup(PopupDelegate method, string headerKey, string contentKey)
    {
        ShowPopup(method, headerKey, contentKey, "POPUP_OK", "POPUP_CANCEL");
    }

    public static void ShowPopup(PopupDelegate method, string headerKey, string contentKey, string fistButtonKey, string secondButtonKey)
    {
        var popup = (PopupScreen)LoadScreen(ScreenName.Popup);

        _cachedMethod = method;
        popup.HeaderKey = headerKey;
        popup.ContentKey = contentKey;
        popup.FistButtonKey = fistButtonKey;
        popup.SecondButtonKey = secondButtonKey;

        popup.OnClick += OnPopupClicked;
    }

    private static void OnPopupClicked(bool result)
    {
        ((PopupScreen)_activeModalScreen).OnClick -= OnPopupClicked;

        _cachedMethod.Invoke(result);

        if (_activeModalScreen != null)
        {
            Object.Destroy(_activeModalScreen.gameObject);
        }

        _activeModalScreen = null;
    }
}