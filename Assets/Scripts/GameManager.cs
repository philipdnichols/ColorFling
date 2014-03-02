using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public TileGridManager tileGridManager;
	public BucketsManager bucketsManager;
	public ScoreManager scoreManager;

	bool isPaused = false;

	public bool IsPaused {
		get {
			return isPaused;
		}
	}

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	public void Restart() {
		tileGridManager.InitializeGrid();
	}

	public void TogglePause() {
		if (isPaused) {
			Time.timeScale = 1.0f;
			isPaused = false;
		} else {
			Time.timeScale = 0.0f;
			isPaused = true;
		}
	}

	public void Pause() {
		Time.timeScale = 0.0f;
		isPaused = true;
	}

	public void Unpause() {
		Time.timeScale = 1.0f;
		isPaused = false;
	}
}