using UnityEditor;
using UnityEngine;
public static class GroupCommand
{
    [MenuItem("GameObject/Group Selected %g")]
    private static void GroupSelected()
    {
        if (!Selection.activeTransform) return;
        var go = new GameObject(Selection.activeTransform.name + " Group");
        Undo.RegisterCreatedObjectUndo(go, "Group Selected");
        go.transform.SetParent(Selection.activeTransform.parent, false);
        var script = go.AddComponent<UnrootChildrenAndDeleteOnRunTime>();
        foreach (var transform in Selection.transforms) Undo.SetTransformParent(transform, go.transform, "Group Selected");
        Selection.activeGameObject = go;
    }
}

[InitializeOnLoad]
public class HierarchyHighlighter
{
    static HierarchyHighlighter()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (gameObject != null && gameObject.name.EndsWith(" Group"))
        {
            Rect rect = new Rect(selectionRect);
            rect.x = 0;
            rect.width = rect.width + 200;

            EditorGUI.DrawRect(rect, new Color(1f, 0.5f, 0.5f, 0.3f)); // You can change the color here
        }
    }
}