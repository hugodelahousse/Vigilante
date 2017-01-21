using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	private HashSet<string> keysInInventory = new HashSet<string>();

	public void AddKey(string key)
	{
		Debug.Log("Added key: " + key);
		keysInInventory.Add(key);
	}

	public bool HasKey(string key)
	{
		return keysInInventory.Contains(key);
	}
}
