using UnityEngine;

public abstract class Screen : MonoBehaviour
{
    public abstract ScreenName ScreenId { get; }

    [SerializeField]
    private bool _isModal;
    public bool IsModal { get { return _isModal; } }
}