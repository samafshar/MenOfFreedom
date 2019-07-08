using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CopyRagd))]
public class CopyRagdEditor : Editor
{
    CopyRagd targetObj;
    SerializedObject _serializedObject;

    void OnEnable()
    {
        targetObj = target as CopyRagd;
        _serializedObject = new SerializedObject(target);
    }

    public override void OnInspectorGUI()
    {
        _serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Scale factor: ");
        targetObj.scaleFactor = EditorGUILayout.FloatField(targetObj.scaleFactor);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Source Bip001: ");
        targetObj.sourceBip001 = EditorGUILayout.ObjectField(targetObj.sourceBip001, typeof(Transform)) as Transform;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Destination Bip001: ");
        targetObj.destBip001 = EditorGUILayout.ObjectField(targetObj.destBip001, typeof(Transform)) as Transform;
        EditorGUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Start copying honey! AAAAA...", EditorStyles.miniButton))
        {
            targetObj.StartCopyingHoney();
        }
        EditorGUILayout.EndHorizontal();
    }
}
