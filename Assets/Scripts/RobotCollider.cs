using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCollider : MonoBehaviour {

	public LayerMask checkSightMask;

	bool playerInVision = false;

	void Update()
	{
		if (playerInVision && LoSToPlayer())
		{
			transform.parent.SendMessage("OnNoticePlayer");
		}
	}

	public bool LoSToPlayer()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Vector3 target = player.transform.position + Vector3.up * (player.GetComponent<CharacterController>().height / 2);

		RaycastHit info;
		if (Physics.Linecast(transform.position, target, out info, checkSightMask, QueryTriggerInteraction.Ignore))
		{
			if (info.collider.gameObject == player)
			{
				return true;
			}
		}
		else
		{
			return true;
		}

		return false;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player"))
		{
			//Debug.Log("Vision enter");
			playerInVision = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.CompareTag("Player"))
		{
			//Debug.Log("Vision exit");
			playerInVision = false;
		}
	}
}
