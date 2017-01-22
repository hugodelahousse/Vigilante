using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

	//private bool playerInBox = false;

	/*
	void LateUpdate()
	{
		//Debug.Log("GH");
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameController controller = FindObjectOfType<GameController>();
		if (controller.inElevatorMenu)
		{
			if (Input.GetKeyUp(KeyCode.Escape))
			{
				controller.CloseElevatorMenu();
				playerInBox = false;
			}
		}
		else
		{
			//Debug.Log("GGH");
			if (playerInBox)
			{
				Debug.Log("GGGH");
				controller.OpenElevatorMenu();
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player"))
		{
			playerInBox = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.CompareTag("Player"))
		{
			playerInBox = false;
		}
	}*/
}
