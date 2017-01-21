using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardItem : MonoBehaviour {

	public string ID = "PLEASE_PUT_ID_HERE";

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player"))
		{
			// Got key!
			col.GetComponent<PlayerInventory>().AddKey(ID);
			Destroy(gameObject);
		}
	}
}
