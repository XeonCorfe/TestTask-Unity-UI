using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : Screen
{
    [SerializeField]
    private Image _loadingBarImage;
    [SerializeField]
    private BetterText _loadingProgressLabel;
    [SerializeField]
    private float _loadingDuration = 10f;

    public override ScreenName ScreenId { get { return ScreenName.Loading; } }

    private IEnumerator Start()
    {
        var loadState = 0f;

        while (true)
        {
            loadState += Time.deltaTime / _loadingDuration;
            _loadingBarImage.fillAmount = loadState;
            _loadingProgressLabel.text = (int)(loadState * 100) + "%";

            if (loadState >= 1)
            {
                ScreenManager.ShowPopup(OnWelcomePopupClosed, "WELCOME_HEAD", "WELCOME_CONTENT");
                break;
            }

            yield return null;
        }
    }

    private void OnWelcomePopupClosed(bool result)
    {
        if (result)
        {
            ScreenManager.LoadScreen(ScreenName.Lobby);
        }
        else
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif

            Application.Quit();
        }
    }
}