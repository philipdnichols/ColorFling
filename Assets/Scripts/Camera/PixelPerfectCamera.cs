using UnityEngine;
using System.Collections;

public class PixelPerfectCamera : MonoBehaviour {
	// Use this for initialization
	void Start() {

	}
	
	// Update is called once per frame
	void Update() {
		// Make the camera pixel perfect:
		camera.orthographicSize = Screen.height / 2.0f / Globals.Instance.pixelsToUnits;

		// Put the bottom left corner at (0,0):
		Vector3 position = new Vector3(0.0f, 0.0f, transform.position.z);
		position.x += Globals.WORLD_WIDTH / 2.0f;
		position.y += Globals.WORLD_HEIGHT / 2.0f;
		transform.position = position;
	}
}