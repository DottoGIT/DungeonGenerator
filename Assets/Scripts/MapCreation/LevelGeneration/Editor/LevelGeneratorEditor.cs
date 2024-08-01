using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelGenerator levelGeneration = (LevelGenerator)target;

        if (GUILayout.Button("Generate New Level"))
        {
            levelGeneration.RenerateLevel();
        }
    }
}
