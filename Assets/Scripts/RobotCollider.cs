using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCollider : MonoBehaviour {

    void OnTriggerEnter(Collider other){
        Debug.Log("Collision Enter");
    }

}
