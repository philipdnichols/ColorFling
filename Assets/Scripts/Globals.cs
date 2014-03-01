using UnityEngine;
using System.Collections;

public class Globals : Singleton<Globals> {
	protected Globals() {}

	public static Vector3 WORLD_TOP_LEFT_POINT;
	public static Vector3 WORLD_TOP_RIGHT_POINT;
	public static Vector3 WORLD_BOTTOM_LEFT_POINT;
	public static Vector3 WORLD_BOTTOM_RIGHT_POINT;

	public static float WORLD_WIDTH;
	public static float WORLD_HEIGHT;

	public int pixelsToUnits = 100;

	Camera mainCamera;

	void Start() {
		mainCamera = Camera.main;

		UpdateWorldVariables();
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

	}
}