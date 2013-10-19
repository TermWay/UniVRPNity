using System;
using UnityEditor;
using UnityEngine;
using UniVRPNity;

[CustomEditor(typeof(ExampleWiimote))]
class ExampleWiimoteEditor : Editor
{
    SerializedProperty deviceName;
    SerializedProperty deviceIP;
    SerializedProperty transformHandled;
    SerializedProperty sensibilityTranslation;
    SerializedProperty sensibilityRotation;
    SerializedProperty forwardKey;
    SerializedProperty backwardKey;
    SerializedProperty leftKey;
    SerializedProperty rightKey;
    SerializedProperty nunchuk;

    public void OnEnable()
    {
        // Setup the SerializedProperties
        deviceName = serializedObject.FindProperty("deviceName");
        deviceIP = serializedObject.FindProperty("deviceIP");
        transformHandled = serializedObject.FindProperty("transformHandled");
        sensibilityTranslation = serializedObject.FindProperty("sensibilityTranslation");
        sensibilityRotation = serializedObject.FindProperty("sensibilityRotation");
        forwardKey = serializedObject.FindProperty("forwardKey");
        backwardKey = serializedObject.FindProperty("backwardKey");
        leftKey = serializedObject.FindProperty("leftKey");
        rightKey = serializedObject.FindProperty("rightKey");
        nunchuk = serializedObject.FindProperty("nunchuk");
    }

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();

        deviceName.stringValue = EditorGUILayout.TextField("Device Name", deviceName.stringValue);
        deviceIP.stringValue = EditorGUILayout.TextField("Device IP", deviceIP.stringValue);
        transformHandled.objectReferenceValue = EditorGUILayout.ObjectField("Transform Handled", transformHandled.objectReferenceValue, typeof(Transform), true);

        sensibilityTranslation.vector3Value = EditorGUILayout.Vector3Field("Sensibility Translation", sensibilityTranslation.vector3Value);
        sensibilityRotation.vector3Value = EditorGUILayout.Vector3Field("Sensibility Rotation", sensibilityRotation.vector3Value);

        forwardKey.enumValueIndex = EditorGUILayout.Popup("Forward", forwardKey.enumValueIndex, forwardKey.enumNames);
        backwardKey.enumValueIndex = EditorGUILayout.Popup("Backward", backwardKey.enumValueIndex, backwardKey.enumNames);
        leftKey.enumValueIndex = EditorGUILayout.Popup("Left", leftKey.enumValueIndex, leftKey.enumNames);
        rightKey.enumValueIndex = EditorGUILayout.Popup("Right", rightKey.enumValueIndex, rightKey.enumNames);

        nunchuk.boolValue = EditorGUILayout.Toggle("Nunchuk", nunchuk.boolValue);

        if (GUI.changed) EditorUtility.SetDirty(target);

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }
}