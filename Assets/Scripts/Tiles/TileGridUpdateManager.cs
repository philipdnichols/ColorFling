using UnityEngine;
using System;
using System.Collections;

using Random = UnityEngine.Random;

[RequireComponent(typeof(TileGridManager))]
[RequireComponent(typeof(TileAnimationManager))]
[RequireComponent(typeof(TileGridBuilder))]
public class TileGridUpdateManager : MonoBehaviour {
	public ColorManager colorManager;

	public bool fillEmptyTiles = true;

	public CardinalDirection gravityDirection = CardinalDirection.Down;

	// Required Components:
	TileGridManager tileGridManager;
	TileAnimationManager tileAnimationManager;
	TileGridBuilder tileGridBuilder;

	// Use this for initialization
	void Start() {
	
	}

	void Awake() {
		tileGridManager = GetComponent<TileGridManager>();
		tileAnimationManager = GetComponent<TileAnimationManager>();
		tileGridBuilder = GetComponent<TileGridBuilder>();
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	public CardinalDirection GetRandomTileDirection() {
		Array tileDirectionValues = Enum.GetValues(typeof(CardinalDirection));
		return (CardinalDirection) tileDirectionValues.GetValue(Random.Range(0, tileDirectionValues.Length));
	}

	public void UpdateTileGrid() {
		UpdateTileGrid(gravityDirection);
	}

	void UpdateTileGrid(CardinalDirection direction) {
		switch (direction) {
		case CardinalDirection.Up:
			UpdateTileGridUp();
			break;
			
		case CardinalDirection.Down:
			UpdateTileGridDown();
			break;
			
		case CardinalDirection.Left:
			UpdateTileGridLeft();
			break;
			
		case CardinalDirection.Right:
			UpdateTileGridRight();
			break;
			
		default:
			break;
		}
	}
	
	void UpdateTileGridUp() {
		// Move existing tiles:
		for (int x = 0; x < tileGridManager.numColumns; x++) {
			for (int y = tileGridManager.numRows - 1; y >= 0; y--) {
				Tile tile = tileGridManager.GetTileAt(x, y);
				if (tile == null) {
					if (!FillTileFromDirection(CardinalDirection.Down, x, y)) {
						break;
					}
				}
			}
		}
		
		// Create new tiles for empty spots:
		if (fillEmptyTiles) {
			for (int x = 0; x < tileGridManager.numColumns; x++) {
				for (int y = tileGridManager.numRows - 1; y >= 0; y--) {
					Tile tile = tileGridManager.GetTileAt(x, y);
					if (tile == null) {
						CreateTileInDirection(CardinalDirection.Down, x, y);
					}
				}
			}
		}
	}
	
	void UpdateTileGridDown() {
		// Move existing tiles:
		for (int x = 0; x < tileGridManager.numColumns; x++) {
			for (int y = 0; y < tileGridManager.numRows; y++) {
				Tile tile = tileGridManager.GetTileAt(x, y);
				if (tile == null) {
					if (!FillTileFromDirection(CardinalDirection.Up, x, y)) {
						break;
					}
				}
			}
		}
		
		// Create new tiles for empty spots:
		if (fillEmptyTiles) {
			for (int x = 0; x < tileGridManager.numColumns; x++) {
				for (int y = 0; y < tileGridManager.numRows; y++) {
					Tile tile = tileGridManager.GetTileAt(x, y);
					if (tile == null) {
						CreateTileInDirection(CardinalDirection.Up, x, y);
					}
				}
			}
		}
	}
	
	void UpdateTileGridLeft() {
		// Move existing tiles:
		for (int y = 0; y < tileGridManager.numRows; y++) {
			for (int x = 0; x < tileGridManager.numColumns; x++) {
				Tile tile = tileGridManager.GetTileAt(x, y);
				if (tile == null) {
					if (!FillTileFromDirection(CardinalDirection.Right, x, y)) {
						break;
					}
				}
			}
		}
		
		// Create new tiles for empty spots:
		if (fillEmptyTiles) {
			for (int y = 0; y < tileGridManager.numRows; y++) {
				for (int x = 0; x < tileGridManager.numColumns; x++) {
					Tile tile = tileGridManager.GetTileAt(x, y);
					if (tile == null) {
						CreateTileInDirection(CardinalDirection.Right, x, y);
					}
				}
			}
		}
	}
	
	void UpdateTileGridRight() {
		// Move existing tiles:
		for (int y = 0; y < tileGridManager.numRows; y++) {
			for (int x = tileGridManager.numColumns - 1; x >= 0; x--) {
				Tile tile = tileGridManager.GetTileAt(x, y);
				if (tile == null) {
					if (!FillTileFromDirection(CardinalDirection.Left, x, y)) {
						break;
					}
				}
			}
		}
		
		// Create new tiles for empty spots:
		if (fillEmptyTiles) {
			for (int y = 0; y < tileGridManager.numRows; y++) {
				for (int x = tileGridManager.numColumns - 1; x >= 0; x--) {
					Tile tile = tileGridManager.GetTileAt(x, y);
					if (tile == null) {
						CreateTileInDirection(CardinalDirection.Left, x, y);
					}
				}
			}
		}
	}
	
	bool FillTileFromDirection(CardinalDirection direction, int x, int y) {
		Tile nextTile = GetNextTileInDirection(direction, x, y);
		if (nextTile != null) {
			int nextTileX = (int) nextTile.TilePosition.x;
			int nextTileY = (int) nextTile.TilePosition.y;
			tileGridManager.ClearTile(nextTileX, nextTileY);
			tileGridManager.SetTileAt(x, y, nextTile);

			Vector3 targetPosition = tileGridManager.GetTileWorldPosition(x, y);
			tileAnimationManager.MoveTile(nextTile, targetPosition, 5.0f, iTween.EaseType.easeInCirc);
			
			return true;
		}
		
		return false;
	}
	
	void CreateTileInDirection(CardinalDirection direction, int x, int y) {
		Vector3 position = tileGridBuilder.GetTileWorldPosition(x, y, 
		                                                        tileGridManager.tileWidth, tileGridManager.tileHeight, 
		                                                        tileGridManager.rowPadding, tileGridManager.columnPadding, 
		                                                        transform.position);
		
		switch (direction) {
		case CardinalDirection.Up:
			position.y += tileGridManager.GridHeight / Globals.Instance.pixelsToUnits;
			break;
			
		case CardinalDirection.Down:
			position.y -= tileGridManager.GridHeight / Globals.Instance.pixelsToUnits;
			break;
			
		case CardinalDirection.Left:
			position.x -= tileGridManager.GridWidth / Globals.Instance.pixelsToUnits;
			break;
			
		case CardinalDirection.Right:
			position.x += tileGridManager.GridWidth / Globals.Instance.pixelsToUnits;
			break;
			
		default:
			break;
		}
		
		Tile tile = tileGridBuilder.BuildTile(tileGridManager.tilePrefab, 
		                                      tileGridManager.tileWidth, tileGridManager.tileHeight, 
		                                      position);
		tile.Color = colorManager.RandomColor();

		tileGridManager.SetTileAt(x, y, tile);

		Vector3 targetPosition = tileGridManager.GetTileWorldPosition(x, y);
		tileAnimationManager.MoveTile(tile, targetPosition, 10.0f, iTween.EaseType.easeInCirc);
	}
	
	Tile GetNextTileInDirection(CardinalDirection direction, int x, int y) {
		switch (direction) {
		case CardinalDirection.Up:
			return GetNextTileUp(x, y);
			
		case CardinalDirection.Down:
			return GetNextTileDown(x, y);
			
		case CardinalDirection.Left:
			return GetNextTileLeft(x, y);
			
		case CardinalDirection.Right:
			return GetNextTileRight(x, y);
			
		default:
			return null;
		}
	}
	
	Tile GetNextTileUp(int x, int y) {
		y++;
		for (; y < tileGridManager.numRows; y++) {
			Tile tile = tileGridManager.GetTileAt(x, y);
			if (tile != null) {
				return tile;
			}
		}
		return null;
	}
	
	Tile GetNextTileDown(int x, int y) {
		y--;
		for (; y >= 0; y--) {
			Tile tile = tileGridManager.GetTileAt(x, y);
			if (tile != null) {
				return tile;
			}
		}
		return null;
	}
	
	Tile GetNextTileLeft(int x, int y) {
		x--;
		for (; x >= 0; x--) {
			Tile tile = tileGridManager.GetTileAt(x, y);
			if (tile != null) {
				return tile;
			}
		}
		return null;
	}
	
	Tile GetNextTileRight(int x, int y) {
		x++;
		for (; x < tileGridManager.numColumns; x++) {
			Tile tile = tileGridManager.GetTileAt(x, y);
			if (tile != null) {
				return tile;
			}
		}
		return null;
	}
}