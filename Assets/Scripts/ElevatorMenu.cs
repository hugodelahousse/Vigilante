using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorMenu : MonoBehaviour {

	public Image floorNumber;

	public Sprite floor2Sprite;

	void Start()
	{
		floorNumber.enabled = false;
	}

	public void OnFloor2MouseEnter()
	{
		floorNumber.enabled = true;
		floorNumber.sprite = floor2Sprite;
	}

	public void OnFloor2MouseExit()
	{
		floorNumber.enabled = false;
	}

	public void GoToFloor2()
	{
		Application.LoadLevel("ActualSecondFloor");
		FindObjectOfType<GameController>().CloseElevatorMenu();
	}
}
