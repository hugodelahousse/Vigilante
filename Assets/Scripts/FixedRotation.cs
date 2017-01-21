using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour {
    Transform cam;

    // Use this for initialization
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").transform;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        transform.rotation.Set(transform.rotation.x, cam.rotation.y, transform.rotation.z, transform.rotation.w);
	}
}
