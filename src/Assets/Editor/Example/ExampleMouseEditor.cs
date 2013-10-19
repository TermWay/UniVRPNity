using System;
using UnityEditor;
using UnityEngine;
using UniVRPNity;

[CustomEditor(typeof(ExampleMouse))]
class ExampleMouseEditor : Editor
{
    SerializedProperty deviceName;
    SerializedProperty deviceIP;
    SerializedProperty transformHandled;
    //Mouse
    SerializedProperty sensibility;
    SerializedProperty bubble;
    SerializedProperty sensibilityBubble;
    SerializedProperty bubbleLeft;
    SerializedProperty bubbleRight;
    SerializedProperty bubbleUp;
    SerializedProperty bubbleDown;

    public void OnEnable()
    {
        // Setup the SerializedProperties
        deviceName = serializedObject.FindProperty("deviceName");
        deviceIP = serializedObject.FindProperty("deviceIP");
        transformHandled = serializedObject.FindProperty("transformHandled");
        //Mouse
        sensibility = serializedObject.FindProperty("sensibility");
        bubble = serializedObject.FindProperty("bubble");
        sensibilityBubble = serializedObject.FindProperty("sensibilityBubble");
        bubbleLeft = serializedObject.FindProperty("bubbleLeft");
        bubbleRight = serializedObject.FindProperty("bubbleRight");
        bubbleUp = serializedObject.FindProperty("bubbleUp");
        bubbleDown = serializedObject.FindProperty("bubbleDown");
    }

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();

        deviceName.stringValue = EditorGUILayout.TextField("Device Name", deviceName.stringValue);
        deviceIP.stringValue = EditorGUILayout.TextField("Device IP", deviceIP.stringValue);
        transformHandled.objectReferenceValue = EditorGUILayout.ObjectField("Transform Handled", transformHandled.objectReferenceValue, typeof(Transform), true);

        sensibility.vector2Value = EditorGUILayout.Vector2Field("Sensibility", sensibility.vector2Value);
        bubble.boolValue = EditorGUILayout.Toggle("Bubble", bubble.boolValue);
        if (bubble.boolValue)
        {
            sensibilityBubble.vector2Value = EditorGUILayout.Vector2Field("sensibilityBubble:", sensibilityBubble.vector2Value);
            bubbleLeft.floatValue = EditorGUILayout.FloatField("bubbleLeft:", bubbleLeft.floatValue);
            bubbleRight.floatValue = EditorGUILayout.FloatField("bubbleRight:", bubbleRight.floatValue);
            bubbleUp.floatValue = EditorGUILayout.FloatField("bubbleUp:", bubbleUp.floatValue);
            bubbleDown.floatValue = EditorGUILayout.FloatField("bubbleDown:", bubbleDown.floatValue);
        }

        if (GUI.changed) EditorUtility.SetDirty(target);

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }
}