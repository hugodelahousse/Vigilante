using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardItem : MonoBehaviour {

	public string ID = "PLEASE_PUT_ID_HERE";

	public Sprite[] sprites;
	public float frameTime = 0.2f;

	private int currentIndex = 0;
	private float currTime = 0.0f;

	void Update()
	{
		if (sprites != null && sprites.Length > 0)
		{
			currTime += Time.deltaTime;

			if (currTime >= frameTime)
			{
				currTime -= frameTime;
				currentIndex = (currentIndex + 1) % sprites.Length;
				GetComponent<SpriteRenderer>().sprite = sprites[currentIndex];
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player"))
		{
			// Got key!
			FindObjectOfType<GameController>().AddKey(ID);
			Destroy(gameObject);
		}
	}
}
