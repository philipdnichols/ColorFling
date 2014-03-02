using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Globals : Singleton<Globals> {
	protected Globals() {}

	public static Vector3 WORLD_TOP_LEFT_POINT;
	public static Vector3 WORLD_TOP_RIGHT_POINT;
	public static Vector3 WORLD_BOTTOM_LEFT_POINT;
	public static Vector3 WORLD_BOTTOM_RIGHT_POINT;

	public static float WORLD_WIDTH;
	public static float WORLD_HEIGHT;

	public int pixelsToUnits = 100;

	Dictionary<string, string> bucketLayersToTileLayers = new Dictionary<string, string>();

	Camera mainCamera;

	public Dictionary<string, string> BucketLayersToTileLayers {
		get {
			return bucketLayersToTileLayers;
		}
	}

	void Start() {
		mainCamera = Camera.main;

		UpdateWorldVariables();

		bucketLayersToTileLayers.Add(Layers.BUCKET1, Layers.TILE1);
		bucketLayersToTileLayers.Add(Layers.BUCKET2, Layers.TILE2);
		bucketLayersToTileLayers.Add(Layers.BUCKET3, Layers.TILE3);
		bucketLayersToTileLayers.Add(Layers.BUCKET4, Layers.TILE4);
	}
	
	// (optional) allow runtime registration of global objects
	static public T RegisterComponent<T>() where T : Component {
		return Instance.GetOrAddComponent<T>();
	}

	// Update is called once per frame
	void Update() {
		UpdateWorldVariables();

		Debug.DrawLine(WORLD_TOP_LEFT_POINT, WORLD_TOP_RIGHT_POINT);
		Debug.DrawLine(WORLD_TOP_LEFT_POINT, WORLD_BOTTOM_LEFT_POINT);
		Debug.DrawLine(WORLD_BOTTOM_LEFT_POINT, WORLD_BOTTOM_RIGHT_POINT);
		Debug.DrawLine(WORLD_TOP_RIGHT_POINT, WORLD_BOTTOM_RIGHT_POINT);
	}

	void UpdateWorldVariables() {
		WORLD_TOP_LEFT_POINT = mainCamera.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 0.0f));
		WORLD_TOP_RIGHT_POINT = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
		WORLD_BOTTOM_LEFT_POINT = mainCamera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
		WORLD_BOTTOM_RIGHT_POINT = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
		
		WORLD_WIDTH = Mathf.Abs(WORLD_TOP_RIGHT_POINT.x - WORLD_TOP_LEFT_POINT.x);
		WORLD_HEIGHT = Mathf.Abs(WORLD_BOTTOM_LEFT_POINT.y - WORLD_TOP_LEFT_POINT.y);
	}

	// TODO
	public static class Tags {
		public static string TILE =			"Tile";
		public static string TILE_GROUP =	"TileGroup";
		public static string BUCKET = 		"Bucket";
	}

	public static class SortingLayers {
		
	}

	public static class Layers {
		public static string BUCKET1 = 		"Bucket1";
		public static string BUCKET2 = 		"Bucket2";
		public static string BUCKET3 = 		"Bucket3";
		public static string BUCKET4 = 		"Bucket4";

		public static string TILE1 = 		"Tile1";
		public static string TILE2 = 		"Tile2";
		public static string TILE3 = 		"Tile3";
		public static string TILE4 = 		"Tile4";
		public static string TILE_NULL = 	"TileNull";
	}
}