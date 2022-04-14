using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMapManager))]
public class TileMapManagerCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TileMapManager tileMapManager = (TileMapManager)target;
        if (GUILayout.Button("Load Universe 0"))
        {
            tileMapManager.Setup();
            tileMapManager.SwapAllTiles(0);
        }
        if (GUILayout.Button("Load Universe 1"))
        {
            tileMapManager.Setup();
            tileMapManager.SwapAllTiles(1);
        }
    }
}
