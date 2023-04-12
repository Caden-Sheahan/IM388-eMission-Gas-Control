using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GasSpawner))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GasSpawner myScript = (GasSpawner)target;
        if (GUILayout.Button("Simplify Range"))
        {
            myScript.Simplify();
        }
    }
}
