using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(BetterButton), true)]
    [CanEditMultipleObjects]
    public class BetterButtonInspector : SelectableEditor
    {
        protected SerializedProperty _normalScale;
        protected SerializedProperty _highlightedScale;
        protected SerializedProperty _pressedScale;
        protected SerializedProperty _disabledScale;
        protected SerializedProperty _scalingDuration;

        protected SerializedProperty m_OnClickProperty;

        protected override void OnEnable()
        {
            base.OnEnable();

            _normalScale = serializedObject.FindProperty("_normalScale");
            _highlightedScale = serializedObject.FindProperty("_highlightedScale");
            _pressedScale = serializedObject.FindProperty("_pressedScale");
            _disabledScale = serializedObject.FindProperty("_disabledScale");
            _scalingDuration = serializedObject.FindProperty("_scalingDuration");

            m_OnClickProperty = serializedObject.FindProperty("m_OnClick");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            EditorGUILayout.LabelField("Scale Transition", EditorStyles.label);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_normalScale);
            EditorGUILayout.PropertyField(_highlightedScale);
            EditorGUILayout.PropertyField(_pressedScale);
            EditorGUILayout.PropertyField(_disabledScale);
            EditorGUILayout.PropertyField(_scalingDuration);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(m_OnClickProperty);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}