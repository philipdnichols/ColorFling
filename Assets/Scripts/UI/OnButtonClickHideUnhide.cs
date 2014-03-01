using UnityEngine;
using System.Collections;

public class OnButtonClickHideUnhide : MonoBehaviour {
	public GameObject target;
	public bool hide = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick() {
		target.SetActive(!hide);
	}
}