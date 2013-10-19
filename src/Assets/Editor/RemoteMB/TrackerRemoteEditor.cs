using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using UniVRPNity;
[CustomEditor(typeof(TrackerRemoteMB))]
public class TrackerRemoteEditor : RemoteEditor
{
     private bool allTrackersVisible = true;
     private List<bool> trackersVisible; 

     public override void OnEnable()
     {
         base.OnEnable();
      
     }

     public override void OnInspectorGUI()
     {
        serializedObject.Update();

        TrackerRemoteMB remote = (TrackerRemoteMB)target;
        if (remote.Sensors != null && trackersVisible == null)
            trackersVisible = new List<bool>(new bool[remote.Sensors.Count]);
        
        base.OnInspectorGUI();
        this.TrackerIterator(ref allTrackersVisible, ref trackersVisible);

         if (GUI.changed) 
             EditorUtility.SetDirty(target);
         serializedObject.ApplyModifiedProperties();
    }

    public void TrackerIterator(ref bool allTrackersVisible, ref List<bool> trackersVisible)
    {
        TrackerRemoteMB remote = (TrackerRemoteMB) target;
        allTrackersVisible = EditorGUILayout.Foldout(allTrackersVisible, "Trackers");
        if (allTrackersVisible && remote.Sensors != null)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < remote.Sensors.Count; i++)
            {
                trackersVisible[i] = EditorGUILayout.Foldout(trackersVisible[i], "Tracker " + i);
                if (trackersVisible[i])
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.Vector3Field("Position", remote.Position(i));
                    EditorGUILayout.Vector4Field("Orientation",  Utils.Convert(remote.Orientation(i)));
                    EditorGUI.indentLevel--;
                }
            }
            EditorGUI.indentLevel--;
        }
    }
}


