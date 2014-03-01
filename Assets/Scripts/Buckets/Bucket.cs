using UnityEngine;
using System.Collections;

public class Bucket : MonoBehaviour {
	CardinalDirection direction;

	Color color;

	public CardinalDirection Direction {
		get {
			return direction;
		}

		set {
			direction = value;
		}
	}

	public Color Color {
		get {
			return color;
		}

		set {
			((SpriteRenderer) renderer).color = value;
			color = value;
		}
	}

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == Globals.Tags.TILE) {
			Tile tile = collider.GetComponent<Tile>();
			if (tile.Color == color) {
				Debug.Log("Score!!!");
			} else {
				Debug.Log("Awwwww...no score...");
			}
		}
	}
}