using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPCBrain))]
public class NPCManagement : Editor
{
    public override void OnInspectorGUI()
    {
        // Cast the target to YourScript
        NPCBrain script = (NPCBrain)target;

        // Draw the default inspector GUI
        DrawDefaultInspector();

        // Show or hide parameters based on the enum selection
        switch (script.MoveType)
        {
            case AgentMoveType.RandomInRange:
                // Show parameter1 and parameter2
                EditorGUILayout.LabelField("Option 1 Parameters", EditorStyles.boldLabel);
                script.MinX = EditorGUILayout.FloatField("MinX", script.MinX);
                script.MinZ = EditorGUILayout.FloatField("MinZ", script.MinZ);
                script.MaxX = EditorGUILayout.FloatField("MaxX", script.MaxX);
                script.MaxZ = EditorGUILayout.FloatField("MaxZ", script.MaxZ);
                break;
            case AgentMoveType.Option2:
                // Show parameter1
                // Hide parameter2
                break;
            case AgentMoveType.Option3:
                // Hide parameter1 and parameter2
                break;
        }

        // Apply modifications to the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}
