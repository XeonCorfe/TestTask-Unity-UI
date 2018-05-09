using System.Collections;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    /// <summary>
    ///   <para>The default Graphic to draw font data to screen.</para>
    /// </summary>
    [AddComponentMenu("BetterUI/Button", 10)]
    public class BetterButton : Button
    {
        [SerializeField]
        protected Vector3 _normalScale;
        [SerializeField]
        protected Vector3 _highlightedScale;
        [SerializeField]
        protected Vector3 _pressedScale;
        [SerializeField]
        protected Vector3 _disabledScale;
        [SerializeField]
        protected float _scalingDuration;

        private Vector3 _buttonScale;
        private Vector3 _targetScale;

        protected override void Awake()
        {
            base.Awake();

            _buttonScale = transform.localScale;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            OnPointerAction(_pressedScale);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            OnPointerAction(_highlightedScale);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            OnPointerAction(_highlightedScale);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            OnPointerAction(_normalScale);
        }

        private void OnPointerAction(Vector3 multiplier)
        {
            _targetScale.x = _buttonScale.x * multiplier.x;
            _targetScale.y = _buttonScale.y * multiplier.y;
            _targetScale.z = _buttonScale.z * multiplier.z;

            StartCoroutine(Resize(_targetScale));
        }

        private IEnumerator Resize(Vector3 currentScale)
        {
            while (Mathf.Abs(transform.localScale.sqrMagnitude - _targetScale.sqrMagnitude) > 0.01f)
            {
                if (currentScale != _targetScale)
                {
                    break;
                }

                transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, _scalingDuration * Time.deltaTime);
                yield return null;
            }
        }

        #if UNITY_EDITOR
        protected override void Reset()
        {
            base.Reset();

            _normalScale = transform.localScale;
            _highlightedScale = transform.localScale;
            _pressedScale = transform.localScale;
            _disabledScale = transform.localScale;

            _scalingDuration = 10f;
        }
        #endif
    }
}