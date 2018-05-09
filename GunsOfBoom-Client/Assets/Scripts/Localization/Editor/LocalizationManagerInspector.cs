using System;
using System.Globalization;
using UnityEngine;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(LocalizationManager), true)]
    public class LocalizationManagerInspector : Editor
    {
        private int _selectedIndexCulture;

        protected SerializedProperty _overrideLocalization;
        protected SerializedProperty _overrideLocalizationIndex;

        private string[] _localizationNames;

        protected void OnEnable()
        {
            _overrideLocalization = serializedObject.FindProperty("_overrideLocalization");
            _overrideLocalizationIndex = serializedObject.FindProperty("_overrideLocalizationIndex");

            var cultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            _localizationNames = new String[cultureInfo.Length];

            for (int i = 0; i < cultureInfo.Length; i++)
            {
                _localizationNames[i] = cultureInfo[i].Name;
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Application", EditorStyles.label);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_overrideLocalization);

            if (_overrideLocalization.boolValue)
            {
                _overrideLocalizationIndex.intValue = EditorGUILayout.Popup("Localization Name", _overrideLocalizationIndex.intValue, Localization.AvailableLocalizationNames);
            }

            if (serializedObject.ApplyModifiedProperties())
            {
                Localization.LocalizationUpdate();
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Localization", EditorStyles.label);
            EditorGUI.indentLevel++;

            _selectedIndexCulture = EditorGUILayout.Popup("Localization Name", _selectedIndexCulture, _localizationNames);

            if (GUILayout.Button("Create Localization Asset"))
            {
                CreateAsset(_localizationNames[_selectedIndexCulture]);
            }
        }

        private void CreateAsset(string assetName)
        {
            var asset = CreateInstance<LocalizationAsset>();

            AssetDatabase.CreateAsset(asset, Localization.ResourcesPath + Localization.LocalizationPath + assetName + ".asset");
            AssetDatabase.SaveAssets();

            Selection.activeObject = asset;
        }
    }
}