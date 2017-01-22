using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveilanceCam : MonoBehaviour {

    public float maxRotation;
    public float minRotation;
    public float rotationSpeed;
    public GameObject coneVision;

	// Update is called once per frame
	void Update () {
        Vector3 temp = transform.rotation.eulerAngles;
        temp.y = minRotation + (Mathf.Sin(Time.time * rotationSpeed) + 1)/2 * maxRotation;
        transform.rotation = Quaternion.Euler(temp);
    }

	void OnNoticePlayer()
	{
		// TODO
		Debug.Log("Camera saw player!");

		Patrol[] patrols = FindObjectsOfType<Patrol>();

		Patrol closestPatrol = null;
		float closestDistSqr = Mathf.Infinity;

		foreach (Patrol patrol in patrols)
		{
			float distSqr = (patrol.transform.position - transform.position).sqrMagnitude;
			if (distSqr < closestDistSqr)
			{
				closestDistSqr = distSqr;
				closestPatrol = patrol;
			}
		}

		if (closestPatrol)
		{
			closestPatrol.SendMessage("OnCameraAlert");
		}
	}
}
