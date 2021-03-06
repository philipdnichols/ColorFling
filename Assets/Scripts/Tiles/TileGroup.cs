﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileGroup : MonoBehaviour {
	List<Tile> tiles = new List<Tile>();

	bool isScored = false;

	float timeAfterScoredToDestroy = 1.0f;

	bool isAttachedToGrid = false;

	public List<Tile> Tiles {
		get {
			return tiles;
		}
	}

	public Color Color {
		get {
			if (tiles.Count != 0) {
				return tiles[0].Color;
			} else {
				return Color.white;
			}
		}
	}

	public bool IsScored {
		get {
			return isScored;
		}

		set {
			isScored = value;
		}
	}

	public float TimeAfterScoredToDestroy {
		get {
			return timeAfterScoredToDestroy;
		}

		set {
			timeAfterScoredToDestroy = value;
		}
	}

	public bool IsAttachedToGrid {
		get {
			return isAttachedToGrid;
		}

		set {
			isAttachedToGrid = value;
		}
	}

	// Use this for initialization
	void Start() {

	}
	
	// Update is called once per frame
	void Update() {
	
	}

	public void AddTile(Tile tile) {
		tile.TileGroup = this;
		tile.transform.parent = transform;
		tile.gameObject.layer = gameObject.layer;
		tiles.Add(tile);
	}

	public void RemoveTile(Tile tile) {
		tiles.Remove(tile);
		tile.transform.parent = null;
		tile.TileGroup = null;
	}

	public void RemoveTileForRemoveAll(Tile tile) {
		tile.transform.parent = null;
		tile.TileGroup = null;
	}

	public Tile[] RemoveAllTiles() {
		foreach (Tile tile in tiles) {
			RemoveTileForRemoveAll(tile);
		}

		Tile[] tilesRemoved = tiles.ToArray();

		tiles.Clear();

		return tilesRemoved;
	}

	public void DestroyAllTiles() {
		foreach (Tile tile in tiles) {
			Destroy(tile.gameObject);
		}

		tiles.Clear();
	}

	public void MovePositionIndependentOfChildren(Vector2 position) {
		foreach (Tile tile in tiles) {
			tile.transform.parent = null;
		}

		transform.position = position;

		foreach (Tile tile in tiles) {
			tile.transform.parent = transform;
		}
	}

	public void SetLayer(string layer) {
		int layerInt = LayerMask.NameToLayer(layer);

		gameObject.layer = layerInt;

		foreach (Tile tile in tiles) {
			tile.gameObject.layer = layerInt;
		}
	}

	public void Scored() {
		isScored = true;

		StartCoroutine("DestroyAfter", timeAfterScoredToDestroy);
	}

	public void NoScored() {
		isScored = true;

		StartCoroutine("DestroyAfter", timeAfterScoredToDestroy);
	}

	IEnumerator DestroyAfter(float t) {
		yield return new WaitForSeconds(t);

		Destroy(gameObject);
	}
}