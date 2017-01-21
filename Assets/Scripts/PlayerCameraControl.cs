using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour {

	public GameObject player;
	public GameObject cameraVertical;

	public float horizontalRotateSpeed = 1.0f;
	public float verticalRotateSpeed = 1.0f;

	public float playerMoveSpeed = 1.0f;

	public float playerRadius = 1.0f;
	public float playerHeight = 2.0f;

	public float jumpVelocity = 6.0f;

	private Vector3 followOffset;

	private bool isGrounded = false;
	private float yVelocity = 0.0f;

	// Use this for initialization
	void Start () {
		followOffset = transform.localPosition;

		isGrounded = Physics.Raycast(new Ray(player.transform.position, Vector3.down), playerHeight / 2f, Physics.AllLayers);
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis("Mouse X") * horizontalRotateSpeed * Time.deltaTime;
		float vertical = Input.GetAxis("Mouse Y") * verticalRotateSpeed * Time.deltaTime * -1.0f;

		player.transform.Rotate(Vector3.up, horizontal);

		cameraVertical.transform.Rotate(Vector3.right, vertical);

		Vector3 forwardMotion = Input.GetAxis("Vertical") * player.transform.forward ;
		Vector3 sideMotion = Input.GetAxis("Horizontal") * player.transform.right;

		Vector3 desiredMove = forwardMotion + sideMotion;

		desiredMove = desiredMove.normalized * playerMoveSpeed * Time.deltaTime;

		if (isGrounded)
		{
			if (yVelocity <= 0.0f)
			{
				yVelocity = 0.0f;
			}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				yVelocity = jumpVelocity;
			}
		}
		else
		{
			yVelocity += Physics.gravity.y * Time.deltaTime;
		}

		desiredMove.y = yVelocity * Time.deltaTime;

		player.GetComponent<CharacterController>().Move(desiredMove);

		isGrounded = Physics.Raycast(new Ray(player.transform.position, Vector3.down), playerHeight / 2f, Physics.AllLayers);
	}
}
