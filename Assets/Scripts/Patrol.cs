using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour {

    // Use this for initialization
    public Transform[] path;
    NavMeshAgent agent;
    int currentDest;

	public float chaseTimeout = 10.0f;
	public float stoppingDistance = 1.0f;

	float timeSinceSeenPlayer = 0.0f;
	bool chasingPlayer = false;

	void Start () {
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        GoToNext();
	}

    void GoToNext() {
        if (path.Length == 0)
            return;
        currentDest = (currentDest + 1) % path.Length;
        agent.destination = path[currentDest].position;

    }

	// Update is called once per frame
	void Update()
	{
		if (chasingPlayer)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			agent.destination = player.transform.position;
			agent.stoppingDistance = stoppingDistance;
			if (GetComponentInChildren<RobotCollider>().LoSToPlayer())
			{
				timeSinceSeenPlayer = 0.0f;
			}
			else
			{
				timeSinceSeenPlayer += Time.deltaTime;
			}

			if (timeSinceSeenPlayer >= chaseTimeout)
			{
				agent.destination = path[currentDest].position;
				chasingPlayer = false;
			}
		}
		else {
			if (agent.remainingDistance < 0.5f)
				GoToNext();
		}
	}

	void OnCameraAlert()
	{
		chasingPlayer = true;
	}

	void OnNoticePlayer()
	{
		// TODO
		Debug.Log("Robot saw player!");
		chasingPlayer = true;
	}
}
