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
		Vector3 bucketGFXScale = bucket.BucketGFX.gameObject.transform.localScale;
		Vector3 bucketFadeGFXScale = bucket.BucketFadeGFX.gameObject.transform.localScale;
		Vector3 triggerSize = Vector2.one;
		Vector3 triggerCenter = Vector2.zero;
		switch (bucket.Direction) {
		case CardinalDirection.Up:
			position.x = Globals.WORLD_WIDTH / 2.0f;
			position.y = Globals.WORLD_HEIGHT + ((bucketSize - overhang) / 2.0f / Globals.Instance.pixelsToUnits);

			colliderSize.x = Globals.WORLD_WIDTH;
			colliderSize.y = (float) bucketSize / Globals.Instance.pixelsToUnits;

			triggerSize.x = Globals.WORLD_WIDTH;
			triggerSize.y = (float) (bucketSize - (overhang / 2.0f)) / Globals.Instance.pixelsToUnits;
			triggerCenter.y = (float) (overhang / 4.0f) / Globals.Instance.pixelsToUnits;

			bucketGFXScale.y = bucketSize;

			bucketFadeGFXScale.x = Globals.WORLD_WIDTH * Globals.Instance.pixelsToUnits;
			bucketFadeGFXScale.y = bucketSize;
			break;
			
		case CardinalDirection.Down:
			position.x = Globals.WORLD_WIDTH / 2.0f;
			position.y = -((bucketSize - overhang) / 2.0f / Globals.Instance.pixelsToUnits);

			colliderSize.x = Globals.WORLD_WIDTH;
			colliderSize.y = (float) bucketSize / Globals.Instance.pixelsToUnits;

			triggerSize.x = Globals.WORLD_WIDTH;
			triggerSize.y = (float) (bucketSize - (overhang / 2.0f)) / Globals.Instance.pixelsToUnits;
			triggerCenter.y = (float) -(overhang / 4.0f) / Globals.Instance.pixelsToUnits;

			bucketGFXScale.y = bucketSize;

			bucketFadeGFXScale.x = Globals.WORLD_WIDTH * Globals.Instance.pixelsToUnits;
			bucketFadeGFXScale.y = bucketSize;
			break;
			
		case CardinalDirection.Left:
			position.x = -((bucketSize - overhang) / 2.0f / Globals.Instance.pixelsToUnits);
			position.y = Globals.WORLD_HEIGHT / 2.0f;

			colliderSize.x = (float) bucketSize / Globals.Instance.pixelsToUnits;
			colliderSize.y = Globals.WORLD_HEIGHT;

			triggerSize.x = (float) (bucketSize - (overhang / 2.0f)) / Globals.Instance.pixelsToUnits;
			triggerSize.y = Globals.WORLD_HEIGHT;
			triggerCenter.x = (float) -(overhang / 4.0f) / Globals.Instance.pixelsToUnits;

			bucketGFXScale.x = bucketSize;

			bucketFadeGFXScale.x = bucketSize;
			bucketFadeGFXScale.y = Globals.WORLD_HEIGHT * Globals.Instance.pixelsToUnits;
			break;
			
		case CardinalDirection.Right:
			position.x = Globals.WORLD_WIDTH + ((bucketSize - overhang) / 2.0f / Globals.Instance.pixelsToUnits);
			position.y = Globals.WORLD_HEIGHT / 2.0f;

			colliderSize.x = (float) bucketSize / Globals.Instance.pixelsToUnits;
			colliderSize.y = Globals.WORLD_HEIGHT;

			triggerSize.x = (float) (bucketSize - (overhang / 2.0f)) / Globals.Instance.pixelsToUnits;
			triggerSize.y = Globals.WORLD_HEIGHT;
			triggerCenter.x = (float) (overhang / 4.0f) / Globals.Instance.pixelsToUnits;

			bucketGFXScale.x = bucketSize;

			bucketFadeGFXScale.x = bucketSize;
			bucketFadeGFXScale.y = Globals.WORLD_HEIGHT * Globals.Instance.pixelsToUnits;
			break;
			
		default:
			break;
		}

		// TODO this can be cleaed up? Not really necessary, not done a lot.
		bucket.BoxCollider2D.size = colliderSize;
		bucket.BucketGFX.gameObject.transform.localScale = bucketGFXScale;
		bucket.BucketFadeGFX.gameObject.transform.localScale = bucketFadeGFXScale;
		bucket.BucketTrigger.BoxCollider2D.size = triggerSize;
		bucket.BucketTrigger.BoxCollider2D.center = triggerCenter;
		
		bucket.gameObject.transform.position = position;
	}
}