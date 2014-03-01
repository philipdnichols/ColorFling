using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(TileGridManager))]
public class TileGridManagerInspector : Editor {
	public override void OnInspectorGUI() {
		// base.OnInspectorGUI();
		DrawDefaultInspector();
		
//		if (GUILayout.Button("Build Tile Grid")) {
//			TileGridManager tileGridManager = (TileGridManager) target;
//			
//			tileGridManager.BuildTileGrid();
//		}
	}
}