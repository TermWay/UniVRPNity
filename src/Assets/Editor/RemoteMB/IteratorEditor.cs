using System;
using UnityEditor;
using UnityEngine;

using UniVRPNity.Device;

public class IteratorEditor
{
    public static void ListIterator<Action>(SerializedObject serializedObject, string propertyPath, ref bool visible)
    {
        SerializedProperty listProperty = serializedObject.FindProperty(propertyPath);
        visible = EditorGUILayout.Foldout(visible, listProperty.name);
        if (visible)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < listProperty.arraySize; i++)
            {
                SerializedProperty elementProperty = listProperty.GetArrayElementAtIndex(i);
                UnityEngine.Rect drawZone = UnityEngine.GUILayoutUtility.GetRect(0f, 16f);
                EditorGUI.PropertyField(drawZone, elementProperty,
                    new UnityEngine.GUIContent(((Action)(object)i).ToString()));
            }
            EditorGUI.indentLevel--;
        }
    }
}


