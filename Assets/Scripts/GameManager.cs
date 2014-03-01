using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public TileGridManager tileGridManager;

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	public void Restart() {
		tileGridManager.InitializeGrid();
	}
}