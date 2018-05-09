using UnityEngine;
using UnityEngine.UI;

/// <summary>
///   <para>Resizes a RectTransform to fit a specified aspect ratio.</para>
/// </summary>
[AddComponentMenu("BetterUI/AspectRatioFitter", 10)]
public class BetterAspectRatioFitter : AspectRatioFitter
{
    [SerializeField]
    private bool _ratioByImage;
    private Texture imageComponent;

    public bool RatioByImage
    {
        get
        {
            return _ratioByImage;
        }
        set
        {
            _ratioByImage = value;
            if (_ratioByImage)
            {
                CalculateRatioByImage();
            }
        }
    }

    public void CalculateRatioByImage()
    {
        imageComponent = GetComponent<Image>().mainTexture;
        aspectRatio = (float)imageComponent.width / imageComponent.height;
    }
}