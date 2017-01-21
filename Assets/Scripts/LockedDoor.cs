using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{

	public string ID = "PLEASE_PUT_ID_HERE";

	public bool isLocked = true;

	public float doorCloseDelay = 2.0f;
	public float doorOpenTime = 1.0f;

	public GameObject doorLock;

	private bool isOpening = false;
	private bool isOpen = false;
	private bool isClosing = false;

	private GameObject player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		if (isOpen && !isClosing && !GetComponent<BoxCollider>().bounds.Contains(player.transform.position))
		{
			Debug.Log("Closing door");
			StartCoroutine("CloseDoor");
		}

		if (!isLocked && doorLock.activeInHierarchy)
		{
			doorLock.SetActive(false);
		}
	}

	IEnumerator OpenDoor()
	{
		isOpening = true;
		float time = doorOpenTime * (1 - transform.GetChild(0).localScale.z);
		float duration = doorOpenTime;
		while (time < duration)
		{
			Vector3 scale = transform.GetChild(0).localScale;
			scale.z = 1 - (time / duration);
			transform.GetChild(0).localScale = scale;
			time += Time.deltaTime;
			yield return null;
		}

		isOpen = true;
		isOpening = false;
	}

	IEnumerator CloseDoor()
	{
		isClosing = true;
		{
			float delayTime = 0.0f;
			while (delayTime < doorCloseDelay)
			{
				delayTime += Time.deltaTime;
				yield return null;
			}
		}

		float time = doorOpenTime * transform.GetChild(0).localScale.z;
		float duration = doorOpenTime;
		while (time < duration)
		{
			Vector3 scale = transform.GetChild(0).localScale;
			scale.z = time / duration;
			transform.GetChild(0).localScale = scale;
			time += Time.deltaTime;
			yield return null;
		}

		isClosing = false;
		isOpen = false;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player"))
		{
			if (isLocked)
			{
				if (col.GetComponent<PlayerInventory>().HasKey(ID))
				{
					// Unlocked door!
					isLocked = false;
				}
			}

			if (!isLocked && !isOpening && !isOpen)
			{
				if (isClosing)
				{
					StopCoroutine("CloseDoor");
				}

				StartCoroutine("OpenDoor");
			}
			else if (isLocked)
			{
				// Tried to open locked door!
			}
		}
	}
}
