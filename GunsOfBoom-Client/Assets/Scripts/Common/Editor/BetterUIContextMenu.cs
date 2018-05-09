using System.Linq;
using UnityEditor;
using UnityEngine;

public static class BetterUIContextMenu
{
    private const string PrefabsPath = "Components/";
    private static GameObject _currentObject;

    [MenuItem("GameObject/BetterUI/Label", false, 5)]
    private static void SpawnLabel()
    {
        SpawnPrefabComponent("Label");
    }

    [MenuItem("GameObject/BetterUI/Button", false, 5)]
    private static void SpawnButton()
    {
        SpawnPrefabComponent("Button");
    }

    private static void SpawnPrefabComponent(string componentName)
    {
        var componentPrefab = Resources.Load(PrefabsPath + componentName) as GameObject;

        if (componentPrefab == null)
        {
            Debug.LogError(componentName + " cannot be loaded from: " + PrefabsPath + componentName);
            return;
        }

        var selectedGameObject = Selection.activeGameObject;
            
        if (selectedGameObject == null)
        {
            _currentObject = Object.Instantiate(componentPrefab);
        }
        else
        {
            _currentObject = Object.Instantiate(componentPrefab, selectedGameObject.transform);
            _currentObject.name = componentPrefab.name;
        }

        Undo.RegisterCreatedObjectUndo(_currentObject, "Create Object");

        // Remove the selected object from the list
        Selection.objects = (from x in Selection.objects where x != Selection.activeObject select x).ToArray();

        if (Selection.objects.Length < 1)
        {
            Selection.activeObject = _currentObject;
        }
    }
}