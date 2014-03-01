using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	Color color;

	TileGroup tileGroup;

	Vector2 tilePosition;

	public Color Color {
		get {
			return color;
		}

		set {
			((SpriteRenderer) renderer).color = value;
			color = value;
		}
	}

	public TileGroup TileGroup {
		get {
			return tileGroup;
		}

		set {
			tileGroup = value;
		}
	}

	public Vector2 TilePosition {
		get {
			return tilePosition;
		}

		set {
			tilePosition = value;
		}
	}

	// Use this for initialization
	void Start() {

	}

	void Awake() {
		iTween.Init(gameObject);
	}
	
	// Update is called once per frame
	void Update() {
	
	}
}