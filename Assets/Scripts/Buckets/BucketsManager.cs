using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BucketBuilder))]
public class BucketsManager : MonoBehaviour {
	public GameObject bucketPrefab;

	public ColorManager colorManager;

	public int bucketSize = 100;
	public int overhang = 5;

	Dictionary<CardinalDirection, Bucket> buckets = new Dictionary<CardinalDirection, Bucket>();

	// Required Components
	BucketBuilder bucketBuilder;

	// Use this for initialization
	void Start() {
		InitializeBuckets();
	}

	void Awake() {
		bucketBuilder = GetComponent<BucketBuilder>();
	}
	
	// Update is called once per frame
	void Update() {
		// Make sure all the buckets are always in the correct places based on the screen
		UpdateBucketsForScreen();
	}

	void UpdateBucketsForScreen() {
		foreach (Bucket bucket in buckets.Values) {
			bucketBuilder.UpdateBucketScaleAndPosition(bucket, bucketSize, overhang);
		}
	}

	public void InitializeBuckets() {
		Color[] uniqueColors = colorManager.UniqueColors(4);

		Bucket bucketUp = bucketBuilder.BuildBucket(bucketPrefab, 
		                                            bucketSize, overhang, 
		                                            CardinalDirection.Up, uniqueColors[0]);
		bucketUp.transform.parent = transform;
		buckets.Add(CardinalDirection.Up, bucketUp);

		Bucket bucketDown = bucketBuilder.BuildBucket(bucketPrefab, 
		                                              bucketSize, overhang, 
		                                              CardinalDirection.Down, uniqueColors[1]);
		bucketDown.transform.parent = transform;
		buckets.Add(CardinalDirection.Down, bucketDown);

		Bucket bucketLeft = bucketBuilder.BuildBucket(bucketPrefab, 
		                                              bucketSize, overhang, 
		                                              CardinalDirection.Left, uniqueColors[2]);
		bucketLeft.transform.parent = transform;
		buckets.Add(CardinalDirection.Left, bucketLeft);

		Bucket bucketRight = bucketBuilder.BuildBucket(bucketPrefab, 
		                                               bucketSize, overhang, 
		                                               CardinalDirection.Right, uniqueColors[3]);
		bucketRight.transform.parent = transform;
		buckets.Add(CardinalDirection.Right, bucketRight);
	}
}