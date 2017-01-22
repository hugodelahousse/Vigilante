using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSplashReduce : MonoBehaviour {

	private float scale = 2.2f;

	Vector3 origScale;

	void Start()
	{
		origScale = transform.localScale;
	}

	// Update is called once per frame
	void Update () {
		if (scale > 0.0f)
		{
			scale -= Time.deltaTime * 0.5f;
			if (scale < 1.0f)
			{
				transform.localScale = origScale * scale;
			}
		}
	}
}
