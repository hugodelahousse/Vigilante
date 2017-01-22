using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour {
    Transform cam;

    // Use this for initialization
    void Start()
    {
        
    }
	
	// Update is called once per frame
	void LateUpdate () {


        //transform.rotation = Quaternion.LookRotation(cam.position - transform.position);
        Vector3 temp = transform.rotation.eulerAngles;
        temp.y = Camera.main.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(temp);
        //transform.LookAt(Camera.main.transform);
    }
}
