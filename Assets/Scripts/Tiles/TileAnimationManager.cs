using UnityEngine;
using System.Collections;

public class TileAnimationManager : MonoBehaviour {
	int numAnimations = 0;

	public bool AnimationsRunning {
		get {
			return numAnimations != 0;
		}
	}

	// Use this for initialization
	void Start() {

	}

	void Awake() {

	}
	
	// Update is called once per frame
	void Update() {
	
	}

	public void MoveTile(Tile tile, Vector3 targetPosition, float speed, iTween.EaseType easeType) {
		string tweenName = "MoveTile(" + tile.TilePosition.x + "," + tile.TilePosition.y + ")";
		float targetPositionX = targetPosition.x;
		float targetPositionY= targetPosition.y;
		float targetPositionZ = targetPosition.z;
		string onStart = "MoveTileStart";
		GameObject onStartTarget = gameObject;
		string onComplete = "MoveTileComplete";
		GameObject onCompleteTarget = gameObject;

		iTween.MoveTo(tile.gameObject, iTween.Hash("name", tweenName,
		                                           "x", targetPositionX, 
		                                           "y", targetPositionY, 
		                                           "z", targetPositionZ, 
		                                           "easeType", easeType, 
		                                           "speed", speed, 
		                                           "onstart", onStart,
		                                           "onstarttarget", onStartTarget,
		                                           "oncomplete", onComplete,
		                                           "oncompletetarget", onCompleteTarget));
	}

	void MoveTileStart() {
		numAnimations++;
	}

	void MoveTileComplete() {
		numAnimations--;
	}
}