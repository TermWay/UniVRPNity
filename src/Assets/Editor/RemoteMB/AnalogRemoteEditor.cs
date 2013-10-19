using System;
using UnityEditor;
using UnityEngine;

using UniVRPNity;
using UniVRPNity.Device;

[CustomEditor(typeof(AnalogRemoteMB))]
public class AnalogRemoteEditor : RemoteEditor
{
     public bool channelsVisible = false;

     protected SerializedProperty layout;

     public override void OnEnable()
     {
         base.OnEnable();
         layout = serializedObject.FindProperty("Layout.Type");
     }

     public override void OnInspectorGUI()
     {
        serializedObject.Update();

        base.OnInspectorGUI();
        //layout.objectReferenceValue = EditorGUILayout.EnumPopup("Layout type", Layout.DeviceType.Undefined);
        layout.enumValueIndex = (int)(Layout.DeviceType)EditorGUILayout.EnumPopup("Layout",
            (Layout.DeviceType)Enum.GetValues(typeof(Layout.DeviceType)).GetValue(layout.enumValueIndex));
        
         this.AnalogIterator(ref channelsVisible);

         if (GUI.changed) EditorUtility.SetDirty(target);
            serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// List all channels from an enum.
    /// </summary>
    /// <param name="visible">Is all analog value visible?</param>
    public void AnalogIterator(ref bool visible)
    {
        AnalogRemote remote = ((AnalogRemoteMB)target).AnalogRemote;
        AnalogEvent analogEvent = remote.LastEvent;
        visible = EditorGUILayout.Foldout(visible, "Channels");
        if (visible && analogEvent != null)
        {
            EditorGUI.indentLevel++;
            System.Type layout = ((AnalogRemoteMB)target).Layout.GetAnalogEnum();
            if (layout != null)
            {
               
                Array values = Enum.GetValues(layout);
                for (int i = 0; i < Math.Min(analogEvent.getNumChannels(), values.Length); i++)
                {
                    EditorGUILayout.FloatField(values.GetValue(i).ToString(), (float)analogEvent.Channel(i));
                }
            }
            else
            {
                for (int i = 0; i < analogEvent.getNumChannels(); i++)
                {
                    EditorGUILayout.FloatField((float)analogEvent.Channel(i));
                }
            }
            EditorGUI.indentLevel--;
        }
    }
}


