using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupScreen : Screen
{
    [SerializeField]
    private BetterText _headerLabel;
    [SerializeField]
    private BetterText _contentLabel;
    [SerializeField]
    private BetterText _fistButtonLabel;
    [SerializeField]
    private BetterText _secondButtonLabel;

    [SerializeField]
    private BetterButton _fistButton;
    [SerializeField]
    private BetterButton _secondButton;

    public string HeaderKey
    {
        get { return _headerLabel.LocalizationKey; }
        set { _headerLabel.LocalizationKey = value; }
    }

    public string ContentKey
    {
        get { return _contentLabel.LocalizationKey; }
        set { _contentLabel.LocalizationKey = value; }
    }

    public string FistButtonKey
    {
        get { return _fistButtonLabel.LocalizationKey; }
        set { _fistButtonLabel.LocalizationKey = value; }
    }

    public string SecondButtonKey
    {
        get { return _secondButtonLabel.LocalizationKey; }
        set { _secondButtonLabel.LocalizationKey = value; }
    }

    public UnityAction<bool> OnClick;

    public override ScreenName ScreenId { get { return ScreenName.Popup; } }

    private void OnEnable()
    {
        _fistButton.onClick.AddListener(OnFistButtonClick);
        _secondButton.onClick.AddListener(OnSecondButtonClick);
    }

    private void OnDisable()
    {
        _fistButton.onClick.RemoveListener(OnFistButtonClick);
        _secondButton.onClick.RemoveListener(OnSecondButtonClick);
    }

    private void OnFistButtonClick()
    {
        if (OnClick != null)
        {
            OnClick.Invoke(true);
        }
    }

    private void OnSecondButtonClick()
    {
        if (OnClick != null)
        {
            OnClick.Invoke(false);
        }
    }
}