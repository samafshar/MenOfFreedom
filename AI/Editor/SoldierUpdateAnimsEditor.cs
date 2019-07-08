using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SoldierUpdateAnims))]
public class SoldierUpdateAnimsEditor : Editor {
    SoldierUpdateAnims targetObj;
    SerializedObject _serializedObject;

    void OnEnable()
    {
        targetObj = target as SoldierUpdateAnims;
        _serializedObject = new SerializedObject(target);
    }

    public override void OnInspectorGUI()
    {
        _serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
        targetObj.animType = EditorGUILayout.ObjectField(targetObj.animType, typeof(AnimType)) as AnimType;
        EditorGUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUI.color = Color.Lerp(Color.white, Color.cyan, .75f);
        if (GUI.changed) _serializedObject.ApplyModifiedProperties();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Update Animations!", EditorStyles.miniButton))
        {
            targetObj.UpdateAnims();
        }
    }
}
