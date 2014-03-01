using UnityEngine;
using System.Collections;

public class ColorManager : MonoBehaviour {
	public Color[] tileColors;

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	public Color RandomColor() {
		return tileColors[Random.Range(0, tileColors.Length)];
	}
}