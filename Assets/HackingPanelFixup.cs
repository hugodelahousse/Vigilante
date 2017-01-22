using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingPanelFixup : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FindObjectOfType<GameController>().hackingPanel = gameObject;
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
