using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public Color[] UniqueColors(int amount) {
		if (amount > tileColors.Length) {
			amount = tileColors.Length;
			Debug.LogWarning("Can't get " + amount + 
			                 " unique colors when there are only " + tileColors.Length + 
			                 " colors to choose from!");
		}

		Color[] uniqueColors = new Color[amount];

		HashSet<Color> uniqueColorsSet = new HashSet<Color>();
		while (uniqueColorsSet.Count != amount) {
			uniqueColorsSet.Add(RandomColor());
		}
		uniqueColorsSet.CopyTo(uniqueColors);

		return uniqueColors;
	}

	public Color[] UniqueColors(int amount, HashSet<Color> excludeColors) {
		if (amount > tileColors.Length) {
			amount = tileColors.Length;
			Debug.LogWarning("Can't get " + amount + 
			                 " unique colors when there are only " + tileColors.Length + 
			                 " colors to choose from!");
		}
		
		Color[] uniqueColors = new Color[amount];
		
		HashSet<Color> uniqueColorsSet = new HashSet<Color>();
		while (uniqueColorsSet.Count != amount) {
			Color randomColor = RandomColor();
			if (!excludeColors.Contains(randomColor)) {
				uniqueColorsSet.Add(randomColor);
			}
		}

		uniqueColorsSet.CopyTo(uniqueColors);
		
		return uniqueColors;
	}
}