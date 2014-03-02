using UnityEngine;
using System.Collections;

public class Bucket : MonoBehaviour {
	CardinalDirection direction;

	Color color;

	SpriteRenderer spriteRenderer;
	GameObject spriteRendererGO;

	BoxCollider2D boxCollider2D;

	// TODO move these
	// How many tiles does it take to fill this thing?
	int currentFullness = 0;
	int maximumSize = 100;

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
			spriteRenderer.color = value;
			color = value;
		}
	}

	public SpriteRenderer SpriteRenderer {
		get {
			return SpriteRenderer;
		}
	}

	public GameObject SpriteRendererGO {
		get {
			return spriteRendererGO;
		}
	}

	public BoxCollider2D BoxCollider2D {
		get {
			return boxCollider2D;
		}
	}

	// Use this for initialization
	void Start() {
	
	}

	void Awake() {
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		spriteRendererGO = spriteRenderer.gameObject;

		boxCollider2D = ((BoxCollider2D) collider2D);
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == Globals.Tags.TILE) {
			Tile tile = collider.GetComponent<Tile>();
			if (!tile.TileGroup.IsScored) {
				if (tile.TileGroup.Color == color) {
					Debug.Log("Score!!!");

					currentFullness += tile.TileGroup.Tiles.Count;
					if (currentFullness > maximumSize) {
						currentFullness = maximumSize;
					}

					// Clean all this up, these calculations could be stored?
					Vector3 scale = spriteRendererGO.transform.localScale;
					switch (direction) {
					case CardinalDirection.Up:
						scale.x = ((float) currentFullness / maximumSize) * (Globals.WORLD_WIDTH * Globals.Instance.pixelsToUnits);
						break;

					case CardinalDirection.Down:
						scale.x = ((float) currentFullness / maximumSize) * (Globals.WORLD_WIDTH * Globals.Instance.pixelsToUnits);
						break;

					case CardinalDirection.Left:
						scale.y = ((float) currentFullness / maximumSize) * (Globals.WORLD_HEIGHT * Globals.Instance.pixelsToUnits);
						break;

					case CardinalDirection.Right:
						scale.y = ((float) currentFullness / maximumSize) * (Globals.WORLD_HEIGHT * Globals.Instance.pixelsToUnits);
						break;

					default:
						break;
					}

					spriteRendererGO.transform.localScale = scale;

					tile.TileGroup.Scored();
				} else {
					Debug.Log("Awwwww...no score...");

					tile.TileGroup.NoScored();
				}
			}
		}
	}
}