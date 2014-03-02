using UnityEngine;
using System.Collections;

public class BucketTrigger : MonoBehaviour {
	BoxCollider2D boxCollider2D;

	Bucket bucket;

	public BoxCollider2D BoxCollider2D {
		get {
			return boxCollider2D;
		}
	}

	// Use this for initialization
	void Start() {
	
	}

	void Awake() {
		boxCollider2D = ((BoxCollider2D) collider2D);

		bucket = transform.parent.GetComponent<Bucket>();
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		bucket.HandleTrigger(collider);
	}
}