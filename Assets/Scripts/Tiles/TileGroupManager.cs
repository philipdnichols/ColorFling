using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TileGridManager))]
[RequireComponent(typeof(TileFlingManager))]
public class TileGroupManager : MonoBehaviour {
	public GameObject tileGroupPrefab;

	public float timeAfterScoredToDestroy = 1.0f;

	public BucketsManager bucketsManager;

	// Required Components:
	TileGridManager tileGridManager;
	TileFlingManager tileFlingManager;

	// Use this for initialization
	void Start() {

	}

	void Awake() {
		tileGridManager = GetComponent<TileGridManager>();
		tileFlingManager = GetComponent<TileFlingManager>();
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
					tileGroup.IsAttachedToGrid = true;
					
					BuildTileGroup(tile, tileGroup);

					UpdateTileGroupLayer(tileGroup);
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

	public void UpdateTileGroupLayers() {
		TileGroup[] tileGroups = GetComponentsInChildren<TileGroup>();

		foreach (TileGroup tileGroup in tileGroups) {
			UpdateTileGroupLayer(tileGroup);
		}

		foreach (TileGroup flungTileGroup in tileFlingManager.FlungTileGroups) {
			UpdateTileGroupLayer(flungTileGroup);
		}
	}

	public void UpdateTileGroupLayer(TileGroup tileGroup) {
		if (!tileGroup.IsScored) {
			if (bucketsManager.ColorToLayer.ContainsKey(tileGroup.Color)) {
				tileGroup.SetLayer(Globals.Instance.BucketLayersToTileLayers[bucketsManager.ColorToLayer[tileGroup.Color]]);
			} else {
				tileGroup.SetLayer(Globals.Layers.TILE_NULL);
			}
		}
	}
}