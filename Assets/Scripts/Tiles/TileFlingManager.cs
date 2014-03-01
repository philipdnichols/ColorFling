using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileGridManager))]
[RequireComponent(typeof(TileGroupManager))]
public class TileFlingManager : MonoBehaviour {
	SpringJoint2D springJoint;

	// Required Components
	TileGridManager tileGridManager;
	TileGroupManager tileGroupManager;

	// Use this for initialization
	void Start() {

	}

	void Awake() {
		tileGridManager = GetComponent<TileGridManager>();
		tileGroupManager = GetComponent<TileGroupManager>();
	}
	
	// Update is called once per frame
	void Update() {
		// TODO something something this should be encapsulated more
		if (UICamera.hoveredObject == null) {
			if (tileGridManager.AllowFlings) {
				if (Input.GetMouseButtonDown(0)) {
					RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 
					                                     Mathf.Infinity);
					if (hit.collider != null) {
						if (springJoint == null) {
							GameObject go = new GameObject("Rigidbody2D Dragger");
							Rigidbody2D body = go.AddComponent<Rigidbody2D>();
							springJoint = go.AddComponent<SpringJoint2D>();
							body.isKinematic = true;
						}

						// TODO move this somewhere else?
						TileGroup tileGroup = hit.transform.GetComponent<TileGroup>();
						tileGroup.MovePositionIndependentOfChildren(hit.point);
						tileGroup.transform.parent = null;

						tileGroupManager.ClearAllTileGroupTiles(tileGroup);
						
						springJoint.transform.position = hit.point;
						springJoint.distance = 0.2f;
						springJoint.dampingRatio = 0.5f;
						springJoint.connectedBody = hit.rigidbody;
						
						hit.rigidbody.isKinematic = false;
						
						BoxCollider2D[] childCollider2Ds = hit.transform.GetComponentsInChildren<BoxCollider2D>();
						foreach (BoxCollider2D childCollider2D in childCollider2Ds) {
							childCollider2D.isTrigger = true;
						}
						
						SpriteRenderer[] childSpriteRenderers = hit.transform.GetComponentsInChildren<SpriteRenderer>();
						foreach (SpriteRenderer childSpriteRenderer in childSpriteRenderers) {
							childSpriteRenderer.sortingOrder = 1;
						}
						
						StartCoroutine("DragObject");

						// TODO when do we trigger this?
						tileGridManager.UpdateTileGridManager();
					}
				}
			}
		}
	}

	IEnumerator DragObject() {
		float oldDrag = springJoint.connectedBody.drag;
		float oldAngularDrag = springJoint.connectedBody.angularDrag;
		springJoint.connectedBody.drag = 1.0f;
		springJoint.connectedBody.angularDrag = 5.0f;
		
		while (Input.GetMouseButton(0)) {
			springJoint.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			yield return null;
		}
		
		if (springJoint.connectedBody != null) {
			springJoint.connectedBody.drag = oldDrag;
			springJoint.connectedBody.angularDrag = oldAngularDrag;
			springJoint.connectedBody = null;
		}
	}
}