using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(BetterText), true)]
    [CanEditMultipleObjects]
    public class BetterTextInspector : TextEditor
    {
        protected SerializedProperty _localizationKey;
        protected SerializedProperty _uppercase;

        protected override void OnEnable()
        {
            base.OnEnable();

            _localizationKey = serializedObject.FindProperty("_localizationKey");
            _uppercase = serializedObject.FindProperty("_uppercase");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_localizationKey);
            EditorGUILayout.PropertyField(_uppercase);
            EditorGUILayout.Space();

            if (serializedObject.ApplyModifiedProperties())
            {
                var _targetBetterText = (BetterText) target;
                _targetBetterText.OnLocalizationChanged();
            }

            base.OnInspectorGUI();
        }
    }
}