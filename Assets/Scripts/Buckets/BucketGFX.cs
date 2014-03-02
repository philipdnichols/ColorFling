using UnityEngine;
using System.Collections;

public class BucketGFX : MonoBehaviour {
	Color color;
	
	SpriteRenderer spriteRenderer;

	public Color Color {
		get {
			return color;
		}
		
		set {
			spriteRenderer.color = value;
			color = value;
		}
	}

	// Use this for initialization
	void Start() {
	
	}

	void Awake() {
		spriteRenderer = (SpriteRenderer) renderer;
	}
	
	// Update is called once per frame
	void Update() {
	
	}
}