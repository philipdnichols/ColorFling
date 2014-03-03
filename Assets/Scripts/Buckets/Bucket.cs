using UnityEngine;
using System.Collections;

public class Bucket : MonoBehaviour {
	CardinalDirection direction;

	Color color;

	BucketGFX bucketGFX;
	BucketFadeGFX bucketFadeGFX;
	BucketTrigger bucketTrigger;

	BoxCollider2D boxCollider2D;

	int currentFullness = 0;
	int maximumSize;

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
			bucketGFX.Color = value;
			bucketFadeGFX.Color = Color.Lerp(value, Color.white, 0.5f);

			color = value;
		}
	}

	public BucketGFX BucketGFX {
		get {
			return bucketGFX;
		}
	}

	public BucketFadeGFX BucketFadeGFX {
		get {
			return bucketFadeGFX;
		}
	}

	public BucketTrigger BucketTrigger {
		get {
			return bucketTrigger;
		}
	}

	public BoxCollider2D BoxCollider2D {
		get {
			return boxCollider2D;
		}
	}

	public int CurrentFullness {
		get {
			return currentFullness;
		}
	}

	public int MaximumSize {
		get {
			return maximumSize;
		}

		set {
			maximumSize = value;
		}
	}

	// Use this for initialization
	void Start() {
	
	}

	void Awake() {
		bucketGFX = GetComponentInChildren<BucketGFX>();
		bucketFadeGFX = GetComponentInChildren<BucketFadeGFX>();
		bucketTrigger = GetComponentInChildren<BucketTrigger>();

		boxCollider2D = ((BoxCollider2D) collider2D);
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	public void SetLayer(string layer) {
		int layerInt = LayerMask.NameToLayer(layer);

		gameObject.layer = layerInt;
	}

	public void HandleTrigger(Collider2D collider) {
		if (collider.tag == Globals.Tags.TILE) {
			Tile tile = collider.GetComponent<Tile>();
			if (!tile.TileGroup.IsScored) {
				if (tile.TileGroup.Color == color) {
					currentFullness += tile.TileGroup.Tiles.Count;
					if (currentFullness > maximumSize) {
						currentFullness = maximumSize;
					}
					
					// Clean all this up, these calculations could be stored?
					Vector3 scale = bucketGFX.gameObject.transform.localScale;
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
					
					bucketGFX.gameObject.transform.localScale = scale;
					
					tile.TileGroup.Scored();

					if (currentFullness == maximumSize) {
						// TODO this is a dirty, dirty hack and I feel bad for doing it. What is a better way to be handling this?
						transform.parent.GetComponent<BucketsManager>().HandleFullBucket(this);
					}
				}
			}
		}
	}
}