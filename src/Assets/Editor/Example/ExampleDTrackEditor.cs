using System;
using UnityEditor;
using UnityEngine;
using UniVRPNity;

[CustomEditor(typeof(ExampleDTrack))]
class ExampleDTrackEditor : Editor
{
    SerializedProperty deviceName;
    SerializedProperty deviceIP;
    SerializedProperty transformHandled;
    SerializedProperty sensor;

    public void OnEnable()
    {
        // Setup the SerializedProperties
        deviceName = serializedObject.FindProperty("deviceName");
        deviceIP = serializedObject.FindProperty("deviceIP");
        transformHandled = serializedObject.FindProperty("transformHandled");
        sensor = serializedObject.FindProperty("sensor");
    }

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();

        deviceName.stringValue = EditorGUILayout.TextField("Device Name", deviceName.stringValue);
        deviceIP.stringValue = EditorGUILayout.TextField("Device IP", deviceIP.stringValue);
        transformHandled.objectReferenceValue = EditorGUILayout.ObjectField("Transform Handled", transformHandled.objectReferenceValue, typeof(Transform), true);
        sensor.intValue = EditorGUILayout.IntField("Sensor", sensor.intValue);

        if (GUI.changed) EditorUtility.SetDirty(target);

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }
}