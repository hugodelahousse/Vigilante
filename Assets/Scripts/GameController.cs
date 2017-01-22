using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    // Use this for initialization

    private bool hacking_ = false;
    public bool hacking
    {
        get { return hacking_; }
        set
        {
            hackingPanel.SetActive(value);
            hacking_ = value;
        }
    }
    public GameObject hackingTarget;
    public GameObject hackingPanel;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Hack(GameObject target) {
        hacking = true;
        hackingTarget = target;
    }
    public void StopHacking()
    {
        hacking = false;
        hackingTarget = null;
        Debug.Log("Stop hacking");
    }
    public void ValidateHacking(HackingMinigame minigame)
    {
        for (int i = 0; i < minigame.matchAmplitudes.Length; ++i)
            if (minigame.matchAmplitudes[i] != minigame.playerAmplitudes[i])
                return;
        switch (hackingTarget.tag)
        {
            case "Camera":
                hackingTarget.GetComponent<SurveilanceCam>().coneVision.SetActive(false);
                hackingTarget.GetComponent<SurveilanceCam>().enabled = false;
                hackingTarget.GetComponentInChildren<SphereCollider>().enabled = false;
                break;
            default:
                break;
        }
        //hackingTarget.SetActive(false);
        StopHacking();
        return;
    }
}
