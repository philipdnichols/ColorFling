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

	Dictionary<Color, string> colorToLayer = new Dictionary<Color, string>();

	// Required Components
	BucketBuilder bucketBuilder;

	public Dictionary<Color, string> ColorToLayer {
		get {
			return colorToLayer;
		}
	}

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

		Color colorUp = uniqueColors[0];
		string layerUp = Globals.Layers.BUCKET1;
		Bucket bucketUp = bucketBuilder.BuildBucket(bucketPrefab, 
		                                            bucketSize, overhang, 
		                                            CardinalDirection.Up, colorUp);
		bucketUp.transform.parent = transform;
		bucketUp.gameObject.layer = LayerMask.NameToLayer(layerUp);
		colorToLayer.Add(colorUp, layerUp);
		buckets.Add(CardinalDirection.Up, bucketUp);

		Color colorDown = uniqueColors[1];
		string layerDown = Globals.Layers.BUCKET2;
		Bucket bucketDown = bucketBuilder.BuildBucket(bucketPrefab, 
		                                              bucketSize, overhang, 
		                                              CardinalDirection.Down, colorDown);
		bucketDown.transform.parent = transform;
		bucketDown.gameObject.layer = LayerMask.NameToLayer(layerDown);
		colorToLayer.Add(colorDown, layerDown);
		buckets.Add(CardinalDirection.Down, bucketDown);

		Color colorLeft = uniqueColors[2];
		string layerLeft = Globals.Layers.BUCKET3;
		Bucket bucketLeft = bucketBuilder.BuildBucket(bucketPrefab, 
		                                              bucketSize, overhang, 
		                                              CardinalDirection.Left, colorLeft);
		bucketLeft.transform.parent = transform;
		bucketLeft.gameObject.layer = LayerMask.NameToLayer(layerLeft);
		colorToLayer.Add(colorLeft, layerLeft);
		buckets.Add(CardinalDirection.Left, bucketLeft);

		Color colorRight = uniqueColors[3];
		string layerRight = Globals.Layers.BUCKET4;
		Bucket bucketRight = bucketBuilder.BuildBucket(bucketPrefab, 
		                                               bucketSize, overhang, 
		                                               CardinalDirection.Right, colorRight);
		bucketRight.transform.parent = transform;
		bucketRight.gameObject.layer = LayerMask.NameToLayer(layerRight);
		colorToLayer.Add(colorRight, layerRight);
		buckets.Add(CardinalDirection.Right, bucketRight);
	}
}