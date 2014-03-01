using UnityEngine;
using System.Collections;

public class TileGridBuilder : MonoBehaviour {
	public Tile[,] BuildTileGrid(GameObject tilePrefab, 
	                             int numRows, int numColumns, 
	                             int tileWidth, int tileHeight, 
	                             int rowPadding, int columnPadding) {
		Tile[,] tiles = new Tile[numRows, numColumns];

		Vector3 position = Vector3.zero;
		for (int y = 0; y < numRows; y++) {
			position.y = GetTileWorldPositionY(y, tileHeight, rowPadding, 0.0f);

			for (int x = 0; x < numColumns; x++) {
				position.x = GetTileWorldPositionX(x, tileWidth, columnPadding, 0.0f);
				
				Tile tile = BuildTile(tilePrefab, tileWidth, tileHeight, position);
				tile.TilePosition = new Vector2(x, y);
				tiles[y, x] = tile;
			}
		}

		return tiles;
	}

	public Tile BuildTile(GameObject tilePrefab, int tileWidth, int tileHeight, Vector3 position) {
		GameObject tileGO = (GameObject) Instantiate(tilePrefab);
		tileGO.transform.localScale = new Vector3(tileWidth, tileHeight, 1.0f);

		tileGO.transform.position = position;
		
		Tile tile = tileGO.GetComponent<Tile>();
		return tile;
	}

	public float GetTileWorldPositionX(int x, int tileWidth, int columnPadding, float offset) {
		return (float) ((x * (tileWidth + columnPadding)) + (tileWidth / 2)) / Globals.Instance.pixelsToUnits + offset;
	}

	public float GetTileWorldPositionY(int y, int tileHeight, int rowPadding, float offset) {
		return (float) ((y * (tileHeight + rowPadding)) + (tileHeight / 2)) / Globals.Instance.pixelsToUnits + offset;
	}

	public float GetTileWorldPositionZ(float offset) {
		return 0.0f + offset;
	}

	public Vector3 GetTileWorldPosition(int x, int y, 
	                                    int tileWidth, int tileHeight,
	                                    int rowPadding, int columnPadding,
	                                    Vector3 offset) {
		return new Vector3(GetTileWorldPositionX(x, tileWidth, columnPadding, offset.x),
		                   GetTileWorldPositionY(y, tileHeight, rowPadding, offset.y),
		                   GetTileWorldPositionZ(offset.z));
	}
}