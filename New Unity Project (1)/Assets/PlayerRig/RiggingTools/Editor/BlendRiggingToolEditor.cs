using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlendRiggingTool))]
public class Blend : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Rig")) {
            BlendRiggingTool myTarget = (BlendRiggingTool)target;
            myTarget.BlendRigCharacter();
        }
    }
}
