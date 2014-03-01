using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TileGridBuilder))]
[RequireComponent(typeof(TileGroupManager))]
[RequireComponent(typeof(TileAnimationManager))]
[RequireComponent(typeof(TileGridUpdateManager))]
public class TileGridManager : MonoBehaviour {
	public int numRows = 24;
	public int numColumns = 12;

	public int tileWidth = 32;
	public int tileHeight = 32;

	public int rowPadding = 2;
	public int columnPadding = 2;

	public GameObject tilePrefab;

	public Color[] tileColors;

	Tile[,] tiles;

	// Required Components
	TileGridBuilder tileGridBuilder;
	TileGroupManager tileGroupManager;
	TileAnimationManager tileAnimationManager;
	TileGridUpdateManager tileGridUpdateManager;

	public int GridWidth {
		get {
			return (numColumns * tileWidth) + ((numColumns - 1) * columnPadding);
		}
	}

	public int GridHeight {
		get {
			return (numRows * tileHeight) + ((numRows - 1) * rowPadding);
		}
	}

	// TODO better way to keep track of whether or not to allow input
	public bool AllowFlings {
		get {
			return !tileAnimationManager.AnimationsRunning;
		}
	}

	// Use this for initialization
	void Start() {
		InitializeGrid();
	}

	void Awake() {
		tileGridBuilder = GetComponent<TileGridBuilder>();
		tileGroupManager = GetComponent<TileGroupManager>();
		tileAnimationManager = GetComponent<TileAnimationManager>();
		tileGridUpdateManager = GetComponent<TileGridUpdateManager>();
	}
	
	// Update is called once per frame
	void Update() {
		// Always keep the grid centered on screen:
		CenterGridOnScreen();
	}

	void CenterGridOnScreen() {
		Vector3 gridPosition = Camera.main.ScreenToWorldPoint(new Vector2((Screen.width - GridWidth) / 2.0f, (Screen.height - GridHeight) / 2.0f));
		gridPosition.z = 0;
		transform.position = gridPosition;
	}

	public void InitializeGrid() {
		tileGroupManager.DestroyTileGroupsAndTiles();
		transform.position = Vector3.zero;
		BuildTileGrid();
		RandomizeTileColors();
		tileGroupManager.BuildTileGroups();
	}

	void BuildTileGrid() {
		tiles = tileGridBuilder.BuildTileGrid(tilePrefab,
		                                      numRows, numColumns,
		                                      tileWidth, tileHeight, 
		                                      rowPadding, columnPadding);
	}

	void RandomizeTileColors() {
		for (int y = 0; y < numRows; y++) {
			for (int x = 0; x < numColumns; x++) {
				Tile tile = GetTileAt(x, y);
				RandomizeTileColor(tile);
			}
		}
	}

	public void RandomizeTileColor(Tile tile) {
		Color randomTileColor = tileColors[Random.Range(0, tileColors.Length)];
		tile.Color = randomTileColor;
	}

	public void UpdateTileGridManager() {
		tileGroupManager.ClearTileGroups();
		tileGridUpdateManager.UpdateTileGrid();
		tileGroupManager.BuildTileGroups();
	}
	
	public Tile GetTileAt(int x, int y) {
		return tiles[y, x];
	}

	public void SetTileAt(int x, int y, Tile tile) {
		tile.TilePosition = new Vector2(x, y);
		tiles[y, x] = tile;
	}

	public void ClearTile(int x, int y) {
		tiles[y, x] = null;
	}

	public Vector3 GetTileWorldPosition(int x, int y) {
		return tileGridBuilder.GetTileWorldPosition(x, y, 
		                                            tileWidth, tileHeight, 
		                                            rowPadding, columnPadding,
		                                            transform.position);
	}

	public List<Tile> GetAdjacentTiles(Tile tile) {
		int x = (int) tile.TilePosition.x;
		int y = (int) tile.TilePosition.y;
		
		List<Tile> adjacentTilesList = new List<Tile>();
		if (x > 0) {
			Tile leftTile = GetTileAt(x - 1, y);
			if (leftTile != null) {
				adjacentTilesList.Add(leftTile);
			}
		}
		if (y > 0) {
			Tile bottomTile = GetTileAt(x, y - 1);
			if (bottomTile != null) {
				adjacentTilesList.Add(bottomTile);
			}
		}
		if (x < numColumns - 1) {
			Tile rightTile = GetTileAt(x + 1, y);
			if (rightTile != null) {
				adjacentTilesList.Add(rightTile);
			}
		}
		if (y < numRows - 1) {
			Tile topTile = GetTileAt(x, y + 1);
			if (topTile != null) {
				adjacentTilesList.Add(topTile);
			}
		}
		return adjacentTilesList;
	}
}