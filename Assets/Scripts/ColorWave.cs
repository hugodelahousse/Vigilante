using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class ColorWave : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<ColorCorrectionCurves>().saturation = Mathf.Sin(Time.time) + 1.5f;  
	}
}
