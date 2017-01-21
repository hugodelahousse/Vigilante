using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour {

	public GameObject player;
	public GameObject cameraVertical;

	public float horizontalRotateSpeed = 1.0f;
	public float verticalRotateSpeed = 1.0f;

	public float playerMoveSpeed = 1.0f;

	private Vector3 followOffset;

	private Vector3 lastMousePos;

	// Use this for initialization
	void Start () {
		followOffset = transform.localPosition;

		lastMousePos = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mouseDelta = Input.mousePosition - lastMousePos;
		lastMousePos = Input.mousePosition;
		float horizontal = Input.GetAxis("Mouse X") * horizontalRotateSpeed * Time.deltaTime;
		float vertical = Input.GetAxis("Mouse Y") * verticalRotateSpeed * Time.deltaTime * -1.0f;

		player.transform.Rotate(Vector3.up, horizontal);

		cameraVertical.transform.Rotate(Vector3.right, vertical);

		Vector3 forwardMotion = Input.GetAxis("Vertical") * player.transform.forward * playerMoveSpeed * Time.deltaTime;
		Vector3 sideMotion = Input.GetAxis("Horizontal") * player.transform.right * playerMoveSpeed * Time.deltaTime;

		player.transform.position += (forwardMotion + sideMotion);

	}
}
