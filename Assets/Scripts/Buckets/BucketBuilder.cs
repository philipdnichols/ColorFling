using UnityEngine;
using System.Collections;

public class BucketBuilder : MonoBehaviour {
	public Bucket BuildBucket(GameObject bucketPrefab, 
	                          int bucketSize, int overhang, 
	                          CardinalDirection direction, Color color) {
		GameObject bucketGO = (GameObject) Instantiate(bucketPrefab);

		Bucket bucket = bucketGO.GetComponent<Bucket>();
		bucket.Direction = direction;
		bucket.Color = color;

		UpdateBucketScaleAndPosition(bucket, bucketSize, overhang);

		return bucket;
	}

	public void UpdateBucketScaleAndPosition(Bucket bucket, int bucketSize, int overhang) {
		Vector3 position = Vector3.zero;
		Vector2 colliderSize = Vector2.one;
		Vector3 spriteScale = bucket.SpriteRendererGO.transform.localScale;
		switch (bucket.Direction) {
		case CardinalDirection.Up:
			position.x = Globals.WORLD_WIDTH / 2.0f;
			position.y = Globals.WORLD_HEIGHT + ((bucketSize - overhang) / 2.0f / Globals.Instance.pixelsToUnits);

			colliderSize.x = Globals.WORLD_WIDTH;
			colliderSize.y = (float) bucketSize / Globals.Instance.pixelsToUnits;

			//spriteScale.x = Globals.WORLD_WIDTH * Globals.Instance.pixelsToUnits;
			spriteScale.y = bucketSize;
			break;
			
		case CardinalDirection.Down:
			position.x = Globals.WORLD_WIDTH / 2.0f;
			position.y = -((bucketSize - overhang) / 2.0f / Globals.Instance.pixelsToUnits);

			colliderSize.x = Globals.WORLD_WIDTH;
			colliderSize.y = (float) bucketSize / Globals.Instance.pixelsToUnits;

			//spriteScale.x = Globals.WORLD_WIDTH * Globals.Instance.pixelsToUnits;
			spriteScale.y = bucketSize;
			break;
			
		case CardinalDirection.Left:
			position.x = -((bucketSize - overhang) / 2.0f / Globals.Instance.pixelsToUnits);
			position.y = Globals.WORLD_HEIGHT / 2.0f;

			colliderSize.x = (float) bucketSize / Globals.Instance.pixelsToUnits;
			colliderSize.y = Globals.WORLD_HEIGHT;

			spriteScale.x = bucketSize;
			//spriteScale.y = Globals.WORLD_HEIGHT * Globals.Instance.pixelsToUnits;
			break;
			
		case CardinalDirection.Right:
			position.x = Globals.WORLD_WIDTH + ((bucketSize - overhang) / 2.0f / Globals.Instance.pixelsToUnits);
			position.y = Globals.WORLD_HEIGHT / 2.0f;

			colliderSize.x = (float) bucketSize / Globals.Instance.pixelsToUnits;
			colliderSize.y = Globals.WORLD_HEIGHT;

			spriteScale.x = bucketSize;
			//spriteScale.y = Globals.WORLD_HEIGHT * Globals.Instance.pixelsToUnits;
			break;
			
		default:
			break;
		}

		// TODO this can be cleaed up? Not really necessary, not done a lot.
		bucket.BoxCollider2D.size = colliderSize;
		bucket.SpriteRendererGO.transform.localScale = spriteScale;
		
		bucket.gameObject.transform.position = position;
	}
}