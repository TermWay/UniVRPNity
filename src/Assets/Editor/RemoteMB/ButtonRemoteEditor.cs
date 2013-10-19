using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using UniVRPNity;
using UniVRPNity.Device;

[CustomEditor(typeof(ButtonRemoteMB))]
public class ButtonRemoteEditor : RemoteEditor
{
     public bool buttonVisible = false;

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
        layout.enumValueIndex = (int)(Layout.DeviceType)EditorGUILayout.EnumPopup("Layout",
             (Layout.DeviceType)Enum.GetValues(typeof(Layout.DeviceType)).GetValue(layout.enumValueIndex));
        
        this.ButtonIterator(ref buttonVisible);

         if (GUI.changed) EditorUtility.SetDirty(target);
            serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// Use layout to display button states. If display is incomplete then layout will be too.
    /// </summary>
    /// <param name="visible"></param>
    public void ButtonIterator(ref bool visible)
    {
        ButtonRemote remote = ((ButtonRemoteMB)target).ButtonRemote;
        List<ButtonEvent> buttonsEvent = remote.buttonStates;
        visible = EditorGUILayout.Foldout(visible, "States");
        if (visible && buttonsEvent.Count != 0)
        {
            EditorGUI.indentLevel++;
            System.Type layout = ((ButtonRemoteMB)target).Layout.GetButtonEnum();
            if (layout != null)
            {
                Array values = Enum.GetValues(layout);
                int i = 0;
                foreach (int v in values)
                {
                    EditorGUILayout.Toggle(values.GetValue(i).ToString(), buttonsEvent[v].State);
                    i++;
                }
            }
            else
            {
                for (int i = 0; i < remote.GetNumButtons(); i++)
                {
                    EditorGUILayout.Toggle(i.ToString(), buttonsEvent[i].State);
                }
            }
            EditorGUI.indentLevel--;
        }
    }
}


