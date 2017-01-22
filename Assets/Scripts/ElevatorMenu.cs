using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorMenu : MonoBehaviour {

	public Image floorNumber;

	public Sprite floor2Sprite;
	public Sprite needsKey;
	public string floor2KeyName = "YELLOW_CARD";

	void Start()
	{
		floorNumber.enabled = false;
        FindObjectOfType<GameController>().elevatorMenu = gameObject;
        gameObject.SetActive(false);
	}

	public void OnFloor2MouseEnter()
	{
		floorNumber.enabled = true;

		if (FindObjectOfType<GameController>().HasKey(floor2KeyName))
		{
			floorNumber.sprite = floor2Sprite;
		}
		else
		{
			floorNumber.sprite = needsKey;
		}
	}

	public void OnFloor2MouseExit()
	{
		floorNumber.enabled = false;
	}

	public void GoToFloor2()
	{
		if (FindObjectOfType<GameController>().HasKey(floor2KeyName))
		{
			Application.LoadLevel("ActualSecondFloor");
			FindObjectOfType<GameController>().CloseElevatorMenu();
		}
	}
}
