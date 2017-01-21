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

	private bool isCrouching = false;
	private bool registeredCrouchChanges = true;

	private Vector3 movementVec;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis("Mouse X") * horizontalRotateSpeed * Time.deltaTime;
		float vertical = Input.GetAxis("Mouse Y") * verticalRotateSpeed * Time.deltaTime * -1.0f;

		player.transform.parent.Rotate(Vector3.up, horizontal);

		cameraVertical.transform.Rotate(Vector3.right, vertical);

		float vertAxis = Input.GetAxis("Vertical");
		float horizAxis = Input.GetAxis("Horizontal");

		if (vertAxis < 0.0f)
		{
			player.GetComponent<SpriteRenderer>().sprite = isCrouching ? backwardCrouch : backward;
		}
		else if (vertAxis > 0.0f)
		{
			player.GetComponent<SpriteRenderer>().sprite = isCrouching ? forwardCrouch : forward;
		}
		else
		{
			bool isForward = (player.GetComponent<SpriteRenderer>().sprite == forward) 
						  || (player.GetComponent<SpriteRenderer>().sprite == forwardCrouch);
			player.GetComponent<SpriteRenderer>().sprite = 
				isCrouching ? 
				  (isForward ? forwardCrouch : backwardCrouch) 
				: (isForward ? forward : backward);
		}

		{
			bool isForward = (player.GetComponent<SpriteRenderer>().sprite == forward)
							  || (player.GetComponent<SpriteRenderer>().sprite == forwardCrouch);
			if (horizAxis > 0.0f)
			{
				player.GetComponent<SpriteRenderer>().flipX = !isForward;
			}
			else if (horizAxis < 0.0f)
			{
				player.GetComponent<SpriteRenderer>().flipX = isForward;
			}
		}

		Vector3 forwardMotion = vertAxis * player.transform.forward ;
		Vector3 sideMotion = horizAxis * player.transform.right;

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

		if (Input.GetKeyDown(crouchButton))
		{
			isCrouching = true;
			player.GetComponent<CapsuleCollider>().height = playerHeight / 2;
			registeredCrouchChanges = false;
		}
		else if(Input.GetKeyUp(crouchButton))
		{
			isCrouching = false;
			player.GetComponent<CapsuleCollider>().height = playerHeight;
			registeredCrouchChanges = false;
		}
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

		if (!registeredCrouchChanges)
		{
			if (isCrouching)
			{
				player.transform.Translate(Vector3.up * -playerHeight / 4);
				player.transform.parent.GetComponent<CharacterController>().center = Vector3.up * -playerHeight / 4;
			}
			else
			{
				player.transform.Translate(Vector3.up * playerHeight / 4);
				player.transform.parent.GetComponent<CharacterController>().center = Vector3.zero;
			}

			registeredCrouchChanges = true;
		}

		if (newY == oldY)
		{
			isGrounded = true;
			yVelocity = 0.0f;
		}
	}
}
