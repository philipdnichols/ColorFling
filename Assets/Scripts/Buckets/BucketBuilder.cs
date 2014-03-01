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
		Vector3 scale = Vector3.one;
		switch (bucket.Direction) {
		case CardinalDirection.Up:
			position.x = Globals.WORLD_WIDTH / 2.0f;
			position.y = Globals.WORLD_HEIGHT + ((bucketSize - overhang) / 2.0f / Globals.Instance.pixelsToUnits);

			scale.x = Globals.WORLD_WIDTH * Globals.Instance.pixelsToUnits;
			scale.y = bucketSize;
			break;
			
		case CardinalDirection.Down:
			position.x = Globals.WORLD_WIDTH / 2.0f;
			position.y = -((bucketSize - overhang) / 2.0f / Globals.Instance.pixelsToUnits);

			scale.x = Globals.WORLD_WIDTH * Globals.Instance.pixelsToUnits;
			scale.y = bucketSize;
			break;
			
		case CardinalDirection.Left:
			position.x = -((bucketSize - overhang) / 2.0f / Globals.Instance.pixelsToUnits);
			position.y = Globals.WORLD_HEIGHT / 2.0f;

			scale.x = bucketSize;
			scale.y = Globals.WORLD_HEIGHT * Globals.Instance.pixelsToUnits;
			break;
			
		case CardinalDirection.Right:
			position.x = Globals.WORLD_WIDTH + ((bucketSize - overhang) / 2.0f / Globals.Instance.pixelsToUnits);
			position.y = Globals.WORLD_HEIGHT / 2.0f;

			scale.x = bucketSize;
			scale.y = Globals.WORLD_HEIGHT * Globals.Instance.pixelsToUnits;
			break;
			
		default:
			break;
		}

		bucket.gameObject.transform.localScale = scale;
		
		bucket.gameObject.transform.position = position;
	}
}