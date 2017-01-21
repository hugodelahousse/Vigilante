using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour {

    // Use this for initialization
    public Transform[] path;
    NavMeshAgent agent;
    int currentDest;

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
	void Update () {
        if (agent.remainingDistance < 0.5f)
            GoToNext();
	}
}
