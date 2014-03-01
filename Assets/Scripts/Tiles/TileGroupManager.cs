using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TileGridManager))]
public class TileGroupManager : MonoBehaviour {
	public GameObject tileGroupPrefab;

	public float timeAfterScoredToDestroy = 1.0f;

	// Required Components:
	TileGridManager tileGridManager;

	// Use this for initialization
	void Start() {

	}

	void Awake() {
		tileGridManager = GetComponent<TileGridManager>();
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	public void BuildTileGroups() {
		for (int y = 0; y < tileGridManager.numRows; y++) {
			for (int x = 0; x < tileGridManager.numColumns; x++) {
				Tile tile = tileGridManager.GetTileAt(x, y);
				
				if (tile != null && tile.TileGroup == null) {
					GameObject tileGroupGO = (GameObject) Instantiate(tileGroupPrefab);
					tileGroupGO.transform.parent = transform;
					
					TileGroup tileGroup = tileGroupGO.GetComponent<TileGroup>();
					tileGroup.TimeAfterScoredToDestroy = timeAfterScoredToDestroy;
					
					BuildTileGroup(tile, tileGroup);
				}
			}
		}
	}
	
	void BuildTileGroup(Tile tile, TileGroup tileGroup) {
		if (tile.TileGroup != null) {
			return;
		} else {
			tileGroup.AddTile(tile);
			
			List<Tile> adjacentTilesList = tileGridManager.GetAdjacentTiles(tile);
			foreach (Tile adjacentTile in adjacentTilesList) {
				if (adjacentTile.Color == tile.Color) {
					BuildTileGroup(adjacentTile, tileGroup);
				}
			}
		}
	}

	public void ClearTileGroups() {
		TileGroup[] tileGroups = GetComponentsInChildren<TileGroup>();
		
		List<Tile> tilesRemovedFromGroups = new List<Tile>();
		foreach (TileGroup tileGroup in tileGroups) {
			tilesRemovedFromGroups.AddRange(tileGroup.RemoveAllTiles());
			Destroy(tileGroup.gameObject);
		}
		
		foreach (Tile tile in tilesRemovedFromGroups) {
			tile.transform.parent = transform;
		}
	}

	public void DestroyTileGroupsAndTiles() {
		TileGroup[] tileGroups = GetComponentsInChildren<TileGroup>();

		foreach (TileGroup tileGroup in tileGroups) {
			tileGroup.DestroyAllTiles();
			Destroy(tileGroup.gameObject);
		}
	}

	public void ClearAllTileGroupTiles(TileGroup tileGroup) {
		foreach (Tile childTile in tileGroup.Tiles) {
			int x = (int) childTile.TilePosition.x;
			int y = (int) childTile.TilePosition.y;
			tileGridManager.ClearTile(x, y);
		}
	}
}