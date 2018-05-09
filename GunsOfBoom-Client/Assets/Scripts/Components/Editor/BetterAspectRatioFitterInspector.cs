namespace UnityEditor.UI
{
    [CustomEditor(typeof(BetterAspectRatioFitter), true)]
    [CanEditMultipleObjects]
    public class BetterAspectRatioFitterInspector : SelfControllerEditor
    {
        protected SerializedProperty _aspectMode;
        protected SerializedProperty _aspectRatio;
        protected SerializedProperty _ratioByImage;

        protected virtual void OnEnable()
        {
            _aspectMode = serializedObject.FindProperty("m_AspectMode");
            _aspectRatio = serializedObject.FindProperty("m_AspectRatio");
            _ratioByImage = serializedObject.FindProperty("_ratioByImage");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_aspectMode);
            EditorGUILayout.PropertyField(_ratioByImage);

            if (!_ratioByImage.boolValue)
            {
                EditorGUILayout.PropertyField(_aspectRatio);
            }

            if (serializedObject.ApplyModifiedProperties())
            {
                var t = (BetterAspectRatioFitter) target;
                t.CalculateRatioByImage();
            }

            base.OnInspectorGUI();
        }
    }
}