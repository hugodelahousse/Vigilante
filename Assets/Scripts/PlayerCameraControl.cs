using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour {

	public GameObject player;
	public GameObject cameraVertical;

	public float horizontalRotateSpeed = 1.0f;
	public float verticalRotateSpeed = 1.0f;

	public float playerMoveSpeed = 3.0f;
	public float crouchSpeed = 2.0f;

	public float playerRadius = 1.0f;
	public float playerHeight = 2.0f;

	public float jumpVelocity = 6.0f;

	public KeyCode crouchButton = KeyCode.C;

	public Sprite forward;
	public Sprite backward;
	public Sprite forwardCrouch;
	public Sprite backwardCrouch;

	private bool isGrounded = false;
	private float yVelocity = 0.0f;

	private Vector3 movementVec;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis("Mouse X") * horizontalRotateSpeed * Time.deltaTime;
		float vertical = Input.GetAxis("Mouse Y") * verticalRotateSpeed * Time.deltaTime * -1.0f;

		bool isCrouching = Input.GetKey(crouchButton);

		player.transform.parent.Rotate(Vector3.up, horizontal);

		cameraVertical.transform.Rotate(Vector3.right, vertical);

		Vector3 forwardMotion = Input.GetAxis("Vertical") * player.transform.forward ;
		Vector3 sideMotion = Input.GetAxis("Horizontal") * player.transform.right;

		Vector3 desiredMove = forwardMotion + sideMotion;

		desiredMove = desiredMove.normalized * (isCrouching ? crouchSpeed : playerMoveSpeed);

		if (isGrounded && !isCrouching && Input.GetKeyDown(KeyCode.Space))
		{
			yVelocity = jumpVelocity;
		}

		movementVec.x = desiredMove.x;
		movementVec.z = desiredMove.z;

		float oldY = player.transform.parent.position.y;
		player.transform.parent.GetComponent<CharacterController>().height = (isCrouching ? playerHeight / 2 : playerHeight);
		float newY = player.transform.parent.position.y;
		transform.Translate(Vector3.up * (oldY - newY));

		if (isCrouching)
		{
			player.GetComponent<CapsuleCollider>().height = playerHeight / 2;
			player.GetComponent<SpriteRenderer>().sprite = forwardCrouch;
		}
		else
		{
			player.GetComponent<CapsuleCollider>().height = playerHeight;
			player.GetComponent<SpriteRenderer>().sprite = forward;
		}

		Debug.Log("isGrounded: " + isGrounded + " yVelocity: " + yVelocity);
	}

	void FixedUpdate()
	{
		isGrounded = player.transform.parent.GetComponent<CharacterController>().isGrounded;

		if (isGrounded)
		{
			if (yVelocity <= 0.0f)
			{
				yVelocity = 0.0f;
			}
		}
		else
		{
			yVelocity += Physics.gravity.y * Time.fixedDeltaTime;
		}

		movementVec.y = yVelocity;

		float oldY = player.transform.parent.position.y;
		player.transform.parent.GetComponent<CharacterController>().Move(movementVec * Time.fixedDeltaTime);
		float newY = player.transform.parent.position.y;

		if (newY == oldY)
		{
			isGrounded = true;
			yVelocity = 0.0f;
		}
	}
}
