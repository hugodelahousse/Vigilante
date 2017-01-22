using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverFixup : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FindObjectOfType<GameController>().gameOverMenuObject = gameObject;
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
