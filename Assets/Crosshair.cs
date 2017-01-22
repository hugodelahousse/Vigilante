using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {
    public float hackRange = 16;
    GameController gameController;
    public GameObject popup;
	public GameObject elevatorPopup;

	// Use this for initialization

	void Start () {
        gameController = FindObjectOfType<GameController>();
		elevatorPopup.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Physics.Linecast(Camera.main.transform.position, Camera.main.transform.position + 16 * (transform.position - Camera.main.transform.position ).normalized, out hit, 1<<11);
        Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position + 16 * (transform.position - Camera.main.transform.position).normalized);
        popup.SetActive(hit.collider);
        if (hit.collider) {
            if(Input.GetButton("Interact"))
                gameController.Hack(hit.collider.transform.parent.gameObject);
        }

		{
			Physics.Linecast(Camera.main.transform.position, Camera.main.transform.position + 6 * (transform.position - Camera.main.transform.position).normalized, out hit, 1 << 12);
			elevatorPopup.SetActive(hit.collider);
			if (hit.collider)
			{
				if (Input.GetButton("Interact"))
					gameController.OpenElevatorMenu();
			}
		}

		if (gameController.inElevatorMenu && Input.GetKeyUp(KeyCode.Escape))
		{
			gameController.CloseElevatorMenu();
		}

    }
}
