using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BucketBuilder))]
public class BucketsManager : MonoBehaviour {
	public GameObject bucketPrefab;

	public ColorManager colorManager;
	public TileGroupManager tileGroupManager;

	public int bucketSize = 100;
	public int overhang = 5;

	public int sizeMin = 10;
	public int sizeMax = 20;

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
		BuildBucket(colorUp, layerUp, CardinalDirection.Up);

		Color colorDown = uniqueColors[1];
		string layerDown = Globals.Layers.BUCKET2;
		BuildBucket(colorDown, layerDown, CardinalDirection.Down);

		Color colorLeft = uniqueColors[2];
		string layerLeft = Globals.Layers.BUCKET3;
		BuildBucket(colorLeft, layerLeft, CardinalDirection.Left);

		Color colorRight = uniqueColors[3];
		string layerRight = Globals.Layers.BUCKET4;
		BuildBucket(colorRight, layerRight, CardinalDirection.Right);
	}

	void BuildBucket(Color color, string layer, CardinalDirection direction) {
		Bucket bucket = bucketBuilder.BuildBucket(bucketPrefab, 
		                                          bucketSize, overhang, 
		                                          direction, color);
		bucket.transform.parent = transform;
		bucket.SetLayer(layer);
		bucket.MaximumSize = Random.Range(sizeMin, sizeMax);

		colorToLayer.Add(color, layer);
		buckets.Add(direction, bucket);
	}

	public void HandleFullBucket(Bucket bucket) {
		string layer = colorToLayer[bucket.Color];
		colorToLayer.Remove(bucket.Color);

		buckets.Remove(bucket.Direction);

		Destroy(bucket.gameObject);

		HashSet<Color> usedColors = new HashSet<Color>(colorToLayer.Keys);

		Color[] colors = colorManager.UniqueColors(1, usedColors);
		BuildBucket(colors[0], layer, bucket.Direction);

		tileGroupManager.UpdateTileGroupLayers();
	}
}