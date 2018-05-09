using System;

namespace UnityEngine.UI
{
    /// <summary>
    ///   <para>The default Graphic to draw font data to screen.</para>
    /// </summary>
    [ExecuteInEditMode]
    [AddComponentMenu("BetterUI/Text", 10)]
    public class BetterText : Text
    {
        [TextArea(1, 10)]
        [SerializeField]
        private string _localizationKey;
        [SerializeField]
        private bool _uppercase;

        public string LocalizationKey
        {
            get
            {
                return _localizationKey;
            }
            set
            {
                _localizationKey = value; 
                OnLocalizationChanged();
            }
        }

        public override String text
        {
            get
            {
                return base.text;
            }
            set
            {
                if (_uppercase)
                {
                    base.text = value.ToUpper();
                }
                else
                {
                    base.text = value;
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            OnLocalizationChanged();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Localization.LocalizationChanged += OnLocalizationChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Localization.LocalizationChanged -= OnLocalizationChanged;
        }

        public void OnLocalizationChanged()
        {
            var newText = Localization.CurrentLocalization.GetValueByKey(_localizationKey);
            if (newText != "")
            {
                text = newText;
            }
        }
    }
}