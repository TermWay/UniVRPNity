using System;
using UnityEditor;
using UnityEngine;

using UniVRPNity.Device;

public class RemoteEditor : Editor
{
    protected SerializedProperty deviceName;
    protected SerializedProperty vrpnServerAddress;
    protected SerializedProperty uniVRPNityAddressServer;
    protected SerializedProperty uniVRPNityPortServer; 

    public virtual void OnEnable()
    {
        deviceName = serializedObject.FindProperty("Name");
        vrpnServerAddress = serializedObject.FindProperty("VRPNAddressServer");
        uniVRPNityAddressServer = serializedObject.FindProperty("UniVRPNityAddressServer");
        uniVRPNityPortServer = serializedObject.FindProperty("UniVRPNityPortServer");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        deviceName.stringValue = EditorGUILayout.TextField("Name", deviceName.stringValue);
        vrpnServerAddress.stringValue = EditorGUILayout.TextField("VRPN server IP", vrpnServerAddress.stringValue);
        uniVRPNityAddressServer.stringValue = EditorGUILayout.TextField("UniVRPNity server IP", uniVRPNityAddressServer.stringValue);
        uniVRPNityPortServer.intValue = EditorGUILayout.IntField("UniVRPNity server Port", uniVRPNityPortServer.intValue);


        if (GUI.changed) EditorUtility.SetDirty(target);
            serializedObject.ApplyModifiedProperties();
    }

    public void ListIterator<Action>(string propertyPath, ref bool visible)
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


