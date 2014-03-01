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
		Bucket bucketUp = bucketBuilder.BuildBucket(bucketPrefab, 
		                                            bucketSize, overhang, 
		                                            CardinalDirection.Up, colorManager.RandomColor());
		buckets.Add(CardinalDirection.Up, bucketUp);

		Bucket bucketDown = bucketBuilder.BuildBucket(bucketPrefab, 
		                                              bucketSize, overhang, 
		                                              CardinalDirection.Down, colorManager.RandomColor());
		buckets.Add(CardinalDirection.Down, bucketDown);

		Bucket bucketLeft = bucketBuilder.BuildBucket(bucketPrefab, 
		                                              bucketSize, overhang, 
		                                              CardinalDirection.Left, colorManager.RandomColor());
		buckets.Add(CardinalDirection.Left, bucketLeft);

		Bucket bucketRight = bucketBuilder.BuildBucket(bucketPrefab, 
		                                               bucketSize, overhang, 
		                                               CardinalDirection.Right, colorManager.RandomColor());
		buckets.Add(CardinalDirection.Right, bucketRight);
	}
}