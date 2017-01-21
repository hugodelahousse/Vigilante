using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCollider : MonoBehaviour {

	public LayerMask checkSightMask;

	bool playerInVision = false;

	void Update()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (playerInVision)
		{
			Vector3 target = player.transform.position + Vector3.up * (player.GetComponent<CharacterController>().height / 2);

			RaycastHit info;
			if (Physics.Linecast(transform.position, target, out info, checkSightMask, QueryTriggerInteraction.Ignore))
			{
				if (info.collider.gameObject == player)
				{
					Debug.Log("Robot saw player!");
				}
			}
			else
			{
				Debug.Log("Robot saw player!");
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player"))
		{
			Debug.Log("Vision enter");
			playerInVision = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.CompareTag("Player"))
		{
			Debug.Log("Vision exit");
			playerInVision = false;
		}
	}
}
