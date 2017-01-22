using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActions : MonoBehaviour {

	public string firstLevelName = "FirstFloor";

	public void RestartLevel()
	{
		Time.timeScale = 1.0f;
		Application.LoadLevel(Application.loadedLevel);
	}

	public void StartGame()
	{
		Time.timeScale = 1.0f;
		Application.LoadLevel(firstLevelName);
	}

	public void EndGame()
	{
		Application.Quit();
	}

	public void LoadMenu()
	{
		Application.LoadLevel(0);
	}
}
