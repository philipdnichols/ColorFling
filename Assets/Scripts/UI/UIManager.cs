using UnityEngine;
using System;
using System.Collections;

public class UIManager : MonoBehaviour {
	public GameManager gameManager;

	public TileGridUpdateManager tileGridUpdateManager;
	public TileGridManager tileGridManager;

	public UISprite modalBackground;

	bool tileGridManagerUpdates = false;

	int numRowsToUpdate;
	int numColumnsToUpdate;
	int tileWidthToUpdate;
	int tileHeightToUpdate;
	int rowPaddingToUpdate;
	int columnPaddingToUpdate;

	// Use this for initialization
	void Start() {
		numRowsToUpdate = tileGridManager.numRows;
		numColumnsToUpdate = tileGridManager.numColumns;
		tileWidthToUpdate = tileGridManager.tileWidth;
		tileHeightToUpdate = tileGridManager.tileHeight;
		rowPaddingToUpdate = tileGridManager.rowPadding;
		columnPaddingToUpdate = tileGridManager.columnPadding;
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	public void GravityChanged() {
		tileGridUpdateManager.gravityDirection = (CardinalDirection) Enum.Parse(typeof(CardinalDirection), UIPopupList.current.value);
	}

	public void FillEmptyTilesChanged() {
		tileGridUpdateManager.fillEmptyTiles = UIToggle.current.value;
	}

	public void NumRowsChanged() {
		numRowsToUpdate = int.Parse(UIInput.current.value);
		tileGridManagerUpdates = true;
	}

	public void NumColumnsChanged() {
		numColumnsToUpdate = int.Parse(UIInput.current.value);
		tileGridManagerUpdates = true;
	}

	public void TileWidthChanged() {
		tileWidthToUpdate = int.Parse(UIInput.current.value);
		tileGridManagerUpdates = true;
	}

	public void TileHeightChanged() {
		tileHeightToUpdate = int.Parse(UIInput.current.value);
		tileGridManagerUpdates = true;
	}

	public void RowPaddingChanged() {
		rowPaddingToUpdate = int.Parse(UIInput.current.value);
		tileGridManagerUpdates = true;
	}

	public void ColumnPaddingChanged() {
		columnPaddingToUpdate = int.Parse(UIInput.current.value);
		tileGridManagerUpdates = true;
	}

	public void OptionsChanged() {
		if (tileGridManagerUpdates) {
			tileGridManagerUpdates = false;
			tileGridManager.numRows = numRowsToUpdate;
			tileGridManager.numColumns = numColumnsToUpdate;
			tileGridManager.tileWidth = tileWidthToUpdate;
			tileGridManager.tileHeight = tileHeightToUpdate;
			tileGridManager.rowPadding = rowPaddingToUpdate;
			tileGridManager.columnPadding = columnPaddingToUpdate;

			gameManager.Restart();
		}
	}

	public void TogglePause() {
		gameManager.TogglePause();
		if (gameManager.IsPaused) {
			ShowModalBackground();
		} else {
			HideModalBackground();
		}
	}

	public void ShowModalBackground() {
		modalBackground.gameObject.SetActive(true);
	}

	public void HideModalBackground() {
		modalBackground.gameObject.SetActive(false);
	}
}