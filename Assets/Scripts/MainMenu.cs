using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Sprite[] sprites;
	public float frameTime = 0.2f;

	private int currentIndex = 0;
	private float currTime = 0.0f;

	// Update is called once per frame
	void Update () {
		if (sprites != null && sprites.Length > 0)
		{
			currTime += Time.deltaTime;

			if (currTime >= frameTime)
			{
				currTime -= frameTime;
				currentIndex = (currentIndex + 1) % sprites.Length;
				GetComponent<RawImage>().texture = sprites[currentIndex].texture;
			}
		}
	}
}
