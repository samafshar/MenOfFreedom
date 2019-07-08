using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(FindObjectsForOcclusionCulling))]
public class FindObjectsForOcclusionCullingEditor : Editor
{
    FindObjectsForOcclusionCulling targetObj;
    SerializedObject serializedObj;

    void OnEnable()
    {
        targetObj = target as FindObjectsForOcclusionCulling;
        serializedObj = new SerializedObject(targetObj);
    }

    public override void OnInspectorGUI()
    {
        serializedObj.Update();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Relative Objects:");
        targetObj.OccList = EditorGUILayout.ObjectField(targetObj.OccList, typeof(OcclusionObjectsList)) as OcclusionObjectsList;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("---------------------------------------------------------------------------------------------------------");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Find Relative Objects", EditorStyles.miniButtonMid) )
        {
            targetObj.FindRelativeObjects();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("---------------------------------------------------------------------------------------------------------");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Restore Hidden Objects", EditorStyles.miniButtonMid))
        {
            targetObj.RestoreHiddenObjects();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("---------------------------------------------------------------------------------------------------------");
        EditorGUILayout.EndHorizontal();

        //EditorGUILayout.BeginHorizontal();
        //if (GUILayout.Button("Remove All Occ. Component", EditorStyles.toolbarButton))
        //{
        //    targetObj.RemoveOcclusionTypeComponent();
        //}
        //EditorGUILayout.EndHorizontal();
    }
}
