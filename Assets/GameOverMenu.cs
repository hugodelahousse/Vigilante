using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().gameOverMenuObject = this.gameObject;
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
